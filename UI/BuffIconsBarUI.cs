using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.Main;

namespace BetterGameUI.UI
{
    public enum ScrollbarPosition
    {
        LeftOfIcons,
        RightOfIcons,
    }

    public enum BuffIconsHorOrder
    {
        LeftToRight,
        RightToLeft,
    }

    public class BuffIconsBarUI : UIState
    {
        public const int IconWidth = 32;
        public const int IconHeight = 32;
        public const int IconTextHeight = 12;
        public const int IconToIconPad = 6;

        public bool IsVisible = true;
        public bool IsLocked = false;
        public bool IsMouseHoveringHitbox;
        public ScrollbarPosition ScrollbarPosition;
        public BuffIconsHorOrder IconsHorOrder;
        public int HitboxModifier;
        public int ScrollbarReservedWidth;
        public ushort IconRowsCount;
        public ushort IconColsCount;

        public ScrollbarUI ScrollbarUI {
            get => Elements[0] as ScrollbarUI;
            set => Elements[0] = value;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            UpdateBeforeDraw();

            if (IsVisible) {
                base.Draw(spriteBatch);
            }

            IsVisible = true;
            IsLocked = false;
        }

        // TODO: allow inverting icons vertical order
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            var rec = GetDimensions().ToRectangle();
            int mouseoveredIcon = -1;
            int buffsBegin = (int)ScrollbarUI.Scrolls * IconColsCount;
            int iconsEnd = Math.Min(Mod.ActiveBuffsIndexes.Count - buffsBegin, IconRowsCount * IconColsCount);
            for (int iconsI = 0; iconsI < iconsEnd; ++iconsI) {
                int x = 0;
                switch (IconsHorOrder) {
                    case BuffIconsHorOrder.LeftToRight:
                        x = rec.Left + (IconWidth + IconToIconPad) * (iconsI % IconColsCount);
                        break;

                    case BuffIconsHorOrder.RightToLeft:
                        x = rec.Left + (IconWidth + IconToIconPad) * (IconColsCount - 1 - (iconsI % IconColsCount));
                        break;
                }

                if (ScrollbarPosition == ScrollbarPosition.LeftOfIcons) {
                    x += ScrollbarReservedWidth;
                }

                int y = rec.Top + (IconHeight + IconTextHeight + IconToIconPad) * (iconsI / IconColsCount);

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

        public virtual void UpdateBeforeDraw() {
            // calculate my own mouse x and y given the UIScale and don't round them down (like main.mouseX and main.mouseY are).

            var hitbox = GetDimensions();
            if (HitboxModifier != 0) {
                hitbox.X -= (float)HitboxModifier / 2;
                hitbox.Y -= (float)HitboxModifier / 2;
                hitbox.Width += HitboxModifier;
                hitbox.Height += HitboxModifier;
            }

            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            IsMouseHoveringHitbox = hitbox.Contains(mouseX, mouseY);

            if (Mod.ActiveBuffsIndexes.Count <= 0) {
                ScrollbarUI.MaxScrolls = 0;
            }
            else {
                ScrollbarUI.MaxScrolls = (uint)Math.Max(
                    Math.Ceiling((double)Mod.ActiveBuffsIndexes.Count / (double)IconColsCount) - IconRowsCount, 0);
            }

            ScrollbarUI.IsDraggingScrollerAllowed &= !IsLocked;
        }

        public int DrawBuffIcon(int drawBuffText, int buffSlotOnPlayer, int x, int y) {
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

            if (!IsLocked && mouseRectangle.Contains(mouseX, mouseY)) {
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
    }
}