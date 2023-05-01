// STFU Microsoft
#pragma warning disable CA2211

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using static Terraria.Main;

namespace BetterGameUI.Edits
{
    public static class HotbarEdits
    {
        public static int PointedToItem { get; set; }
        public static bool HasControlUseItemStoppedSinceAnimationStarted { get; set; }

        public static void Load()
        {
            try
            {
                IL.Terraria.Player.Update += IL_Player_Update;
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                throw new Exception.FailedToLoadHotbarEdits(e);
            }
            catch (Exception.InstructionNotFound e)
            {
                throw new Exception.FailedToLoadHotbarEdits(e);
            }

            On.Terraria.Player.ScrollHotbar += On_Player_ScrollHotbar;
        }

        public static void IL_Player_Update(ILContext il)
        {
            var c = new ILCursor(il);

            // ->: if (selectedItem != 58) {
            //   :     SmartSelectLookup()
            if (!c.TryGotoNext(MoveType.Before,
                x => x.MatchLdarg(0) &&
                x.Next.MatchLdfld("Terraria.Player", "selectedItem") &&
                x.Next.Next.MatchLdcI4(58) &&
                x.Next.Next.Next.MatchBeq(out _) &&
                x.Next.Next.Next.Next.MatchLdarg(0) &&
                x.Next.Next.Next.Next.Next.MatchCall("Terraria.Player", "SmartSelectLookup")))
            {
                throw new Exception.InstructionNotFound();
            }

            var label = c.MarkLabel();

            // ->: if (itemAnimation == 0 && ItemTimeIsZero && reuseDelay == 0) {
            if (!c.TryGotoPrev(MoveType.Before,
                x => x.MatchLdarg(0) &&
                x.Next.MatchLdfld("Terraria.Player", "itemAnimation") &&
                x.Next.Next.MatchBrtrue(out _) &&
                x.Next.Next.Next.MatchLdarg(0) &&
                x.Next.Next.Next.Next.MatchCall("Terraria.Player", "get_ItemTimeIsZero") &&
                x.Next.Next.Next.Next.Next.MatchBrfalse(out _) &&
                x.Next.Next.Next.Next.Next.Next.MatchLdarg(0) &&
                x.Next.Next.Next.Next.Next.Next.Next.MatchLdfld("Terraria.Player", "reuseDelay") &&
                x.Next.Next.Next.Next.Next.Next.Next.Next.MatchBrtrue(out _)))
            {
                throw new Exception.InstructionNotFound();
            }
            c.MoveAfterLabels();

            // ++: Player_Update_Detour(this);
            // ++: if (false) {
            //   :     if (itemAnimation == 0 && ItemTimeIsZero && reuseDelay == 0) {
            //   :     [...]
            // ++: }
            //   :
            //   : if (selectedItem != 58) {
            //   :     SmartSelectLookup()
            c.Emit(OpCodes.Ldarg_0);
            // TODO: emit individual instruccions instead of delegate
            c.EmitDelegate(Player_Update_Detour);
            c.Emit(OpCodes.Br, label);
        }

        public static void On_Player_ScrollHotbar(On.Terraria.Player.orig_ScrollHotbar orig, Terraria.Player player,
            int Offset)
        {
            Offset *= Mod.ClientConfig.Hotbar_InvertWheelScroll ? 1 : -1;

            if (PointedToItem >= 10)
            {
                return;
            }

            while (Offset > 9)
            {
                Offset -= 10;
            }

            while (Offset < 0)
            {
                Offset += 10;
            }

            PointedToItem += Offset;
            if (Offset != 0)
            {
                Reflection.SoundEngineReflection.PlaySound(12);
                int num = PointedToItem - Offset;
                player.DpadRadial.ChangeSelection(-1);
                player.CircularRadial.ChangeSelection(-1);
                PointedToItem = num + Offset;
            }

            if (player.changeItem >= 0)
            {
                if (PointedToItem != player.changeItem)
                    Reflection.SoundEngineReflection.PlaySound(12);

                PointedToItem = player.changeItem;
                player.changeItem = -1;
            }

            if (PointedToItem != 58)
            {
                while (PointedToItem > 9)
                {
                    PointedToItem -= 10;
                }

                while (PointedToItem < 0)
                {
                    PointedToItem += 10;
                }
            }
        }

        public static void Player_Update_Detour(Terraria.Player player)
        {
            if (player.itemAnimation == 0 && player.ItemTimeIsZero && player.reuseDelay == 0)
            {
                player.dropItemCheck();
            }

            int prevPointedToItem = PointedToItem;
            if (!drawingPlayerChat && PointedToItem != 58 && !editSign && !editChest)
            {
                if (PlayerInput.Triggers.Current.Hotbar1)
                {
                    PointedToItem = 0;
                }
                if (PlayerInput.Triggers.Current.Hotbar2)
                {
                    PointedToItem = 1;
                }
                if (PlayerInput.Triggers.Current.Hotbar3)
                {
                    PointedToItem = 2;
                }
                if (PlayerInput.Triggers.Current.Hotbar4)
                {
                    PointedToItem = 3;
                }
                if (PlayerInput.Triggers.Current.Hotbar5)
                {
                    PointedToItem = 4;
                }
                if (PlayerInput.Triggers.Current.Hotbar6)
                {
                    PointedToItem = 5;
                }
                if (PlayerInput.Triggers.Current.Hotbar7)
                {
                    PointedToItem = 6;
                }
                if (PlayerInput.Triggers.Current.Hotbar8)
                {
                    PointedToItem = 7;
                }
                if (PlayerInput.Triggers.Current.Hotbar9)
                {
                    PointedToItem = 8;
                }
                if (PlayerInput.Triggers.Current.Hotbar10)
                {
                    PointedToItem = 9;
                }

                int selectedBinding = player.DpadRadial.SelectedBinding;
                int selectedBinding2 = player.CircularRadial.SelectedBinding;
                _ = player.QuicksRadial.SelectedBinding;
                player.DpadRadial.Update();
                player.CircularRadial.Update();
                player.QuicksRadial.Update();
                if (player.CircularRadial.SelectedBinding >= 0 && selectedBinding2 != player.CircularRadial.SelectedBinding)
                {
                    player.DpadRadial.ChangeSelection(-1);
                }

                if (player.DpadRadial.SelectedBinding >= 0 && selectedBinding != player.DpadRadial.SelectedBinding)
                {
                    player.CircularRadial.ChangeSelection(-1);
                }

                if (player.QuicksRadial.SelectedBinding != -1 && PlayerInput.Triggers.JustReleased.RadialQuickbar &&
                        !PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed)
                {
                    switch (player.QuicksRadial.SelectedBinding)
                    {
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

                if (player.controlTorch || prevPointedToItem != PointedToItem)
                {
                    player.DpadRadial.ChangeSelection(-1);
                    player.CircularRadial.ChangeSelection(-1);
                }
            }

            bool flag8 = hairWindow;
            if (flag8)
            {
                int y = screenHeight / 2 + 60;
                flag8 = new Rectangle(screenWidth / 2 - Terraria.GameContent.TextureAssets.HairStyleBack.Width() / 2, y,
                    Terraria.GameContent.TextureAssets.HairStyleBack.Width(),
                    Terraria.GameContent.TextureAssets.HairStyleBack.Height()).Contains(MouseScreen.ToPoint());
            }

            if (prevPointedToItem != PointedToItem)
            {
                Reflection.SoundEngineReflection.PlaySound(12);
                if (CaptureManager.Instance.Active)
                {
                    CaptureManager.Instance.Active = false;
                }
            }

            if (mapFullscreen)
            {
                float num7 = PlayerInput.ScrollWheelDelta / 120;
                if (PlayerInput.UsingGamepad)
                    num7 += (PlayerInput.Triggers.Current.HotbarPlus.ToInt() - PlayerInput.Triggers.Current.HotbarMinus.ToInt()) * 0.1f;

                mapFullscreenScale *= 1f + num7 * 0.3f;
            }
            else if (CaptureManager.Instance.Active)
            {
                CaptureManager.Instance.Scrolling();
            }
            else if (!flag8)
            {
                if (Reflection.PlayerInputReflection.GetMouseInModdedUI().Count > 0)
                {
                    //Do nothing
                }
                else if (!playerInventory)
                {
                    Reflection.PlayerReflection.HandleHotbar(player);
                }
                else
                {
                    int num8 = Terraria.Player.GetMouseScrollDelta();
                    if (!Mod.ClientConfig.RecipesList_InvertWheelScroll)
                    {
                        num8 *= -1;
                    }

                    bool flag9 = true;
                    if (recBigList)
                    {
                        int num9 = 42;
                        int num10 = 340;
                        int num11 = 310;
                        PlayerInput.SetZoom_UI();
                        int num12 = (screenWidth - num11 - 280) / num9;
                        int num13 = (screenHeight - num10 - 20) / num9;
                        if (new Rectangle(num11, num10, num12 * num9, num13 * num9).Contains(MouseScreen.ToPoint()))
                        {
                            num8 *= -1;
                            int num14 = Math.Sign(num8);
                            while (num8 != 0)
                            {
                                if (num8 < 0)
                                {
                                    recStart -= num12;
                                    if (recStart < 0)
                                        recStart = 0;
                                }
                                else
                                {
                                    recStart += num12;
                                    if (recStart > numAvailableRecipes - num12)
                                        recStart = numAvailableRecipes - num12;
                                }

                                num8 -= num14;
                            }
                        }

                        PlayerInput.SetZoom_World();
                    }

                    if (flag9)
                    {
                        focusRecipe += num8;
                        if (focusRecipe > numAvailableRecipes - 1)
                            focusRecipe = numAvailableRecipes - 1;

                        if (focusRecipe < 0)
                            focusRecipe = 0;
                    }
                }

                Reflection.PlayerInputReflection.GetMouseInModdedUI().Clear();
            }

            if (player.controlUseItem)
            {
                if (player.ItemAnimationJustStarted)
                {
                    HasControlUseItemStoppedSinceAnimationStarted = false;
                }
            }
            else
            {
                HasControlUseItemStoppedSinceAnimationStarted = true;
            }

            if (player.controlTorch)
            {
                player.nonTorch = PointedToItem;
            }

            if (player.selectedItem != 58)
            {
                if (player.itemAnimation == 0 && player.ItemTimeIsZero && player.reuseDelay == 0)
                {
                    player.selectedItem = PointedToItem;
                }
                else if (player.ItemAnimationEndingOrEnded && HasControlUseItemStoppedSinceAnimationStarted &&
                    !player.controlTorch)
                {
                    player.selectedItem = PointedToItem;
                    player.reuseDelay = 0;
                }
            }
        }

        public static bool DrawInterface_Hotbar()
        {
            if (playerInventory || player[myPlayer].ghost || inFancyUI)
            {
                return true;
            }

            Texture2D value = TextureAssets.HbLock[(!player[myPlayer].hbLocked) ? 1 : 0].Value;
            spriteBatch.Draw(value, new Vector2(0, 21), value.Frame(2), Microsoft.Xna.Framework.Color.White, 0f, default(Vector2), 0.9f, SpriteEffects.None, 0f);

            // set to 'items' in case the next conditional turns out false
            string text = Lang.inter[37].Value;
            // if selected item has a name AND that name is not empty
            if (player[myPlayer].inventory[PointedToItem].Name != null && player[myPlayer].inventory[PointedToItem].Name != "")
            {
                // get name of selected item
                text = player[myPlayer].inventory[PointedToItem].AffixName();
            }

            // draw selected item name
            Vector2 vector = Terraria.GameContent.FontAssets.MouseText.Value.MeasureString(text) / 2f;
            spriteBatch.DrawString(Terraria.GameContent.FontAssets.MouseText.Value, text, new Vector2(236f - vector.X, 0f), new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, default, 1f, SpriteEffects.None, 0f);

            // iterate over each item in the hotbar and draw it
            int num = 20;
            for (int i = 0; i < 10; i++)
            {
                // if the item to draw is the selected one, progresively scale it up.
                hotbarScale[i] += i == PointedToItem ? 0.05f : -0.05f;
                hotbarScale[i] = Math.Clamp(hotbarScale[i], 0.75f, 1f);

                float itrHotbarScale = hotbarScale[i];
                int num3 = (int)(20f + 22f * (1f - itrHotbarScale));
                // a = 230 if item is selected, a = 185 otherwise
                int a = (int)(75f + 150f * itrHotbarScale);
                Color lightColor = new Color(255, 255, 255, a);
                // if the hotbar is not locked AND the player input doesn't say to ignore the mouse interface AND the mouse is hovering this hotbar slot
                if (!player[myPlayer].hbLocked && mouseX >= num && mouseX <= num +
                    Terraria.GameContent.TextureAssets.InventoryBack.Width() * hotbarScale[i] && mouseY >= num3 && mouseY <= num3 + Terraria.GameContent.TextureAssets.InventoryBack.Height() * hotbarScale[i] && !player[myPlayer].channel)
                {
                    if (!PlayerInput.IgnoreMouseInterface)
                    {
                        player[myPlayer].mouseInterface = true;
                        player[myPlayer].cursorItemIconEnabled = false;
                    }

                    // NOTE: clicking on item is reflected one frame late
                    if (mouseLeft && mouseLeftRelease && !blockMouse)
                    {
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
            if (player[myPlayer].selectedItem >= 10 && (player[myPlayer].selectedItem != 58 || mouseItem.type > 0))
            {
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
    }
}
