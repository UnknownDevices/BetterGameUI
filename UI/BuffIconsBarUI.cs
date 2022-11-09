using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameInput;
using static Terraria.Main;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria.DataStructures;
using ReLogic.Content;

namespace BetterGameUI.UI {
    public class BuffIconsBarUI : UIElement {
        public const int IconW = 32;
        public const int IconH = 32;
        public const int IconTextH = 12;
        public const int IconToIconPad = 6;
        public const int FirstIconsColX = 16;
        public const int FirstIconsRowY = 0;
        public static int IconRowsCount => Mod.ClientConfig.IconRowsCount;
        public static int IconColsCount => Mod.ClientConfig.IconColsCount;
        public ScrollbarUI ScrollbarUI {
            get => Elements[0] as ScrollbarUI;
            set => Elements[0] = value;
        }
        public ScrollerUI ScrollerUI {
            get => ScrollbarUI.ScrollerUI;
            set => ScrollbarUI.ScrollerUI = value;
        }

        public static BuffIconsBarUI Default() {
            var output = new BuffIconsBarUI {
                Top = StyleDimension.FromPixels(Mod.ClientConfig.Y),
                Left = StyleDimension.FromPixels(Mod.ClientConfig.X),
                Width = StyleDimension.FromPixels(((IconW + IconToIconPad) *
                    IconColsCount) - IconToIconPad + 16),
                Height = StyleDimension.FromPixels(((IconH + IconTextH + IconToIconPad) *
                    IconRowsCount) - IconToIconPad)
            };

            output.Append(new ScrollbarUI {
                Top = StyleDimension.FromPixels(2f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(10f),
                Height = StyleDimension.FromPixelsAndPercent(-16f, 1f),
                Alpha = 0.5f,
                CornerHeight = 4,
            });

            output.ScrollbarUI.Append(new ScrollerUI {
                Top = StyleDimension.FromPixels(0f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(6f),
                Height = StyleDimension.FromPixels(8f),
                MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinScrollerHeight),
                Alpha = 0.5f,
                CornerHeight = 2,
            });

            output.Recalculate();
            Mod.OnClientConfigChanged += output.HandleClientConfigChanged;

            return output;
        }

        public override void Update(GameTime gameTime) {
            // TODO: this should be called at a higher level
            Mod.UpdateActiveBuffsIndexes();

            if (Mod.ActiveBuffsIndexes.Count <= 0) {
                ScrollbarUI.MaxScrolls = 0;
            } else {
                ScrollbarUI.MaxScrolls = (uint)Math.Max(
                    Math.Ceiling((double)Mod.ActiveBuffsIndexes.Count / (double)IconColsCount) - IconRowsCount, 0);
            }

            ScrollbarUI.IsVisible = Mod.ClientConfig.AlwaysShowScrollbar | 0 < ScrollbarUI.MaxScrolls;

            bool isMouseScrollAllowed =
                !Mod.ClientConfig.NeverAllowMouseScroll &
                Player.IsMouseScrollAllowed &
                (!Mod.ClientConfig.HoverUIToAllowMouseScroll | IsMouseHovering);
            bool isScrollerDraggingAllowed = Mod.ClientConfig.AllowScrollerDragging &&
                (!Mod.ClientConfig.LockWhenHotbarIsLocked | !player[myPlayer].hbLocked);

            if (isMouseScrollAllowed & Mod.ClientConfig.SmartLockVanillaMouseScroll) {
                PlayerInput.LockVanillaMouseScroll("BuffIconsBarUI");
            }

            ScrollbarUI.ProcessInput(isMouseScrollAllowed, isScrollerDraggingAllowed,
                Mod.ClientConfig.ScrollerHitboxWidthModifier, Mod.ClientConfig.ScrollerHitboxHeightModifier);

            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            var rec = GetDimensions().ToRectangle();
            int mouseoveredIcon = -1;
            int buffsBegin = (int)ScrollbarUI.Scrolls * IconColsCount;
            int iconsEnd = Math.Min(Mod.ActiveBuffsIndexes.Count - buffsBegin, IconRowsCount * IconColsCount);
            for (int iconsI = 0; iconsI < iconsEnd; ++iconsI) {
                int x = rec.Left + FirstIconsColX + (IconW + IconToIconPad) * (iconsI % IconColsCount);
                int y = rec.Top + FirstIconsRowY + (IconH + IconTextH + IconToIconPad) * (iconsI / IconColsCount);
                mouseoveredIcon = DrawBuffIcon(mouseoveredIcon, Mod.ActiveBuffsIndexes[iconsI + buffsBegin], x, y);
            }

            if (mouseoveredIcon >= 0) {
                int buffTy = player[myPlayer].buffType[mouseoveredIcon];
                if (buffTy > 0) {
                    string buffName = Lang.GetBuffName(buffTy);
                    string buffTooltip = GetBuffTooltip(player[myPlayer], buffTy);

                    int buffRarity = 0;
                    if (meleeBuff[buffTy]) {
                        buffRarity = -10;
                    }
                    if (buffTy == 147) {
                        bannerMouseOver = true;
                    }

                    BuffLoader.ModifyBuffTip(buffTy, ref buffTooltip, ref buffRarity);
                    instance.MouseTextHackZoom(buffName, buffRarity, 0, buffTooltip);
                }
            }
        }

        // TODO: reformat
        public static int DrawBuffIcon(int drawBuffText, int buffSlotOnPlayer, int x, int y) {
            int buffTy = player[myPlayer].buffType[buffSlotOnPlayer];
            if (buffTy == 0) {
                return drawBuffText;
            }

            var color = new Color(buffAlpha[buffSlotOnPlayer], buffAlpha[buffSlotOnPlayer], buffAlpha[buffSlotOnPlayer], 
                buffAlpha[buffSlotOnPlayer]);
            
            Asset<Texture2D> buffAsset = TextureAssets.Buff[buffTy];
            Texture2D texture = buffAsset.Value;
            Vector2 drawPosition = new Vector2(x, y);
            int width = buffAsset.Width();
            int height = buffAsset.Height();
            Vector2 textPosition = new Vector2(x, y + height);
            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle mouseRectangle = new Rectangle(x, y, width, height);
            Color drawColor = color;

            BuffDrawParams drawParams = new BuffDrawParams(texture, drawPosition, textPosition, sourceRectangle, mouseRectangle, drawColor);

            bool skipped = !BuffLoader.PreDraw(spriteBatch, buffTy, buffSlotOnPlayer, ref drawParams);

            (texture, drawPosition, textPosition, sourceRectangle, mouseRectangle, drawColor) = drawParams;

            if (!skipped)
                spriteBatch.Draw(texture, drawPosition, sourceRectangle, drawColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

            BuffLoader.PostDraw(spriteBatch, buffTy, buffSlotOnPlayer, drawParams);

            if (TryGetBuffTime(buffSlotOnPlayer, out int buffTimeValue) && buffTimeValue > 2) {
                string text = Lang.LocalizedDuration(new TimeSpan(0, 0, buffTimeValue / 60), abbreviated: true, showAllAvailableUnits: false);
                spriteBatch.DrawString(FontAssets.ItemStack.Value, text, textPosition, color, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
            }

            if ((!Mod.ClientConfig.LockWhenHotbarIsLocked | !player[myPlayer].hbLocked) && 
                mouseRectangle.Contains(mouseX, mouseY)) 
            {
                drawBuffText = buffSlotOnPlayer;
                buffAlpha[buffSlotOnPlayer] += 0.1f;

                bool flag = mouseRight && mouseRightRelease;
                if (PlayerInput.UsingGamepad) {
                    flag = (mouseLeft && mouseLeftRelease && playerInventory);
                    if (playerInventory)
                        player[myPlayer].mouseInterface = true;
                }
                else {
                    player[myPlayer].mouseInterface = true;
                }

                if (flag)
                    flag &= BuffLoader.RightClick(buffTy, buffSlotOnPlayer);

                if (flag)
                    TryRemovingBuff(buffSlotOnPlayer, buffTy);
            }
            else {
                buffAlpha[buffSlotOnPlayer] -= 0.05f;
            }

            if (buffAlpha[buffSlotOnPlayer] > 1f)
                buffAlpha[buffSlotOnPlayer] = 1f;
            else if (buffAlpha[buffSlotOnPlayer] < 0.4)
                buffAlpha[buffSlotOnPlayer] = 0.4f;

            if (PlayerInput.UsingGamepad && !playerInventory)
                drawBuffText = -1;

            return drawBuffText;
        }

        public void HandleClientConfigChanged() {
            Top = StyleDimension.FromPixels(Mod.ClientConfig.Y);
            Left = StyleDimension.FromPixels(Mod.ClientConfig.X);
            Width = StyleDimension.FromPixels(((IconW + IconToIconPad) *
                IconColsCount) - IconToIconPad + 16);
            Height = StyleDimension.FromPixels(((IconH + IconTextH + IconToIconPad) *
                IconRowsCount) - IconToIconPad);

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinScrollerHeight);
            Recalculate();
        }
    }
}