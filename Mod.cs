// STFU Microsoft
#pragma warning disable CA2211

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.Utils;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.ID;
using static Terraria.Main;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: scrollbar alfa config
        // TODO: play sound when auto use item changes
        // TODO: signal auto select being active through some other, unique way
        // TODO: consider crafting item clicked on crafting window
        // TODO: consider displaying item's name on top of hotbar even while the inventory is up
        // TODO: OnServerConfigChanged is not needed at the time but do consider it
        // TODO: look into fasterUIs mod in steam
        // TODO: visually signal somehow when the hotbar is locked without needing to open the inventory
        // FIXME: text of baner buff icon has trouble displaying full text if the icon is too low on the screen - 12/22/22: can't replicate bug
        public static event Action OnClientConfigChanged;

        // updated every frame by DrawInterface_Logic_0
        public static List<int> ActiveBuffsIndexes { get; set; }
        public static ClientConfig ClientConfig { get; set; }

        internal static void RaiseClientConfigChanged() => OnClientConfigChanged?.Invoke();

        // TODO: unload
        public override void Load() {
            IL.Terraria.Main.DrawInventory += IL_Main_DrawInventory;
            if (!ClientConfig.DisableThisModChangesToTheItemSlots) {
                IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color +=
                    IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
            }
            if (!ClientConfig.DisableThisModChangesToTheHotbar) {
                IL.Terraria.Player.Update += IL_Player_Update;
                On.Terraria.Player.ScrollHotbar += On_Player_ScrollHotbar;
            }

            if (Main.netMode != NetmodeID.Server) {
                BetterGameUI.Assets.Load();
            }
        }

        public override void Unload() {
            IL.Terraria.Main.DrawInventory -= IL_Main_DrawInventory;
            IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color -=
                IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color;
            IL.Terraria.Player.Update -= IL_Player_Update;
            On.Terraria.Player.ScrollHotbar -= On_Player_ScrollHotbar;
            
            OnClientConfigChanged = null;
            ActiveBuffsIndexes = null;
            ClientConfig = null;

            BetterGameUI.Assets.Unload();
        }

        public static void IL_Main_DrawInventory(ILContext il) {
            var c = new ILCursor(il);

            var mouseTextHackZoomInfo = typeof(Main).GetMethod("MouseTextHackZoom",
                    BindingFlags.Public | BindingFlags.Instance,
                    new[] { typeof(string), typeof(int), typeof(byte), typeof(string) });

            if (!c.TryGotoNext(MoveType.After,
                x => x.MatchCall(mouseTextHackZoomInfo))) {
                return;
            }

            var afterVanillaBuffsBarDrawLabel = c.MarkLabel();

            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchLdloc(55) &&
                x.Next.MatchLdcI4(247) &&
                x.Next.Next.MatchAdd())) {
                return;
            }

            c.Emit(OpCodes.Ldsfld, typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static));
            c.Emit(OpCodes.Call, typeof(UI.UISystem).GetMethod("Draw_InventoryBuffsBar"));
            c.Emit(OpCodes.Br, afterVanillaBuffsBarDrawLabel);
        }

        public static void IL_Player_Update(ILContext il) {
            // TODO: document this process
            il.IL.InsertAfter(il.Instrs[1854], il.IL.Create(OpCodes.Call, typeof(Mod).GetMethod("Player_Update_Detour")));
            il.IL.InsertAfter(il.Instrs[1854], il.IL.Create(OpCodes.Ldarg_0));

            il.IL.InsertAfter(il.Instrs[1419], il.IL.Create(OpCodes.Br, il.Instrs[1966]));
            il.IL.InsertAfter(il.Instrs[1419], il.IL.Create(OpCodes.Call, typeof(Mod).GetMethod("Player_Update_Detour")));
            il.IL.InsertAfter(il.Instrs[1419], il.IL.Create(OpCodes.Ldarg_0));

            // remove instructions which are not gonna be run to compensate for the instructions inserted
            il.IL.RemoveAt(1423);
            il.IL.RemoveAt(1423);
            il.IL.RemoveAt(1423);
            il.IL.RemoveAt(1423);
            il.IL.RemoveAt(1423);
        }

        public static void On_Player_ScrollHotbar(On.Terraria.Player.orig_ScrollHotbar orig, Terraria.Player player,
            int Offset) 
        {
            if (PointedToItem >= 10) {
                return;
            }

            while (Offset > 9) {
                Offset -= 10;
            }

            while (Offset < 0) {
                Offset += 10;
            }

            PointedToItem += Offset;
            if (Offset != 0) {
                Reflection.SoundEngine.PlaySound(12);
                int num = PointedToItem - Offset;
                player.DpadRadial.ChangeSelection(-1);
                player.CircularRadial.ChangeSelection(-1);
                PointedToItem = num + Offset;
            }

            if (player.changeItem >= 0) {
                if (PointedToItem != player.changeItem)
                    Reflection.SoundEngine.PlaySound(12);

                PointedToItem = player.changeItem;
                player.changeItem = -1;
            }

            if (PointedToItem != 58) {
                while (PointedToItem > 9) {
                    PointedToItem -= 10;
                }

                while (PointedToItem < 0) {
                    PointedToItem += 10;
                }
            }
        }

        public static int PointedToItem;
        public static bool HasControlUseItemStoppedSinceAnimationStarted;
        public static void Player_Update_Detour(Terraria.Player player) {
            if (player.itemAnimation == 0 && player.ItemTimeIsZero && player.reuseDelay == 0) {
                player.dropItemCheck();
            }

            int prevPointedToItem = PointedToItem;
            if (!Main.drawingPlayerChat && PointedToItem != 58 && !Main.editSign && !Main.editChest) {
                if (PlayerInput.Triggers.Current.Hotbar1) {
                    PointedToItem = 0;
                }
                if (PlayerInput.Triggers.Current.Hotbar2) {
                    PointedToItem = 1;
                }
                if (PlayerInput.Triggers.Current.Hotbar3) {
                    PointedToItem = 2;
                }
                if (PlayerInput.Triggers.Current.Hotbar4) {
                    PointedToItem = 3;
                }
                if (PlayerInput.Triggers.Current.Hotbar5) {
                    PointedToItem = 4;
                }
                if (PlayerInput.Triggers.Current.Hotbar6) {
                    PointedToItem = 5;
                }
                if (PlayerInput.Triggers.Current.Hotbar7) {
                    PointedToItem = 6;
                }
                if (PlayerInput.Triggers.Current.Hotbar8) {
                    PointedToItem = 7;
                }
                if (PlayerInput.Triggers.Current.Hotbar9) {
                    PointedToItem = 8;
                }
                if (PlayerInput.Triggers.Current.Hotbar10) {
                    PointedToItem = 9;
                }

                int selectedBinding = player.DpadRadial.SelectedBinding;
                int selectedBinding2 = player.CircularRadial.SelectedBinding;
                _ = player.QuicksRadial.SelectedBinding;
                player.DpadRadial.Update();
                player.CircularRadial.Update();
                player.QuicksRadial.Update();
                if (player.CircularRadial.SelectedBinding >= 0 && selectedBinding2 != player.CircularRadial.SelectedBinding) {
                    player.DpadRadial.ChangeSelection(-1);
                }

                if (player.DpadRadial.SelectedBinding >= 0 && selectedBinding != player.DpadRadial.SelectedBinding) {
                    player.CircularRadial.ChangeSelection(-1);
                }

                if (player.QuicksRadial.SelectedBinding != -1 && PlayerInput.Triggers.JustReleased.RadialQuickbar && 
                        !PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed) 
                {
                    switch (player.QuicksRadial.SelectedBinding) {
                        case 0:
                            player.QuickMount();
                            break;
                        case 1:
                            player.QuickHeal();
                            break;
                        case 2:
                            player.QuickBuff();
                            break;
                        case 3:
                            player.QuickMana();
                            break;
                    }
                }

                if (player.controlTorch || prevPointedToItem != PointedToItem) {
                    player.DpadRadial.ChangeSelection(-1);
                    player.CircularRadial.ChangeSelection(-1);
                }
            }

            bool flag8 = Main.hairWindow;
            if (flag8) {
                int y = Main.screenHeight / 2 + 60;
                flag8 = new Rectangle(Main.screenWidth / 2 - Terraria.GameContent.TextureAssets.HairStyleBack.Width() / 2, y,
                    Terraria.GameContent.TextureAssets.HairStyleBack.Width(),
                    Terraria.GameContent.TextureAssets.HairStyleBack.Height()).Contains(Main.MouseScreen.ToPoint());
            }

            if (prevPointedToItem != PointedToItem) {
                Reflection.SoundEngine.PlaySound(12);
                if (CaptureManager.Instance.Active) {
                    CaptureManager.Instance.Active = false;
                }
            }

            if (Main.mapFullscreen) {
                float num7 = PlayerInput.ScrollWheelDelta / 120;
                if (PlayerInput.UsingGamepad)
                    num7 += (float)(PlayerInput.Triggers.Current.HotbarPlus.ToInt() - PlayerInput.Triggers.Current.HotbarMinus.ToInt()) * 0.1f;

                Main.mapFullscreenScale *= 1f + num7 * 0.3f;
            }
            else if (CaptureManager.Instance.Active) {
                CaptureManager.Instance.Scrolling();
            }
            else if (!flag8) {
                if (Reflection.PlayerInput.GetMouseInModdedUI().Count > 0) {
                    //Do nothing
                }
                else if (!Main.playerInventory) {
                    Reflection.Player.HandleHotbar(player);
                }
                else {
                    int num8 = Terraria.Player.GetMouseScrollDelta();
                    bool flag9 = true;
                    if (Main.recBigList) {
                        int num9 = 42;
                        int num10 = 340;
                        int num11 = 310;
                        PlayerInput.SetZoom_UI();
                        int num12 = (Main.screenWidth - num11 - 280) / num9;
                        int num13 = (Main.screenHeight - num10 - 20) / num9;
                        if (new Rectangle(num11, num10, num12 * num9, num13 * num9).Contains(Main.MouseScreen.ToPoint())) {
                            num8 *= -1;
                            int num14 = Math.Sign(num8);
                            while (num8 != 0) {
                                if (num8 < 0) {
                                    Main.recStart -= num12;
                                    if (Main.recStart < 0)
                                        Main.recStart = 0;
                                }
                                else {
                                    Main.recStart += num12;
                                    if (Main.recStart > Main.numAvailableRecipes - num12)
                                        Main.recStart = Main.numAvailableRecipes - num12;
                                }

                                num8 -= num14;
                            }
                        }

                        PlayerInput.SetZoom_World();
                    }

                    if (flag9) {
                        Main.focusRecipe += num8;
                        if (Main.focusRecipe > Main.numAvailableRecipes - 1)
                            Main.focusRecipe = Main.numAvailableRecipes - 1;

                        if (Main.focusRecipe < 0)
                            Main.focusRecipe = 0;
                    }
                }

                Reflection.PlayerInput.GetMouseInModdedUI().Clear();
            }

            if (player.controlUseItem) {
                if (player.ItemAnimationJustStarted) {
                    HasControlUseItemStoppedSinceAnimationStarted = false;
                }
            }
            else {
                HasControlUseItemStoppedSinceAnimationStarted = true;
            }

            if (player.controlTorch) {
                player.nonTorch = PointedToItem;
            }

            if (player.selectedItem != 58) {
                if (player.itemAnimation == 0 && player.ItemTimeIsZero && player.reuseDelay == 0) {
                    player.selectedItem = PointedToItem;
                }
                else if (player.ItemAnimationEndingOrEnded && HasControlUseItemStoppedSinceAnimationStarted &&
                    !player.controlTorch) {
                    player.selectedItem = PointedToItem;
                    player.reuseDelay = 0;
                }
            }
        }

        public static bool DrawInterface_Hotbar() {
            if (playerInventory || player[myPlayer].ghost || inFancyUI) {
                return true;
            }

            // set to 'items' in case the next conditional turns out false
            string text = Lang.inter[37].Value;
            // if selected item has a name AND that name is not empty
            if (player[myPlayer].inventory[PointedToItem].Name != null && player[myPlayer].inventory[PointedToItem].Name != "") {
                // get name of selected item
                text = player[myPlayer].inventory[PointedToItem].AffixName();
            }

            // draw selected item name
            Vector2 vector = Terraria.GameContent.FontAssets.MouseText.Value.MeasureString(text) / 2f;
            spriteBatch.DrawString(Terraria.GameContent.FontAssets.MouseText.Value, text, new Vector2(236f - vector.X, 0f), new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, default, 1f, SpriteEffects.None, 0f);

            // iterate over each item in the hotbar and draw it
            int num = 20;
            for (int i = 0; i < 10; i++) {
                // if the item to draw is the selected one, progresively scale it up.
                hotbarScale[i] += (i == PointedToItem) ? 0.05f : -0.05f;
                hotbarScale[i] = Math.Clamp(hotbarScale[i], 0.75f, 1f);

                float itrHotbarScale = hotbarScale[i];
                int num3 = (int)(20f + 22f * (1f - itrHotbarScale));
                // a = 230 if item is selected, a = 185 otherwise
                int a = (int)(75f + 150f * itrHotbarScale);
                Color lightColor = new Color(255, 255, 255, a);
                // if the hotbar is not locked AND the player input doesn't say to ignore the mouse interface AND the mouse is hovering this hotbar slot
                if (!player[myPlayer].hbLocked && mouseX >= num && mouseX <= num + 
                    Terraria.GameContent.TextureAssets.InventoryBack.Width() * hotbarScale[i] && mouseY >= num3 && mouseY <= num3 + Terraria.GameContent.TextureAssets.InventoryBack.Height() * hotbarScale[i] && !player[myPlayer].channel) {
                    if (!Terraria.GameInput.PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        player[myPlayer].cursorItemIconEnabled = false;
                    }

                    // NOTE: clicking on item is reflected one frame late
                    if (mouseLeft && !blockMouse) {
                        player[myPlayer].changeItem = i;
                    }

                    // TODO: consider adding comas to stacks of very big numbers
                    hoverItemName = player[myPlayer].inventory[i].AffixName();
                    if (player[myPlayer].inventory[i].stack > 1)
                        hoverItemName = hoverItemName + " (" + player[myPlayer].inventory[i].stack + ")";

                    rare = player[myPlayer].inventory[i].rare;
                }

                float prevInventoryScale = inventoryScale;
                inventoryScale = itrHotbarScale;
                Terraria.UI.ItemSlot.Draw(spriteBatch, player[myPlayer].inventory, 13, i, new Vector2(num, num3), lightColor);
                inventoryScale = prevInventoryScale;
                num += (int)(Terraria.GameContent.TextureAssets.InventoryBack.Width() * hotbarScale[i]) + 4;
            }

            // if the selected item doesn't fit in the scrollbar (such as those selected by the smart select)   
            if (player[myPlayer].selectedItem >= 10 && (player[myPlayer].selectedItem != 58 || mouseItem.type > 0)) {
                // a2 will always equal a non-transitioning selected item alpha
                int a2 = (int)(75f + 150f * 1f);
                Color lightColor2 = new Color(255, 255, 255, a2);
                float prevInventoryScale = inventoryScale;
                inventoryScale = 0.75F;
                float yPos = 25f;
                Terraria.UI.ItemSlot.Draw(spriteBatch, player[myPlayer].inventory, 13, player[myPlayer].selectedItem, new Vector2(num, yPos), lightColor2);
                inventoryScale = prevInventoryScale;
            }

            return true;
        }

        public void IL_ItemSlot_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color(MonoMod.Cil.ILContext il) {
            // 1554: - if (context == 0 && slot < 10 && player.player.selectedItem == slot) {
            // 1554: + if (context == 0 && player.player.selectedItem == slot) {
            il.Instrs[89] = il.IL.Create(OpCodes.Nop);
            il.Instrs[90] = il.IL.Create(OpCodes.Nop);
            il.Instrs[91] = il.IL.Create(OpCodes.Nop);

            // - 1571: else if (context == 0 && slot < 10) {
            // + 1571: else if (context == 0) {
            // + 1572:     if (slot < 10) {
            // + 1573:         value = TextureAssets.InventoryBack9.Value;
            // + 1574:     }
            il.Instrs[214].Operand = il.Instrs[218];
        }
    }
}