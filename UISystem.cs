// STFU Microsoft
#pragma warning disable CA2211

using BetterGameUI.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using static BetterGameUI.MainReflection;
using static Terraria.Main;

namespace BetterGameUI
{
    public class UISystem : ModSystem
    {
        public static UserInterface UserInterfaceInventoryDownBuffsBar;
        public static UserInterface UserInterfaceMap;
        public static GameTime LastUpdateUIGameTime;

        public static UIInventoryDownBuffsBar UIInventoryDownBuffsBar =>
            UserInterfaceInventoryDownBuffsBar.CurrentState as UIInventoryDownBuffsBar;
        public static UIBasic UIMap => UserInterfaceMap.CurrentState as UIBasic;
        public static UIInventoryUpBuffsBar UIInventoryUpBuffsBar =>
            UserInterfaceMap.CurrentState.Children.First() as UIInventoryUpBuffsBar;

        public static void DrawItemSlot(SpriteBatch spriteBatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor = default(Color)) {
            Terraria.Player player = Main.player[Main.myPlayer];
            Item item = inv[slot];
            float inventoryScale = Main.inventoryScale;
            Color color = Color.White;
            if (lightColor != Color.Transparent)
                color = lightColor;

            bool flag = false;
            int num = 0;
            int gamepadPointForSlot = ItemSlotReflection.GetGamepadPointForSlot(inv, context, slot);
            if (PlayerInput.UsingGamepadUI) {
                flag = (UILinkPointNavigator.CurrentPoint == gamepadPointForSlot);
                if (PlayerInput.SettingsForUI.PreventHighlightsForGamepad)
                    flag = false;

                if (context == 0) {
                    num = player.DpadRadial.GetDrawMode(slot);
                    if (num > 0 && !PlayerInput.CurrentProfile.UsingDpadHotbar())
                        num = 0;
                }
            }

            Texture2D value = TextureAssets.InventoryBack.Value;
            Color color2 = Main.inventoryBack;
            bool flag2 = false;
            bool highlightThingsForMouse = PlayerInput.SettingsForUI.HighlightThingsForMouse;
            if (item.type > 0 && item.stack > 0 && item.favorited && context != 13 && context != 21 && context != 22 && context != 14) {
                value = TextureAssets.InventoryBack10.Value;
                if (context == 0/* && slot < 10*/ && player.selectedItem == slot) {
                    color2 = Color.White;
                    value = TextureAssets.InventoryBack17.Value;
                }
            }
            else if (item.type > 0 && item.stack > 0 && ItemSlot.Options.HighlightNewItems && item.newAndShiny && context != 13 && context != 21 && context != 14 && context != 22) {
                value = TextureAssets.InventoryBack15.Value;
                float num2 = (float)(int)Main.mouseTextColor / 255f;
                num2 = num2 * 0.2f + 0.8f;
                color2 = color2.MultiplyRGBA(new Color(num2, num2, num2));
            }
            else if (!highlightThingsForMouse && item.type > 0 && item.stack > 0 && num != 0 && context != 13 && context != 21 && context != 22) {
                value = TextureAssets.InventoryBack15.Value;
                float num3 = (float)(int)Main.mouseTextColor / 255f;
                num3 = num3 * 0.2f + 0.8f;
                color2 = ((num != 1) ? color2.MultiplyRGBA(new Color(num3 / 2f, num3, num3 / 2f)) : color2.MultiplyRGBA(new Color(num3, num3 / 2f, num3 / 2f)));
            }
            else if (context == 0) {
                if (slot < 10) {
                    value = TextureAssets.InventoryBack9.Value;
                }

                if (player.selectedItem == slot && highlightThingsForMouse) {
                    value = TextureAssets.InventoryBack14.Value;
                    color2 = Color.White;
                }
            }
            else {
                switch (context) {
                    case 28:
                        value = TextureAssets.InventoryBack7.Value;
                        color2 = Color.White;
                        break;
                    case 8:
                    case 10:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                        value = TextureAssets.InventoryBack3.Value;
                        break;
                    case 9:
                    case 11:
                    case 23:
                    case 24:
                    case 26:
                        value = TextureAssets.InventoryBack8.Value;
                        break;
                    case 12:
                    case 25:
                    case 27:
                        value = TextureAssets.InventoryBack12.Value;
                        break;
                    // These are added by TML.
                    case -10:
                    case -11:
                    case -12:
                        value = AccessorySlotLoaderReflection.GetBackgroundTexture(LoaderManager.Get<AccessorySlotLoader>(), slot, context);
                        break;
                    case 3:
                        value = TextureAssets.InventoryBack5.Value;
                        break;
                    case 4:
                        value = TextureAssets.InventoryBack2.Value;
                        break;
                    case 5:
                    case 7:
                        value = TextureAssets.InventoryBack4.Value;
                        break;
                    case 6:
                        value = TextureAssets.InventoryBack7.Value;
                        break;
                    case 13: {
                            byte b = 200;
                            if (slot == Main.player[Main.myPlayer].selectedItem) {
                                value = TextureAssets.InventoryBack14.Value;
                                b = byte.MaxValue;
                            }

                            color2 = new Color(b, b, b, b);
                            break;
                        }
                    case 14:
                    case 21:
                        flag2 = true;
                        break;
                    case 15:
                        value = TextureAssets.InventoryBack6.Value;
                        break;
                    case 29:
                        color2 = new Color(53, 69, 127, 255);
                        value = TextureAssets.InventoryBack18.Value;
                        break;
                    case 30:
                        flag2 = !flag;
                        break;
                    case 22:
                        value = TextureAssets.InventoryBack4.Value;
                        if (ItemSlot.DrawGoldBGForCraftingMaterial) {
                            ItemSlot.DrawGoldBGForCraftingMaterial = false;
                            value = TextureAssets.InventoryBack14.Value;
                            float num4 = (float)(int)color2.A / 255f;
                            num4 = ((!(num4 < 0.7f)) ? 1f : Utils.GetLerpValue(0f, 0.7f, num4, clamped: true));
                            color2 = Color.White * num4;
                        }
                        break;
                }
            }

            if ((context == 0 || context == 2) & ItemSlotReflection.GetInventoryGlowTime()[slot] > 0 && !inv[slot].favorited && !inv[slot].IsAir) {
                float scale = Main.invAlpha / 255f;
                Color value2 = new Color(63, 65, 151, 255) * scale;
                Color value3 = Main.hslToRgb(ItemSlotReflection.GetInventoryGlowHue()[slot], 1f, 0.5f) * scale;
                float num5 = (float)ItemSlotReflection.GetInventoryGlowTime()[slot] / 300f;
                num5 *= num5;
                color2 = Color.Lerp(value2, value3, num5 / 2f);
                value = TextureAssets.InventoryBack13.Value;
            }

            if ((context == 4 || context == 3) && ItemSlotReflection.GetInventoryGlowTimeChest()[slot] > 0 && !inv[slot].favorited && !inv[slot].IsAir) {
                float scale2 = Main.invAlpha / 255f;
                Color value4 = new Color(130, 62, 102, 255) * scale2;
                if (context == 3)
                    value4 = new Color(104, 52, 52, 255) * scale2;

                Color value5 = Main.hslToRgb(ItemSlotReflection.GetInventoryGlowHueChest()[slot], 1f, 0.5f) * scale2;
                float num6 = (float)ItemSlotReflection.GetInventoryGlowTimeChest()[slot] / 300f;
                num6 *= num6;
                color2 = Color.Lerp(value4, value5, num6 / 2f);
                value = TextureAssets.InventoryBack13.Value;
            }

            if (flag) {
                value = TextureAssets.InventoryBack14.Value;
                color2 = Color.White;
            }

            if (context == 28 && Main.MouseScreen.Between(position, position + value.Size() * inventoryScale) && !player.mouseInterface) {
                value = TextureAssets.InventoryBack14.Value;
                color2 = Color.White;
            }

            if (!flag2)
                spriteBatch.Draw(value, position, null, color2, 0f, default(Vector2), inventoryScale, SpriteEffects.None, 0f);

            int num7 = -1;
            switch (context) {
                case 8:
                case 23:
                    if (slot == 0)
                        num7 = 0;
                    if (slot == 1)
                        num7 = 6;
                    if (slot == 2)
                        num7 = 12;
                    break;
                case 26:
                    num7 = 0;
                    break;
                case 9:
                    if (slot == 10)
                        num7 = 3;
                    if (slot == 11)
                        num7 = 9;
                    if (slot == 12)
                        num7 = 15;
                    break;
                case 10:
                case 24:
                    num7 = 11;
                    break;
                case 11:
                    num7 = 2;
                    break;
                case 12:
                case 25:
                case 27:
                    num7 = 1;
                    break;
                // Added by TML: [[
                case -10:
                    num7 = 11;
                    break;
                case -11:
                    num7 = 2;
                    break;
                case -12:
                    num7 = 1;
                    break;
                // ]]
                case 16:
                    num7 = 4;
                    break;
                case 17:
                    num7 = 13;
                    break;
                case 19:
                    num7 = 10;
                    break;
                case 18:
                    num7 = 7;
                    break;
                case 20:
                    num7 = 17;
                    break;
            }

            if ((item.type <= 0 || item.stack <= 0) && num7 != -1) {
                Texture2D value6 = TextureAssets.Extra[54].Value;
                Rectangle rectangle = value6.Frame(3, 6, num7 % 3, num7 / 3);
                rectangle.Width -= 2;
                rectangle.Height -= 2;

                // Modded Accessory Slots
                if (context == -10 || context == -11 || context == -12) {
                    AccessorySlotLoaderReflection.DrawSlotTexture(LoaderManager.Get<AccessorySlotLoader>(), value6, position + value.Size() / 2f * inventoryScale, rectangle, Color.White * 0.35f, 0f, rectangle.Size() / 2f, inventoryScale, SpriteEffects.None, 0f, slot, context);

                    goto SkipVanillaDraw;
                }

                spriteBatch.Draw(value6, position + value.Size() / 2f * inventoryScale, rectangle, Color.White * 0.35f, 0f, rectangle.Size() / 2f, inventoryScale, SpriteEffects.None, 0f);

            SkipVanillaDraw:;
            }

            Vector2 vector = value.Size() * inventoryScale;
            if (item.type > 0 && item.stack > 0) {
                Main.instance.LoadItem(item.type);
                Texture2D value7 = TextureAssets.Item[item.type].Value;
                Rectangle rectangle2 = (Main.itemAnimations[item.type] == null) ? value7.Frame() : Main.itemAnimations[item.type].GetFrame(value7);
                Color currentColor = color;
                float scale3 = 1f;
                ItemSlot.GetItemLight(ref currentColor, ref scale3, item);
                float num8 = 1f;
                if (rectangle2.Width > 32 || rectangle2.Height > 32)
                    num8 = ((rectangle2.Width <= rectangle2.Height) ? (32f / (float)rectangle2.Height) : (32f / (float)rectangle2.Width));

                num8 *= inventoryScale;
                Vector2 position2 = position + vector / 2f - rectangle2.Size() * num8 / 2f;
                Vector2 origin = rectangle2.Size() * (scale3 / 2f - 0.5f);

                if (!ItemLoader.PreDrawInInventory(item, spriteBatch, position2, rectangle2, item.GetAlpha(currentColor), item.GetColor(color), origin, num8 * scale3))
                    goto SkipVanillaItemDraw;

                spriteBatch.Draw(value7, position2, rectangle2, item.GetAlpha(currentColor), 0f, origin, num8 * scale3, SpriteEffects.None, 0f);
                if (item.color != Color.Transparent) {
                    Color newColor = color;
                    if (context == 13)
                        newColor.A = byte.MaxValue;

                    // Extra context.

                    spriteBatch.Draw(value7, position2, rectangle2, item.GetColor(newColor), 0f, origin, num8 * scale3, SpriteEffects.None, 0f);
                }

            SkipVanillaItemDraw:
                ItemLoader.PostDrawInInventory(item, spriteBatch, position2, rectangle2, item.GetAlpha(currentColor), item.GetColor(color), origin, num8 * scale3);

                if (ItemID.Sets.TrapSigned[item.type])
                    spriteBatch.Draw(TextureAssets.Wire.Value, position + new Vector2(40f, 40f) * inventoryScale, new Rectangle(4, 58, 8, 8), color, 0f, new Vector2(4f), 1f, SpriteEffects.None, 0f);

                if (item.stack > 1)
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, item.stack.ToString(), position + new Vector2(10f, 26f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);

                int num9 = -1;
                if (context == 13) {
                    if (item.DD2Summon) {
                        for (int i = 0; i < 58; i++) {
                            if (inv[i].type == 3822)
                                num9 += inv[i].stack;
                        }

                        if (num9 >= 0)
                            num9++;
                    }

                    if (item.useAmmo > 0) {
                        int useAmmo = item.useAmmo;
                        num9 = 0;
                        for (int j = 0; j < 58; j++) {
                            if (inv[j].stack > 0 && ItemLoader.CanChooseAmmo(item, inv[j], player))
                                num9 += inv[j].stack;
                        }
                    }

                    if (item.fishingPole > 0) {
                        num9 = 0;
                        for (int k = 0; k < 58; k++) {
                            if (inv[k].bait > 0)
                                num9 += inv[k].stack;
                        }
                    }

                    if (item.tileWand > 0) {
                        int tileWand = item.tileWand;
                        num9 = 0;
                        for (int l = 0; l < 58; l++) {
                            if (inv[l].type == tileWand)
                                num9 += inv[l].stack;
                        }
                    }

                    if (item.type == 509 || item.type == 851 || item.type == 850 || item.type == 3612 || item.type == 3625 || item.type == 3611) {
                        num9 = 0;
                        for (int m = 0; m < 58; m++) {
                            if (inv[m].type == 530)
                                num9 += inv[m].stack;
                        }
                    }
                }

                if (num9 != -1)
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, num9.ToString(), position + new Vector2(8f, 30f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale * 0.8f), -1f, inventoryScale);

                if (context == 13) {
                    string text = (slot + 1).ToString() ?? "";
                    if (text == "10")
                        text = "0";

                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position + new Vector2(8f, 4f) * inventoryScale, color, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
                }

                if (context == 13 && item.potion) {
                    Vector2 position3 = position + value.Size() * inventoryScale / 2f - TextureAssets.Cd.Value.Size() * inventoryScale / 2f;
                    Color color3 = item.GetAlpha(color) * ((float)player.potionDelay / (float)player.potionDelayTime);
                    spriteBatch.Draw(TextureAssets.Cd.Value, position3, null, color3, 0f, default(Vector2), num8, SpriteEffects.None, 0f);

                    // Extra context.
                }

                if ((context == 10 || context == 18) && ((item.expertOnly && !Main.expertMode) || (item.masterOnly && !Main.masterMode))) {
                    Vector2 position4 = position + value.Size() * inventoryScale / 2f - TextureAssets.Cd.Value.Size() * inventoryScale / 2f;
                    Color white = Color.White;
                    spriteBatch.Draw(TextureAssets.Cd.Value, position4, null, white, 0f, default(Vector2), num8, SpriteEffects.None, 0f);
                }

                // Extra context.
            }
            else if (context == 6) {
                Texture2D value8 = TextureAssets.Trash.Value;
                Vector2 position5 = position + value.Size() * inventoryScale / 2f - value8.Size() * inventoryScale / 2f;
                spriteBatch.Draw(value8, position5, null, new Color(100, 100, 100, 100), 0f, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
            }

            if (context == 0 && slot < 10) {
                float num10 = inventoryScale;
                string text2 = (slot + 1).ToString() ?? "";
                if (text2 == "10")
                    text2 = "0";

                Color baseColor = Main.inventoryBack;
                int num11 = 0;
                if (Main.player[Main.myPlayer].selectedItem == slot) {
                    baseColor = Color.White;
                    baseColor.A = 200;
                    num11 -= 2;
                    num10 *= 1.4f;
                }

                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text2, position + new Vector2(6f, 4 + num11) * inventoryScale, baseColor, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
            }

            if (gamepadPointForSlot != -1)
                UILinkPointNavigator.SetPosition(gamepadPointForSlot, position + vector * 0.75f);
        }

        public static void DrawInventory() {
            Recipe.GetThroughDelayedFindRecipes();
            if (ShouldPVPDraw)
                DrawPVPIcons();

            int num = 0;
            int num2 = 0;
            int num3 = screenWidth;
            int num4 = 0;
            int num5 = screenWidth;
            int num6 = 0;
            Vector2 value = new Vector2(num, num2);
            new Vector2(num3, num4);
            new Vector2(num5, num6);
            DrawBestiaryIcon(num, num2);
            DrawEmoteBubblesButton(num, num2);
            DrawTrashItemSlot(num, num2);
            spriteBatch.DrawString(FontAssets.MouseText.Value, Lang.inter[4].Value, new Vector2(40f, 0f) + value, new Microsoft.Xna.Framework.Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            inventoryScale = 0.85f;
            if (mouseX > 20 && mouseX < (int)(20f + 560f * inventoryScale) && mouseY > 20 && mouseY < (int)(20f + 280f * inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                player[myPlayer].mouseInterface = true;

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 5; j++) {
                    int num7 = (int)(20f + (float)(i * 56) * inventoryScale) + num;
                    int num8 = (int)(20f + (float)(j * 56) * inventoryScale) + num2;
                    int num9 = i + j * 10;
                    new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                    if (mouseX >= num7 && (float)mouseX <= (float)num7 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num8 && (float)mouseY <= (float)num8 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        ItemSlot.OverrideHover(player[myPlayer].inventory, 0, num9);
                        if (player[myPlayer].inventoryChestStack[num9] && (player[myPlayer].inventory[num9].type == 0 || player[myPlayer].inventory[num9].stack == 0))
                            player[myPlayer].inventoryChestStack[num9] = false;

                        if (!player[myPlayer].inventoryChestStack[num9]) {
                            ItemSlot.LeftClick(player[myPlayer].inventory, 0, num9);
                            ItemSlot.RightClick(player[myPlayer].inventory, 0, num9);
                            if (mouseLeftRelease && mouseLeft)
                                Recipe.FindRecipes();
                        }

                        ItemSlot.MouseHover(player[myPlayer].inventory, 0, num9);
                    }

                    DrawItemSlot(spriteBatch, player[myPlayer].inventory, 0, num9, new Vector2(num7, num8));
                }
            }

            GetBuilderAccsCountToShow(LocalPlayer, out int _, out int _, out int totalDrawnIcons);
            bool pushSideToolsUp = totalDrawnIcons >= 10;
            if (!PlayerInput.UsingGamepad)
                DrawHotbarLockIcon(instance, num, num2, pushSideToolsUp);

            ItemSlot.DrawRadialDpad(spriteBatch, new Vector2(20f) + new Vector2(56f * inventoryScale * 10f, 56f * inventoryScale * 5f) + new Vector2(26f, 70f) + value);
            if ((achievementAdvisorInfo.GetValue(instance) as AchievementAdvisor).CanDrawAboveCoins) {
                int num10 = (int)(20f + 560f * inventoryScale) + num;
                int num11 = (int)(20f + 0f * inventoryScale) + num2;
                (achievementAdvisorInfo.GetValue(instance) as AchievementAdvisor).DrawOneAchievement(spriteBatch, new Vector2(num10, num11) + new Vector2(5f), large: true);
            }

            if (mapEnabled) {
                bool flag = false;
                int num12 = num3 - 440;
                int num13 = 40 + num4;
                if (screenWidth < 940)
                    flag = true;

                if (flag) {
                    num12 = num5 - 40;
                    num13 = num6 - 200;
                }

                int num14 = 0;
                for (int k = 0; k < 4; k++) {
                    int num16 = num12 + k * 32 - num14;
                    int num17 = num13;
                    if (flag) {
                        num16 = num12;
                        num17 = num13 + k * 32 - num14;
                    }

                    int num18 = k;
                    int num15 = 120;
                    if (k > 0 && mapStyle == k - 1)
                        num15 = 200;

                    if (mouseX >= num16 && mouseX <= num16 + 32 && mouseY >= num17 && mouseY <= num17 + 30 && !PlayerInput.IgnoreMouseInterface) {
                        num15 = 255;
                        num18 += 4;
                        player[myPlayer].mouseInterface = true;
                        if (mouseLeft && mouseLeftRelease) {
                            if (k == 0) {
                                playerInventory = false;
                                player[myPlayer].SetTalkNPC(-1);
                                npcChatCornerItem = 0;
                                SoundEngineReflection.PlaySound(10);
                                mapFullscreenScale = 2.5f;
                                mapFullscreen = true;
                                resetMapFull = true;
                            }

                            if (k == 1) {
                                mapStyle = 0;
                                SoundEngineReflection.PlaySound(12);
                            }

                            if (k == 2) {
                                mapStyle = 1;
                                SoundEngineReflection.PlaySound(12);
                            }

                            if (k == 3) {
                                mapStyle = 2;
                                SoundEngineReflection.PlaySound(12);
                            }
                        }
                    }

                    spriteBatch.Draw(TextureAssets.MapIcon[num18].Value, new Vector2(num16, num17), new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.MapIcon[num18].Width(), TextureAssets.MapIcon[num18].Height()), new Microsoft.Xna.Framework.Color(num15, num15, num15, num15), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
            }

            if (armorHide) {
                armorAlpha -= 0.1f;
                if (armorAlpha < 0f)
                    armorAlpha = 0f;
            }
            else {
                armorAlpha += 0.025f;
                if (armorAlpha > 1f)
                    armorAlpha = 1f;
            }

            new Microsoft.Xna.Framework.Color((byte)((float)(int)mouseTextColor * armorAlpha), (byte)((float)(int)mouseTextColor * armorAlpha), (byte)((float)(int)mouseTextColor * armorAlpha), (byte)((float)(int)mouseTextColor * armorAlpha));
            armorHide = false;
            int num19 = 8 + player[myPlayer].GetAmountOfExtraAccessorySlotsToShow();
            int num20 = 174 + (int)mapHeight.GetValue(null);
            int num21 = 950;
            cannotDrawAccessoriesHorizontally.SetValue(null, false);
            if (screenHeight < num21 && num19 >= 10) {
                num20 -= (int)(56f * inventoryScale * (float)(num19 - 9));
                cannotDrawAccessoriesHorizontally.SetValue(null, true);
            }

            int num22 = DrawPageIcons(num20 - 32);
            if (num22 > -1) {
                HoverItem = new Item();
                switch (num22) {
                    case 1:
                        hoverItemName = Lang.inter[80].Value;
                        break;

                    case 2:
                        hoverItemName = Lang.inter[79].Value;
                        break;

                    case 3:
                        hoverItemName = (CaptureModeDisabled ? Lang.inter[115].Value : Lang.inter[81].Value);
                        break;
                }
            }

            if (EquipPage == 2) {
                Microsoft.Xna.Framework.Point value2 = new Microsoft.Xna.Framework.Point(mouseX, mouseY);
                Microsoft.Xna.Framework.Rectangle r = new Microsoft.Xna.Framework.Rectangle(0, 0, (int)((float)TextureAssets.InventoryBack.Width() * inventoryScale), (int)((float)TextureAssets.InventoryBack.Height() * inventoryScale));
                Item[] inv = player[myPlayer].miscEquips;
                int num23 = screenWidth - 92;
                int num24 = (int)mapHeight.GetValue(null) + 174;
                for (int l = 0; l < 2; l++) {
                    switch (l) {
                        case 0:
                            inv = player[myPlayer].miscEquips;
                            break;

                        case 1:
                            inv = player[myPlayer].miscDyes;
                            break;
                    }

                    r.X = num23 + l * -47;
                    for (int m = 0; m < 5; m++) {
                        int context = 0;
                        int num25 = -1;
                        switch (m) {
                            case 0:
                                context = 19;
                                num25 = 0;
                                break;

                            case 1:
                                context = 20;
                                num25 = 1;
                                break;

                            case 2:
                                context = 18;
                                break;

                            case 3:
                                context = 17;
                                break;

                            case 4:
                                context = 16;
                                break;
                        }

                        if (l == 1) {
                            context = 12;
                            num25 = -1;
                        }

                        r.Y = num24 + m * 47;
                        Texture2D value3 = TextureAssets.InventoryTickOn.Value;
                        if (player[myPlayer].hideMisc[num25])
                            value3 = TextureAssets.InventoryTickOff.Value;

                        Microsoft.Xna.Framework.Rectangle r2 = new Microsoft.Xna.Framework.Rectangle(r.Left + 34, r.Top - 2, value3.Width, value3.Height);
                        int num26 = 0;
                        bool flag2 = false;
                        if (r2.Contains(value2) && !PlayerInput.IgnoreMouseInterface) {
                            player[myPlayer].mouseInterface = true;
                            flag2 = true;
                            if (mouseLeft && mouseLeftRelease) {
                                if (num25 == 0)
                                    player[myPlayer].TogglePet();

                                if (num25 == 1)
                                    player[myPlayer].ToggleLight();

                                mouseLeftRelease = false;
                                SoundEngineReflection.PlaySound(12);
                                if (netMode == 1)
                                    NetMessage.SendData(4, -1, -1, null, myPlayer);
                            }

                            num26 = ((!player[myPlayer].hideMisc[num25]) ? 1 : 2);
                        }

                        if (r.Contains(value2) && !flag2 && !PlayerInput.IgnoreMouseInterface) {
                            player[myPlayer].mouseInterface = true;
                            armorHide = true;
                            ItemSlot.Handle(inv, context, m);
                        }

                        ItemSlot.Draw(spriteBatch, inv, context, m, r.TopLeft());
                        if (num25 != -1) {
                            spriteBatch.Draw(value3, r2.TopLeft(), Microsoft.Xna.Framework.Color.White * 0.7f);
                            if (num26 > 0) {
                                HoverItem = new Item();
                                hoverItemName = Lang.inter[58 + num26].Value;
                            }
                        }
                    }
                }
            }
            else if (EquipPage == 1) {
                DrawNPCHousesInUI(instance);
            }
            else if (EquipPage == 0) {// allow mods to add custom equip pages
                int num35 = 4;
                if (mouseX > screenWidth - 64 - 28 && mouseX < (int)((float)(screenWidth - 64 - 28) + 56f * inventoryScale) && mouseY > num20 && mouseY < (int)((float)num20 + 448f * inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                    player[myPlayer].mouseInterface = true;

                float num36 = inventoryScale;
                bool flag3 = false;
                int num37 = num19 - 1;
                bool flag4 = LocalPlayer.CanDemonHeartAccessoryBeShown();
                bool flag5 = LocalPlayer.CanMasterModeAccessoryBeShown();
                if ((bool)settingsButtonIsPushedToSide.GetValue(null))
                    num37--;

                int num38 = num37 - 1;
                Microsoft.Xna.Framework.Color color = inventoryBack;
                Microsoft.Xna.Framework.Color color2 = new Microsoft.Xna.Framework.Color(80, 80, 80, 80);
                int num39 = -1;
                for (int num40 = 0; num40 < 3; num40++) {
                    if ((num40 == 8 && !flag4) || (num40 == 9 && !flag5))
                        continue;

                    num39++;
                    bool flag6 = LocalPlayer.IsAValidEquipmentSlotForIteration(num40);
                    if (!flag6)
                        flag3 = true;

                    int num41 = screenWidth - 64 - 28;
                    int num42 = (int)((float)num20 + (float)(num39 * 56) * inventoryScale);
                    new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                    int num43 = screenWidth - 58;
                    int num44 = (int)((float)(num20 - 2) + (float)(num39 * 56) * inventoryScale);
                    int context2 = 8;
                    if (num40 > 2) {
                        num42 += num35;
                        num44 += num35;
                        context2 = 10;
                    }

                    /*
                    if (num39 == num38 && !(achievementAdvisorInfo.GetValue(instance) as AchievementAdvisor).CanDrawAboveCoins) {
                        (achievementAdvisorInfo.GetValue(instance) as AchievementAdvisor).DrawOneAchievement(spriteBatch, new Vector2(num41 - 10 - 47 - 47 - 14 - 14, num42 + 8), large: false);
                        UILinkPointNavigator.SetPosition(1570, new Vector2(num41 - 10 - 47 - 47 - 14 - 14, num42 + 8) + new Vector2(20f) * inventoryScale);
                    }

                    if (num39 == num37)
                        DrawDefenseCounter(num41, num42);
                    */

                    Texture2D value4 = TextureAssets.InventoryTickOn.Value;
                    if (player[myPlayer].hideVisibleAccessory[num40])
                        value4 = TextureAssets.InventoryTickOff.Value;

                    Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(num43, num44, value4.Width, value4.Height);
                    int num45 = 0;
                    if (num40 > 2 && rectangle.Contains(new Microsoft.Xna.Framework.Point(mouseX, mouseY)) && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        if (mouseLeft && mouseLeftRelease) {
                            player[myPlayer].hideVisibleAccessory[num40] = !player[myPlayer].hideVisibleAccessory[num40];
                            SoundEngineReflection.PlaySound(12);
                            if (netMode == 1)
                                NetMessage.SendData(4, -1, -1, null, myPlayer);
                        }

                        num45 = ((!player[myPlayer].hideVisibleAccessory[num40]) ? 1 : 2);
                    }
                    else if (mouseX >= num41 && (float)mouseX <= (float)num41 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num42 && (float)mouseY <= (float)num42 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                        armorHide = true;
                        player[myPlayer].mouseInterface = true;
                        ItemSlot.OverrideHover(player[myPlayer].armor, context2, num40);
                        if (flag6 || mouseItem.IsAir)
                            ItemSlot.LeftClick(player[myPlayer].armor, context2, num40);

                        ItemSlot.MouseHover(player[myPlayer].armor, context2, num40);
                    }

                    if (flag3)
                        inventoryBack = color2;

                    ItemSlot.Draw(spriteBatch, player[myPlayer].armor, context2, num40, new Vector2(num41, num42));
                    if (num40 > 2) {
                        spriteBatch.Draw(value4, new Vector2(num43, num44), Microsoft.Xna.Framework.Color.White * 0.7f);
                        if (num45 > 0) {
                            HoverItem = new Item();
                            hoverItemName = Lang.inter[58 + num45].Value;
                        }
                    }
                }

                inventoryBack = color;
                if (mouseX > screenWidth - 64 - 28 - 47 && mouseX < (int)((float)(screenWidth - 64 - 20 - 47) + 56f * inventoryScale) && mouseY > num20 && mouseY < (int)((float)num20 + 168f * inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                    player[myPlayer].mouseInterface = true;

                num39 = -1;
                for (int num46 = 10; num46 < 13; num46++) {
                    if ((num46 == 18 && !flag4) || (num46 == 19 && !flag5))
                        continue;

                    num39++;
                    bool num47 = LocalPlayer.IsAValidEquipmentSlotForIteration(num46);
                    flag3 = !num47;
                    bool flag7 = !num47 && !mouseItem.IsAir;
                    int num48 = screenWidth - 64 - 28 - 47;
                    int num49 = (int)((float)num20 + (float)(num39 * 56) * inventoryScale);
                    new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                    if (num46 > 12)
                        num49 += num35;

                    int context3 = 9;
                    if (num46 > 12)
                        context3 = 11;

                    if (mouseX >= num48 && (float)mouseX <= (float)num48 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num49 && (float)mouseY <= (float)num49 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        armorHide = true;
                        ItemSlot.OverrideHover(player[myPlayer].armor, context3, num46);
                        if (!flag7) {
                            ItemSlot.LeftClick(player[myPlayer].armor, context3, num46);
                            ItemSlot.RightClick(player[myPlayer].armor, context3, num46);
                        }

                        ItemSlot.MouseHover(player[myPlayer].armor, context3, num46);
                    }

                    if (flag3)
                        inventoryBack = color2;

                    ItemSlot.Draw(spriteBatch, player[myPlayer].armor, context3, num46, new Vector2(num48, num49));
                }

                inventoryBack = color;
                if (mouseX > screenWidth - 64 - 28 - 47 && mouseX < (int)((float)(screenWidth - 64 - 20 - 47) + 56f * inventoryScale) && mouseY > num20 && mouseY < (int)((float)num20 + 168f * inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                    player[myPlayer].mouseInterface = true;

                num39 = -1;
                for (int num50 = 0; num50 < 3; num50++) {
                    if ((num50 == 8 && !flag4) || (num50 == 9 && !flag5))
                        continue;

                    num39++;
                    bool num51 = LocalPlayer.IsAValidEquipmentSlotForIteration(num50);
                    flag3 = !num51;
                    bool flag8 = !num51 && !mouseItem.IsAir;
                    int num52 = screenWidth - 64 - 28 - 47 - 47;
                    int num53 = (int)((float)num20 + (float)(num39 * 56) * inventoryScale);
                    new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                    if (num50 > 2)
                        num53 += num35;

                    if (mouseX >= num52 && (float)mouseX <= (float)num52 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num53 && (float)mouseY <= (float)num53 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        armorHide = true;
                        ItemSlot.OverrideHover(player[myPlayer].dye, 12, num50);
                        if (!flag8) {
                            if (mouseRightRelease && mouseRight)
                                ItemSlot.RightClick(player[myPlayer].dye, 12, num50);

                            ItemSlot.LeftClick(player[myPlayer].dye, 12, num50);
                        }

                        ItemSlot.MouseHover(player[myPlayer].dye, 12, num50);
                    }

                    if (flag3)
                        inventoryBack = color2;

                    ItemSlot.Draw(spriteBatch, player[myPlayer].dye, 12, num50, new Vector2(num52, num53));
                }

                inventoryBack = color;

                var defPos = AccessorySlotLoader.DefenseIconPosition;
                DrawDefenseCounter((int)defPos.X, (int)defPos.Y);

                if (!(achievementAdvisorInfo.GetValue(instance) as AchievementAdvisor).CanDrawAboveCoins) {
                    var achievePos = new Vector2(defPos.X - 10 - 47 - 47 - 14 - 14, defPos.Y - 56 * inventoryScale * 0.5f);
                    (achievementAdvisorInfo.GetValue(instance) as AchievementAdvisor).DrawOneAchievement(spriteBatch, achievePos, large: false);
                    UILinkPointNavigator.SetPosition(1570, achievePos + new Vector2(20f) * inventoryScale);
                }

                inventoryBack = color;
                inventoryScale = num36;
            }

            LoaderManager.Get<AccessorySlotLoader>().DrawAccSlots(num20);

            int num54 = (screenHeight - 600) / 2;
            int num55 = (int)((float)screenHeight / 600f * 250f);
            if (screenHeight < 700) {
                num54 = (screenHeight - 508) / 2;
                num55 = (int)((float)screenHeight / 600f * 200f);
            }
            else if (screenHeight < 850) {
                num55 = (int)((float)screenHeight / 600f * 225f);
            }

            if (craftingHide) {
                craftingAlpha -= 0.1f;
                if (craftingAlpha < 0f)
                    craftingAlpha = 0f;
            }
            else {
                craftingAlpha += 0.025f;
                if (craftingAlpha > 1f)
                    craftingAlpha = 1f;
            }

            Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color((byte)((float)(int)mouseTextColor * craftingAlpha), (byte)((float)(int)mouseTextColor * craftingAlpha), (byte)((float)(int)mouseTextColor * craftingAlpha), (byte)((float)(int)mouseTextColor * craftingAlpha));
            craftingHide = false;
            if (InReforgeMenu) {
                if (mouseReforge) {
                    if (reforgeScale < 1f)
                        reforgeScale += 0.02f;
                }
                else if (reforgeScale > 1f) {
                    reforgeScale -= 0.02f;
                }

                if (player[myPlayer].chest != -1 || npcShop != 0 || player[myPlayer].talkNPC == -1 || InGuideCraftMenu) {
                    InReforgeMenu = false;
                    player[myPlayer].dropItemCheck();
                    Recipe.FindRecipes();
                }
                else {
                    int num56 = 50;
                    int num57 = 270;
                    string text = Lang.inter[46].Value + ": ";
                    if (reforgeItem.type > 0) {
                        //int num58 = reforgeItem.value;
                        int num58 = reforgeItem.value * reforgeItem.stack; // TML: #StackablePrefixWeapons: scale with current stack size
                        bool canApplyDiscount = true;
                        if (!ItemLoader.ReforgePrice(reforgeItem, ref num58, ref canApplyDiscount))
                            goto skipVanillaPricing;

                        if (canApplyDiscount && LocalPlayer.discount)
                            num58 = (int)((double)num58 * 0.8);

                        num58 = (int)((double)num58 * player[myPlayer].currentShoppingSettings.PriceAdjustment);
                        num58 /= 3;

                    skipVanillaPricing:
                        string text2 = "";
                        int num59 = 0;
                        int num60 = 0;
                        int num61 = 0;
                        int num62 = 0;
                        int num63 = num58;
                        if (num63 < 1)
                            num63 = 1;

                        if (num63 >= 1000000) {
                            num59 = num63 / 1000000;
                            num63 -= num59 * 1000000;
                        }

                        if (num63 >= 10000) {
                            num60 = num63 / 10000;
                            num63 -= num60 * 10000;
                        }

                        if (num63 >= 100) {
                            num61 = num63 / 100;
                            num63 -= num61 * 100;
                        }

                        if (num63 >= 1)
                            num62 = num63;

                        if (num59 > 0)
                            text2 = text2 + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + num59 + " " + Lang.inter[15].Value + "] ";

                        if (num60 > 0)
                            text2 = text2 + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + num60 + " " + Lang.inter[16].Value + "] ";

                        if (num61 > 0)
                            text2 = text2 + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + num61 + " " + Lang.inter[17].Value + "] ";

                        if (num62 > 0)
                            text2 = text2 + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + num62 + " " + Lang.inter[18].Value + "] ";

                        ItemSlot.DrawSavings(spriteBatch, num56 + 130, instance.invBottom, horizontal: true);
                        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, text2, new Vector2((float)(num56 + 50) + FontAssets.MouseText.Value.MeasureString(text).X, num57), Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, Vector2.One);
                        int num64 = num56 + 70;
                        int num65 = num57 + 40;
                        bool num66 = mouseX > num64 - 15 && mouseX < num64 + 15 && mouseY > num65 - 15 && mouseY < num65 + 15 && !PlayerInput.IgnoreMouseInterface;
                        Texture2D value5 = TextureAssets.Reforge[0].Value;
                        if (num66)
                            value5 = TextureAssets.Reforge[1].Value;

                        spriteBatch.Draw(value5, new Vector2(num64, num65), null, Microsoft.Xna.Framework.Color.White, 0f, value5.Size() / 2f, reforgeScale, SpriteEffects.None, 0f);
                        UILinkPointNavigator.SetPosition(304, new Vector2(num64, num65) + value5.Size() / 4f);
                        if (num66) {
                            hoverItemName = Lang.inter[19].Value;
                            if (!mouseReforge)
                                SoundEngineReflection.PlaySound(12);

                            mouseReforge = true;
                            player[myPlayer].mouseInterface = true;
                            if (mouseLeftRelease && mouseLeft && player[myPlayer].CanBuyItem(num58) && ItemLoader.PreReforge(reforgeItem)) {
                                player[myPlayer].BuyItem(num58);
                                bool favorited = reforgeItem.favorited;
                                int stack = reforgeItem.stack;  //#StackablePrefixWeapons: keep the stack, (i.e. light discs)
                                                                //vanilla doesn't have a good way of resetting prefixes, so it creates a new item entirely
                                                                //before we roll the prefix, we simply copy over the old mod data before doing so
                                Item r = new Item();
                                r.netDefaults(reforgeItem.netID);
                                r = r.CloneWithModdedDataFrom(reforgeItem);
                                r.Prefix(-2);
                                reforgeItem = r.Clone();
                                reforgeItem.position.X = player[myPlayer].position.X + (float)(player[myPlayer].width / 2) - (float)(reforgeItem.width / 2);
                                reforgeItem.position.Y = player[myPlayer].position.Y + (float)(player[myPlayer].height / 2) - (float)(reforgeItem.height / 2);
                                reforgeItem.favorited = favorited;
                                reforgeItem.stack = stack;
                                ItemLoader.PostReforge(reforgeItem);
                                PopupText.NewText(PopupTextContext.ItemReforge, reforgeItem, reforgeItem.stack, noStack: true);
                                SoundEngine.PlaySound(SoundID.Item37);
                            }
                        }
                        else {
                            mouseReforge = false;
                        }
                    }
                    else {
                        text = Lang.inter[20].Value;
                    }

                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, text, new Vector2(num56 + 50, num57), new Microsoft.Xna.Framework.Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, Vector2.Zero, Vector2.One);
                    if (mouseX >= num56 && (float)mouseX <= (float)num56 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num57 && (float)mouseY <= (float)num57 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        craftingHide = true;
                        ItemSlot.LeftClick(ref reforgeItem, 5);
                        if (mouseLeftRelease && mouseLeft)
                            Recipe.FindRecipes();

                        ItemSlot.RightClick(ref reforgeItem, 5);
                        ItemSlot.MouseHover(ref reforgeItem, 5);
                    }

                    ItemSlot.Draw(spriteBatch, ref reforgeItem, 5, new Vector2(num56, num57));
                }
            }
            else if (InGuideCraftMenu) {
                if (player[myPlayer].chest != -1 || npcShop != 0 || player[myPlayer].talkNPC == -1 || InReforgeMenu) {
                    InGuideCraftMenu = false;
                    player[myPlayer].dropItemCheck();
                    Recipe.FindRecipes();
                }
                else {
                    DrawGuideCraftText(num54, color3, out int inventoryX, out int inventoryY);
                    new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                    if (mouseX >= inventoryX && (float)mouseX <= (float)inventoryX + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= inventoryY && (float)mouseY <= (float)inventoryY + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        craftingHide = true;
                        ItemSlot.OverrideHover(ref guideItem, 7);
                        ItemSlot.LeftClick(ref guideItem, 7);
                        if (mouseLeftRelease && mouseLeft)
                            Recipe.FindRecipes();

                        ItemSlot.RightClick(ref guideItem, 7);
                        ItemSlot.MouseHover(ref guideItem, 7);
                    }

                    //Extra patch context.
                    ItemSlot.Draw(spriteBatch, ref guideItem, 7, new Vector2(inventoryX, inventoryY));
                }
            }

            CreativeMenu.Draw(spriteBatch);
            bool flag9 = CreativeMenu.Enabled && !CreativeMenu.Blocked;
            if (!InReforgeMenu && !hidePlayerCraftingMenu && !LocalPlayer.tileEntityAnchor.InUse && !flag9) {
                UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = -1;
                UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;
                if (numAvailableRecipes > 0)
                    spriteBatch.DrawString(FontAssets.MouseText.Value, Lang.inter[25].Value, new Vector2(76f, 414 + num54), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

                for (int num67 = 0; num67 < Recipe.maxRecipes; num67++) {
                    inventoryScale = 100f / (Math.Abs(availableRecipeY[num67]) + 100f);
                    if ((double)inventoryScale < 0.75)
                        inventoryScale = 0.75f;

                    if (recFastScroll)
                        inventoryScale = 0.75f;

                    if (availableRecipeY[num67] < (float)((num67 - focusRecipe) * 65)) {
                        if (availableRecipeY[num67] == 0f && !recFastScroll)
                            SoundEngineReflection.PlaySound(12);

                        availableRecipeY[num67] += 6.5f;
                        if (recFastScroll)
                            availableRecipeY[num67] += 130000f;

                        if (availableRecipeY[num67] > (float)((num67 - focusRecipe) * 65))
                            availableRecipeY[num67] = (num67 - focusRecipe) * 65;
                    }
                    else if (availableRecipeY[num67] > (float)((num67 - focusRecipe) * 65)) {
                        if (availableRecipeY[num67] == 0f && !recFastScroll)
                            SoundEngineReflection.PlaySound(12);

                        availableRecipeY[num67] -= 6.5f;
                        if (recFastScroll)
                            availableRecipeY[num67] -= 130000f;

                        if (availableRecipeY[num67] < (float)((num67 - focusRecipe) * 65))
                            availableRecipeY[num67] = (num67 - focusRecipe) * 65;
                    }
                    else {
                        recFastScroll = false;
                    }

                    if (num67 >= numAvailableRecipes || Math.Abs(availableRecipeY[num67]) > (float)num55)
                        continue;

                    int num68 = (int)(46f - 26f * inventoryScale);
                    int num69 = (int)(410f + availableRecipeY[num67] * inventoryScale - 30f * inventoryScale + (float)num54);
                    double num70 = inventoryBack.A + 50;
                    double num71 = 255.0;
                    if (Math.Abs(availableRecipeY[num67]) > (float)num55 - 100f) {
                        num70 = (double)(150f * (100f - (Math.Abs(availableRecipeY[num67]) - ((float)num55 - 100f)))) * 0.01;
                        num71 = (double)(255f * (100f - (Math.Abs(availableRecipeY[num67]) - ((float)num55 - 100f)))) * 0.01;
                    }

                    new Microsoft.Xna.Framework.Color((byte)num70, (byte)num70, (byte)num70, (byte)num70);
                    Microsoft.Xna.Framework.Color lightColor = new Microsoft.Xna.Framework.Color((byte)num71, (byte)num71, (byte)num71, (byte)num71);
                    if (!LocalPlayer.creativeInterface && mouseX >= num68 && (float)mouseX <= (float)num68 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num69 && (float)mouseY <= (float)num69 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface)
                        HoverOverCraftingItemButton(num67);

                    if (numAvailableRecipes <= 0)
                        continue;

                    num70 -= 50.0;
                    if (num70 < 0.0)
                        num70 = 0.0;

                    if (num67 == focusRecipe) {
                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = 0;
                        if (PlayerInput.SettingsForUI.HighlightThingsForMouse)
                            ItemSlot.DrawGoldBGForCraftingMaterial = true;
                    }
                    else {
                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;
                    }

                    Microsoft.Xna.Framework.Color color4 = inventoryBack;
                    inventoryBack = new Microsoft.Xna.Framework.Color((byte)num70, (byte)num70, (byte)num70, (byte)num70);
                    ItemSlot.Draw(spriteBatch, ref recipe[availableRecipe[num67]].createItem, 22, new Vector2(num68, num69), lightColor);
                    inventoryBack = color4;
                }

                if (numAvailableRecipes > 0) {
                    UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = -1;
                    UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;
                    for (int num72 = 0; num72 < recipe[availableRecipe[focusRecipe]].requiredItem.Count; num72++) {
                        if (recipe[availableRecipe[focusRecipe]].requiredItem[num72].type == 0) {
                            UILinkPointNavigator.Shortcuts.CRAFT_CurrentIngredientsCount = num72 + 1;
                            break;
                        }

                        int num73 = 80 + num72 * 40;
                        int num74 = 380 + num54;
                        double num75 = inventoryBack.A + 50;
                        double num76 = 255.0;
                        Microsoft.Xna.Framework.Color white = Microsoft.Xna.Framework.Color.White;
                        Microsoft.Xna.Framework.Color white2 = Microsoft.Xna.Framework.Color.White;
                        num75 = (float)(inventoryBack.A + 50) - Math.Abs(availableRecipeY[focusRecipe]) * 2f;
                        num76 = 255f - Math.Abs(availableRecipeY[focusRecipe]) * 2f;
                        if (num75 < 0.0)
                            num75 = 0.0;

                        if (num76 < 0.0)
                            num76 = 0.0;

                        white.R = (byte)num75;
                        white.G = (byte)num75;
                        white.B = (byte)num75;
                        white.A = (byte)num75;
                        white2.R = (byte)num76;
                        white2.G = (byte)num76;
                        white2.B = (byte)num76;
                        white2.A = (byte)num76;
                        inventoryScale = 0.6f;
                        if (num75 == 0.0)
                            break;

                        if (mouseX >= num73 && (float)mouseX <= (float)num73 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num74 && (float)mouseY <= (float)num74 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                            craftingHide = true;
                            player[myPlayer].mouseInterface = true;
                            SetRecipeMaterialDisplayName(num72);
                        }

                        num75 -= 50.0;
                        if (num75 < 0.0)
                            num75 = 0.0;

                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = 1 + num72;
                        Microsoft.Xna.Framework.Color color5 = inventoryBack;
                        inventoryBack = new Microsoft.Xna.Framework.Color((byte)num75, (byte)num75, (byte)num75, (byte)num75);
                        Item tempItem = recipe[availableRecipe[focusRecipe]].requiredItem[num72];
                        ItemSlot.Draw(spriteBatch, ref tempItem, 22, new Vector2(num73, num74));
                        inventoryBack = color5;
                    }
                }

                if (numAvailableRecipes == 0) {
                    recBigList = false;
                }
                else {
                    int num77 = 94;
                    int num78 = 450 + num54;
                    if (InGuideCraftMenu)
                        num78 -= 150;

                    bool flag10 = mouseX > num77 - 15 && mouseX < num77 + 15 && mouseY > num78 - 15 && mouseY < num78 + 15 && !PlayerInput.IgnoreMouseInterface;
                    int num79 = recBigList.ToInt() * 2 + flag10.ToInt();
                    spriteBatch.Draw(TextureAssets.CraftToggle[num79].Value, new Vector2(num77, num78), null, Microsoft.Xna.Framework.Color.White, 0f, TextureAssets.CraftToggle[num79].Value.Size() / 2f, 1f, SpriteEffects.None, 0f);
                    if (flag10) {
                        instance.MouseText(Language.GetTextValue("GameUI.CraftingWindow"), 0, 0);
                        player[myPlayer].mouseInterface = true;
                        if (mouseLeft && mouseLeftRelease) {
                            if (!recBigList) {
                                recBigList = true;
                                SoundEngineReflection.PlaySound(12);
                            }
                            else {
                                recBigList = false;
                                SoundEngineReflection.PlaySound(12);
                            }
                        }
                    }
                }
            }
            hidePlayerCraftingMenu = false;

            if (recBigList && !flag9) {
                UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = -1;
                UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;
                int num80 = 42;
                if ((double)inventoryScale < 0.75)
                    inventoryScale = 0.75f;

                int num81 = 340;
                int num82 = 310;
                int num83 = (screenWidth - num82 - 280) / num80;
                int num84 = (screenHeight - num81 - 20) / num80;
                UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow = num83;
                UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn = num84;
                int num85 = 0;
                int num86 = 0;
                int num87 = num82;
                int num88 = num81;
                int num89 = num82 - 20;
                int num90 = num81 + 2;
                if (recStart > numAvailableRecipes - num83 * num84) {
                    recStart = numAvailableRecipes - num83 * num84;
                    if (recStart < 0)
                        recStart = 0;
                }

                if (recStart > 0) {
                    if (mouseX >= num89 && mouseX <= num89 + TextureAssets.CraftUpButton.Width() && mouseY >= num90 && mouseY <= num90 + TextureAssets.CraftUpButton.Height() && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        if (mouseLeftRelease && mouseLeft) {
                            recStart -= num83;
                            if (recStart < 0)
                                recStart = 0;

                            SoundEngineReflection.PlaySound(12);
                            mouseLeftRelease = false;
                        }
                    }

                    spriteBatch.Draw(TextureAssets.CraftUpButton.Value, new Vector2(num89, num90), new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.CraftUpButton.Width(), TextureAssets.CraftUpButton.Height()), new Microsoft.Xna.Framework.Color(200, 200, 200, 200), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }

                if (recStart < numAvailableRecipes - num83 * num84) {
                    num90 += 20;
                    if (mouseX >= num89 && mouseX <= num89 + TextureAssets.CraftUpButton.Width() && mouseY >= num90 && mouseY <= num90 + TextureAssets.CraftUpButton.Height() && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        if (mouseLeftRelease && mouseLeft) {
                            recStart += num83;
                            SoundEngineReflection.PlaySound(12);
                            if (recStart > numAvailableRecipes - num83)
                                recStart = numAvailableRecipes - num83;

                            mouseLeftRelease = false;
                        }
                    }

                    spriteBatch.Draw(TextureAssets.CraftDownButton.Value, new Vector2(num89, num90), new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.CraftUpButton.Width(), TextureAssets.CraftUpButton.Height()), new Microsoft.Xna.Framework.Color(200, 200, 200, 200), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }

                for (int num91 = recStart; num91 < Recipe.maxRecipes && num91 < numAvailableRecipes; num91++) {
                    int num92 = num87;
                    int num93 = num88;
                    double num94 = inventoryBack.A + 50;
                    double num95 = 255.0;
                    new Microsoft.Xna.Framework.Color((byte)num94, (byte)num94, (byte)num94, (byte)num94);
                    new Microsoft.Xna.Framework.Color((byte)num95, (byte)num95, (byte)num95, (byte)num95);
                    if (mouseX >= num92 && (float)mouseX <= (float)num92 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num93 && (float)mouseY <= (float)num93 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        if (mouseLeftRelease && mouseLeft) {
                            focusRecipe = num91;
                            recFastScroll = true;
                            recBigList = false;
                            SoundEngineReflection.PlaySound(12);
                            mouseLeftRelease = false;
                            if (PlayerInput.UsingGamepadUI) {
                                UILinkPointNavigator.ChangePage(9);
                                LockCraftingForThisCraftClickDuration();
                            }
                        }

                        craftingHide = true;
                        hoverItemName = recipe[availableRecipe[num91]].createItem.Name;
                        HoverItem = recipe[availableRecipe[num91]].createItem.Clone();
                        if (recipe[availableRecipe[num91]].createItem.stack > 1)
                            hoverItemName = hoverItemName + " (" + recipe[availableRecipe[num91]].createItem.stack + ")";
                    }

                    if (numAvailableRecipes > 0) {
                        num94 -= 50.0;
                        if (num94 < 0.0)
                            num94 = 0.0;

                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = num91 - recStart;
                        Microsoft.Xna.Framework.Color color6 = inventoryBack;
                        inventoryBack = new Microsoft.Xna.Framework.Color((byte)num94, (byte)num94, (byte)num94, (byte)num94);
                        ItemSlot.Draw(spriteBatch, ref recipe[availableRecipe[num91]].createItem, 22, new Vector2(num92, num93));
                        inventoryBack = color6;
                    }

                    num87 += num80;
                    num85++;
                    if (num85 >= num83) {
                        num87 = num82;
                        num88 += num80;
                        num85 = 0;
                        num86++;
                        if (num86 >= num84)
                            break;
                    }
                }
            }

            Vector2 vector = FontAssets.MouseText.Value.MeasureString("Coins");
            Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(Lang.inter[26].Value);
            float num96 = vector.X / vector2.X;
            spriteBatch.DrawString(FontAssets.MouseText.Value, Lang.inter[26].Value, new Vector2(496f, 84f + (vector.Y - vector.Y * num96) / 2f), new Microsoft.Xna.Framework.Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, default(Vector2), 0.75f * num96, SpriteEffects.None, 0f);
            inventoryScale = 0.6f;
            for (int num97 = 0; num97 < 4; num97++) {
                int num98 = 497;
                int num99 = (int)(85f + (float)(num97 * 56) * inventoryScale + 20f);
                int slot = num97 + 50;
                new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                if (mouseX >= num98 && (float)mouseX <= (float)num98 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num99 && (float)mouseY <= (float)num99 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                    player[myPlayer].mouseInterface = true;
                    ItemSlot.OverrideHover(player[myPlayer].inventory, 1, slot);
                    ItemSlot.LeftClick(player[myPlayer].inventory, 1, slot);
                    ItemSlot.RightClick(player[myPlayer].inventory, 1, slot);
                    if (mouseLeftRelease && mouseLeft)
                        Recipe.FindRecipes();

                    ItemSlot.MouseHover(player[myPlayer].inventory, 1, slot);
                }

                ItemSlot.Draw(spriteBatch, player[myPlayer].inventory, 1, slot, new Vector2(num98, num99));
            }

            Vector2 vector3 = FontAssets.MouseText.Value.MeasureString("Ammo");
            Vector2 vector4 = FontAssets.MouseText.Value.MeasureString(Lang.inter[27].Value);
            float num100 = vector3.X / vector4.X;
            spriteBatch.DrawString(FontAssets.MouseText.Value, Lang.inter[27].Value, new Vector2(532f, 84f + (vector3.Y - vector3.Y * num100) / 2f), new Microsoft.Xna.Framework.Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, default(Vector2), 0.75f * num100, SpriteEffects.None, 0f);
            inventoryScale = 0.6f;
            for (int num101 = 0; num101 < 4; num101++) {
                int num102 = 534;
                int num103 = (int)(85f + (float)(num101 * 56) * inventoryScale + 20f);
                int slot2 = 54 + num101;
                new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                if (mouseX >= num102 && (float)mouseX <= (float)num102 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num103 && (float)mouseY <= (float)num103 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                    player[myPlayer].mouseInterface = true;
                    ItemSlot.OverrideHover(player[myPlayer].inventory, 2, slot2);
                    ItemSlot.LeftClick(player[myPlayer].inventory, 2, slot2);
                    ItemSlot.RightClick(player[myPlayer].inventory, 2, slot2);
                    if (mouseLeftRelease && mouseLeft)
                        Recipe.FindRecipes();

                    ItemSlot.MouseHover(player[myPlayer].inventory, 2, slot2);
                }

                ItemSlot.Draw(spriteBatch, player[myPlayer].inventory, 2, slot2, new Vector2(num102, num103));
            }

            if (npcShop > 0 && (!playerInventory || player[myPlayer].talkNPC == -1))
                SetNPCShopIndex(0);

            if (npcShop > 0 && !recBigList) {
                Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, Lang.inter[28].Value, 504f, instance.invBottom, Microsoft.Xna.Framework.Color.White * ((float)(int)mouseTextColor / 255f), Microsoft.Xna.Framework.Color.Black, Vector2.Zero);
                ItemSlot.DrawSavings(spriteBatch, 504f, instance.invBottom);
                inventoryScale = 0.755f;
                if (mouseX > 73 && mouseX < (int)(73f + 560f * inventoryScale) && mouseY > instance.invBottom && mouseY < (int)((float)instance.invBottom + 224f * inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                    player[myPlayer].mouseInterface = true;

                for (int num104 = 0; num104 < 10; num104++) {
                    for (int num105 = 0; num105 < 4; num105++) {
                        int num106 = (int)(73f + (float)(num104 * 56) * inventoryScale);
                        int num107 = (int)((float)instance.invBottom + (float)(num105 * 56) * inventoryScale);
                        int slot3 = num104 + num105 * 10;
                        new Microsoft.Xna.Framework.Color(100, 100, 100, 100);
                        if (mouseX >= num106 && (float)mouseX <= (float)num106 + (float)TextureAssets.InventoryBack.Width() * inventoryScale && mouseY >= num107 && (float)mouseY <= (float)num107 + (float)TextureAssets.InventoryBack.Height() * inventoryScale && !PlayerInput.IgnoreMouseInterface) {
                            ItemSlot.OverrideHover(instance.shop[npcShop].item, 15, slot3);
                            player[myPlayer].mouseInterface = true;
                            ItemSlot.LeftClick(instance.shop[npcShop].item, 15, slot3);
                            ItemSlot.RightClick(instance.shop[npcShop].item, 15, slot3);
                            ItemSlot.MouseHover(instance.shop[npcShop].item, 15, slot3);
                        }

                        ItemSlot.Draw(spriteBatch, instance.shop[npcShop].item, 15, slot3, new Vector2(num106, num107));
                    }
                }
            }

            if (player[myPlayer].chest > -1 && !tileContainer[tile[player[myPlayer].chestX, player[myPlayer].chestY].TileType]) {
                player[myPlayer].chest = -1;
                Recipe.FindRecipes();
            }

            int offsetDown = 0;
            UIVirtualKeyboard.ShouldHideText = !PlayerInput.SettingsForUI.ShowGamepadHints;
            if (!PlayerInput.UsingGamepad)
                offsetDown = 9999;

            UIVirtualKeyboard.OffsetDown = offsetDown;
            ChestUI.Draw(spriteBatch);
            LocalPlayer.tileEntityAnchor.GetTileEntity()?.OnInventoryDraw(LocalPlayer, spriteBatch);
            if (player[myPlayer].chest == -1 && npcShop == 0) {
                int num108 = 0;
                int num109 = 498;
                int num110 = 244;
                int num111 = TextureAssets.ChestStack[num108].Width();
                int num112 = TextureAssets.ChestStack[num108].Height();
                UILinkPointNavigator.SetPosition(301, new Vector2((float)num109 + (float)num111 * 0.75f, (float)num110 + (float)num112 * 0.75f));
                if (mouseX >= num109 && mouseX <= num109 + num111 && mouseY >= num110 && mouseY <= num110 + num112 && !PlayerInput.IgnoreMouseInterface) {
                    num108 = 1;
                    if (!allChestStackHover) {
                        SoundEngineReflection.PlaySound(12);
                        allChestStackHover = true;
                    }

                    if (mouseLeft && mouseLeftRelease) {
                        mouseLeftRelease = false;
                        player[myPlayer].QuickStackAllChests();
                        Recipe.FindRecipes();
                    }

                    player[myPlayer].mouseInterface = true;
                }
                else if (allChestStackHover) {
                    SoundEngineReflection.PlaySound(12);
                    allChestStackHover = false;
                }

                spriteBatch.Draw(TextureAssets.ChestStack[num108].Value, new Vector2(num109, num110), new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.ChestStack[num108].Width(), TextureAssets.ChestStack[num108].Height()), Microsoft.Xna.Framework.Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                if (!mouseText && num108 == 1)
                    instance.MouseText(Language.GetTextValue("GameUI.QuickStackToNearby"), 0, 0);
            }

            if (player[myPlayer].chest != -1 || npcShop != 0)
                return;

            int num113 = 0;
            int num114 = 534;
            int num115 = 244;
            int num116 = 30;
            int num117 = 30;
            UILinkPointNavigator.SetPosition(302, new Vector2((float)num114 + (float)num116 * 0.75f, (float)num115 + (float)num117 * 0.75f));
            bool flag11 = false;
            if (mouseX >= num114 && mouseX <= num114 + num116 && mouseY >= num115 && mouseY <= num115 + num117 && !PlayerInput.IgnoreMouseInterface) {
                num113 = 1;
                flag11 = true;
                player[myPlayer].mouseInterface = true;
                if (mouseLeft && mouseLeftRelease) {
                    mouseLeftRelease = false;
                    ItemSorting.SortInventory();
                    Recipe.FindRecipes();
                }
            }

            if (flag11 != inventorySortMouseOver) {
                SoundEngineReflection.PlaySound(12);
                inventorySortMouseOver = flag11;
            }

            Texture2D value6 = TextureAssets.InventorySort[inventorySortMouseOver ? 1 : 0].Value;
            spriteBatch.Draw(value6, new Vector2(num114, num115), null, Microsoft.Xna.Framework.Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            if (!mouseText && num113 == 1)
                instance.MouseText(Language.GetTextValue("GameUI.SortInventory"), 0, 0);
        }


        public static bool DrawInterface_Logic_0() {
           BetterGameUI.Mod.ActiveBuffsIndexes = new List<int>(Terraria.Player.MaxBuffs);
            for (int i = 0; i < Terraria.Player.MaxBuffs; ++i) {
                if (player[myPlayer].buffType[i] > 0) {
                    BetterGameUI.Mod.ActiveBuffsIndexes.Add(i);

                    buffAlpha[i] -= 0.05f;
                    if (buffAlpha[i] < 0f) {
                        buffAlpha[i] = 0f;
                    }
                }
            }

            HandleHotbar();
            return true;
        }

        public static int DisplayedSelectedItem = 0;
        public static int ItemSelectedOnAnimationStart = 0;
        public static void HandleHotbar() {
            if (!BetterGameUI.Mod.ClientConfig.AllowInteractingWithHotbarWhileUsingAnItem) {
                DisplayedSelectedItem = player[myPlayer].selectedItem;
            }
            else if (player[myPlayer].itemAnimation == 0 && player[myPlayer].ItemTimeIsZero && player[myPlayer].reuseDelay == 0) {
                ItemSelectedOnAnimationStart = DisplayedSelectedItem = player[myPlayer].selectedItem;
            }
            else {
                if (player[myPlayer].ItemAnimationJustStarted) {
                    ItemSelectedOnAnimationStart = DisplayedSelectedItem = player[myPlayer].selectedItem;
                }
                if (player[myPlayer].selectedItem != ItemSelectedOnAnimationStart) {
                    ItemSelectedOnAnimationStart = DisplayedSelectedItem = player[myPlayer].selectedItem;
                }

                if (!drawingPlayerChat && DisplayedSelectedItem != 58 && !editSign && !editChest) {
                    if (PlayerInput.Triggers.Current.Hotbar1) {
                        DisplayedSelectedItem = 0;
                    }
                    if (PlayerInput.Triggers.Current.Hotbar2) {
                        DisplayedSelectedItem = 1;
                    }
                    if (PlayerInput.Triggers.Current.Hotbar3) {
                        DisplayedSelectedItem = 2;
                    }
                    if (PlayerInput.Triggers.Current.Hotbar4) {
                        DisplayedSelectedItem = 3;
                    }
                    if (PlayerInput.Triggers.Current.Hotbar5) {
                        DisplayedSelectedItem = 4;
                    }
                    if (PlayerInput.Triggers.Current.Hotbar6) {
                        DisplayedSelectedItem = 5;
                    }
                    if (PlayerInput.Triggers.Current.Hotbar7) {
                        DisplayedSelectedItem = 6;
                    }
                    if (PlayerInput.Triggers.Current.Hotbar8) {
                        DisplayedSelectedItem = 7;

                    }
                    if (PlayerInput.Triggers.Current.Hotbar9) {
                        DisplayedSelectedItem = 8;

                    }
                    if (PlayerInput.Triggers.Current.Hotbar10) {
                        DisplayedSelectedItem = 9;
                    }
                };

                int offset = 0;
                // these correrpond to mouse 4 and mouse 5 respectively
                int num9 = PlayerInput.Triggers.Current.HotbarPlus.ToInt() - PlayerInput.Triggers.Current.HotbarMinus.ToInt();
                if (PlayerInput.CurrentProfile.HotbarAllowsRadial && num9 != 0 && PlayerInput.Triggers.Current.HotbarHoldTime > PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired && PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired != -1) {
                    PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed = true;
                    PlayerInput.Triggers.Current.HotbarScrollCD = 2;
                }

                if (PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired != -1) {
                    num9 = PlayerInput.Triggers.JustReleased.HotbarPlus.ToInt() - PlayerInput.Triggers.JustReleased.HotbarMinus.ToInt();
                    if (PlayerInput.Triggers.Current.HotbarScrollCD == 1 && num9 != 0)
                        num9 = 0;
                }

                if (PlayerInput.Triggers.Current.HotbarScrollCD == 0 && num9 != 0) {
                    offset += num9;
                    PlayerInput.Triggers.Current.HotbarScrollCD = 8;
                }

                if (!inFancyUI && !Main.ingameOptionsWindow)
                    offset += PlayerInput.ScrollWheelDelta / -120;

                if (DisplayedSelectedItem <= 9) {
                    while (offset + DisplayedSelectedItem > 9) {
                        offset -= 10;
                    }

                    while (offset + DisplayedSelectedItem < 0) {
                        offset += 10;
                    }

                    DisplayedSelectedItem += offset;
                    if (offset != 0) {
                        SoundEngineReflection.PlaySound(12);
                        int num10 = DisplayedSelectedItem - offset;
                        player[myPlayer].DpadRadial.ChangeSelection(-1);
                        player[myPlayer].CircularRadial.ChangeSelection(-1);
                        DisplayedSelectedItem = num10 + offset;
                        player[myPlayer].nonTorch = -1;
                    }

                    if (player[myPlayer].changeItem >= 0) {
                        if (DisplayedSelectedItem != player[myPlayer].changeItem) {
                            SoundEngineReflection.PlaySound(12);
                        }

                        DisplayedSelectedItem = player[myPlayer].changeItem;
                        player[myPlayer].changeItem = -1;
                    }

                    if (player[myPlayer].itemAnimation == 0 && DisplayedSelectedItem != 58) {
                        while (DisplayedSelectedItem > 9) {
                            DisplayedSelectedItem -= 10;
                        }

                        while (DisplayedSelectedItem < 0) {
                            DisplayedSelectedItem += 10;
                        }
                    }
                }

                if (player[myPlayer].itemAnimation == 1) {
                    if (player[myPlayer].selectedItem != DisplayedSelectedItem) {
                        player[myPlayer].selectedItem = DisplayedSelectedItem;
                        player[myPlayer].reuseDelay = 0;
                    }
                }
            }
        }

        // TODO: shift should behave like any other request to change the selected item
        // TODO: fix minimap buttons being interactable when the hotbar is locked1
        // NOTE: has the map always been drawn while the menu is up? // yes, though it probably shouldn't
        // TODO: fix inventory item in usage not lightening up when the inventory is up
        public static bool DrawInterface_Hotbar() {
            if (playerInventory || player[myPlayer].ghost) {
                return true;
            }

            // set to 'items' in case the next conditional turns out false
            string text = Lang.inter[37].Value;
            // if selected item has a name AND that name is not empty
            if (player[myPlayer].inventory[DisplayedSelectedItem].Name != null && player[myPlayer].inventory[DisplayedSelectedItem].Name != "") {
                // get name of selected item
                text = player[myPlayer].inventory[DisplayedSelectedItem].AffixName();
            }

            // draw selected item name
            Vector2 vector = FontAssets.MouseText.Value.MeasureString(text) / 2f;
            spriteBatch.DrawString(FontAssets.MouseText.Value, text, new Vector2(236f - vector.X, 0f), new Microsoft.Xna.Framework.Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

            // iterate over each item in the hotbar and draw it
            int num = 20;
            for (int i = 0; i < 10; i++) {
                // TODO: consider making some of these values configurable
                // if the item to draw is the selected one, progresively scale it up.
                if (i == DisplayedSelectedItem) {
                    if (hotbarScale[i] < 1f)
                        hotbarScale[i] += 0.05f;
                }
                // else progresively scale it down
                else if ((double)hotbarScale[i] > 0.75) {
                    hotbarScale[i] -= 0.05f;
                }

                // TODO: an animation where the left most and right most items dont move would be nicer
                // TODO: clamp hotbarScale[i]

                float itrHotbarScale = hotbarScale[i];
                int num3 = (int)(20f + 22f * (1f - itrHotbarScale));
                // a = 230 if item is selected, a = 185 otherwise
                int a = (int)(75f + 150f * itrHotbarScale);
                Microsoft.Xna.Framework.Color lightColor = new Microsoft.Xna.Framework.Color(255, 255, 255, a);
                // if the hotbar is not locked AND the player input doesn't say to ignore the mouse interface AND the mouse is hovering this hotbar slot
                if (!player[myPlayer].hbLocked && mouseX >= num && (float)mouseX <= (float)num + (float)TextureAssets.InventoryBack.Width() * hotbarScale[i] && mouseY >= num3 && (float)mouseY <= (float)num3 + (float)TextureAssets.InventoryBack.Height() * hotbarScale[i] && !player[myPlayer].channel) {
                    if (!PlayerInput.IgnoreMouseInterface) {
                        player[myPlayer].mouseInterface = true;
                        player[myPlayer].cursorItemIconEnabled = false;
                    }

                    // NOTE: clicking on item is reflected one frame late
                    // TODO: look into blockMouse
                    if (BetterGameUI.Mod.ClientConfig.AllowInteractingWithHotbarWhileUsingAnItem & mouseLeft & !blockMouse) {
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
                ItemSlot.Draw(spriteBatch, player[myPlayer].inventory, 13, i, new Vector2(num, num3), lightColor);
                inventoryScale = prevInventoryScale;
                num += (int)((float)TextureAssets.InventoryBack.Width() * hotbarScale[i]) + 4;
            }

            // if the selected item doesn't fit in the scrollbar (such as those selected by the smart cursor)
            if (DisplayedSelectedItem >= 10 && (DisplayedSelectedItem != 58 || mouseItem.type > 0)) {
                // a2 will always equal a non-transitioning selected item alpha
                int a2 = (int)(75f + 150f * 1f);
                Microsoft.Xna.Framework.Color lightColor2 = new Microsoft.Xna.Framework.Color(255, 255, 255, a2);
                float prevInventoryScale = inventoryScale;
                inventoryScale = 1f;
                ItemSlot.Draw(spriteBatch, player[myPlayer].inventory, 13, DisplayedSelectedItem, new Vector2(num, 20f), lightColor2);
                inventoryScale = prevInventoryScale;
            }

            return true;
        }

        public static bool DrawInterface_Inventory() {
            HackForGamepadInputHell(instance);
            if (playerInventory) {
                if (player[myPlayer].chest != -1) {
                    CreativeMenu.CloseMenu();
                }

                UISystem.DrawInventory();
            }
            else {
                CreativeMenu.CloseMenu();
                recFastScroll = true;
                instance.SetMouseNPC(-1, -1);
                EquipPage = 0;
            }

            // TODO: check if the breath being drawn below the player when the inventory is open is something that's always been a thing or something that I'm causing
            // TODO: make mapHeight automatically call GetValue(null) on itself
            // TODO: do this in UIMap.Update
            var mapHeight = (int)MainReflection.mapHeight.GetValue(null);
            if (UIMap.Height.Pixels != mapHeight) {
                UIMap.Height = StyleDimension.FromPixels(mapHeight);
                UserInterfaceMap.Recalculate();
            }

            UserInterfaceMap.Draw(spriteBatch, LastUpdateUIGameTime);
            
            return true;
        }

        public static bool DrawInterface_ResourceBars() {
            ResourceSetsManager.Draw();
            // TODO: doesn't GameInterfaceLayer.Draw() already call spriteBatch.Begin? does it use the same params?
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None,
                RasterizerState.CullCounterClockwise, null, UIScaleMatrix);

            DrawInterface_Resources_Breath();
            DrawInterface_Resources_ClearBuffs();
            
            UserInterfaceInventoryDownBuffsBar.Draw(spriteBatch, LastUpdateUIGameTime);
            return true;
        }

        // TODO: UIMap class
        public override void Load() {
            if (!dedServ) {
                UserInterfaceInventoryDownBuffsBar = new();
                UserInterfaceInventoryDownBuffsBar.SetState(new UIInventoryDownBuffsBar());
                UIInventoryDownBuffsBar.Activate();

                var inventoryBuffIconsBarParentUI = new UIBasic();
                inventoryBuffIconsBarParentUI.Append(new UIInventoryUpBuffsBar());
                inventoryBuffIconsBarParentUI.Width = StyleDimension.FromPercent(1f);
                UserInterfaceMap = new();
                UserInterfaceMap.SetState(inventoryBuffIconsBarParentUI);
                UIInventoryUpBuffsBar.Activate();
            }
        }

        public override void Unload() {
            LastUpdateUIGameTime = null;
            UserInterfaceInventoryDownBuffsBar = null;
            UserInterfaceMap = null;
        }

        public override void UpdateUI(GameTime gameTime) {
            LastUpdateUIGameTime = gameTime;
            if (LastUpdateUIGameTime != null) {
                UserInterfaceInventoryDownBuffsBar.Update(LastUpdateUIGameTime);
                UserInterfaceMap.Update(LastUpdateUIGameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            layers.Insert(0, new LegacyGameInterfaceLayer(
                    "BetterGameUI: Logic 0", DrawInterface_Logic_0,
                    InterfaceScaleType.None));

            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (index != -1) {
                layers[index] = new LegacyGameInterfaceLayer(
                    "Vanilla: Resource Bars", DrawInterface_ResourceBars,
                    InterfaceScaleType.UI);
            }

            index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (index != -1) {
                layers[index] = new LegacyGameInterfaceLayer(
                    "Vanilla: Inventory", DrawInterface_Inventory,
                    InterfaceScaleType.UI);
            }

            index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (index != -1) {
                layers[index] = new LegacyGameInterfaceLayer(
                    "Vanilla: Hotbar", DrawInterface_Hotbar,
                    InterfaceScaleType.UI);
            }
        }
    }
}