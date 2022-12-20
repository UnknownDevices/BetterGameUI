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
    public class UIBuffsBar : UIBasic
    {
        public const int IconWidth = 32;
        public const int IconHeight = 32;
        public const int IconTextHeight = 12;
        public const int IconToIconPad = 6;
        public const int ScrollbarReservedWidth = 14;

        public ScrollbarRelPos ScrollbarPosition;
        public BuffIconsHorOrder IconsHorOrder;
        public int HoveredIcon;
        public ushort IconRowsCount;
        public ushort IconColsCount;

        public UIBuffsBarScrollbar UIScrollbar {
            get => Elements[0] as UIBuffsBarScrollbar;
            set => Elements[0] = value;
        }

        public UIBuffsBar() {
            Mod.OnClientConfigChanged += HandleClientConfigChanged;

            Append(new UIBuffsBarScrollbar());
            UpdateClientConfigDependencies();

            Recalculate();
        }

        // NOTE: the lightning up of icons in one buffs bar reflects on the other.
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            if (!IsEnabled) {
                return;
            }

            Rectangle rec = GetDimensions().ToRectangle();
            int mouseoveredIcon = -1;
            int buffsBegin = (int)UIScrollbar.ScrolledNotches * IconColsCount;
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
                if (ScrollbarPosition == ScrollbarRelPos.LeftOfIcons) {
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

        public virtual bool IsLocked() {
            return false;
        }

        public virtual void UpdateClientConfigDependencies() {
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);

            switch (ScrollbarPosition) {
                case ScrollbarRelPos.LeftOfIcons:
                    UIScrollbar.Left = StyleDimension.Empty;
                    break;
                case ScrollbarRelPos.RightOfIcons:
                    UIScrollbar.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            UIScrollbar.UIScroller.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinScrollerHeight);
        }

        public virtual void HandleClientConfigChanged() {
            UpdateClientConfigDependencies();
            Recalculate();
        }

        public virtual bool IsMouseHoveringHitbox() {
            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            return GetDimensions().GrowFromCenter(Mod.ClientConfig.BuffsBarHitboxMod).
                Contains(mouseX, mouseY);
        }

        public int DrawBuffIcon(int drawBuffText, int buffSlotOnPlayer, int x, int y) {
            // TODO: this should not interrupt mouse if IgnoreMouseInterface is set to true, same with scroller
            int buffTy = player[myPlayer].buffType[buffSlotOnPlayer];
            if (buffTy == 0) {
                return drawBuffText;
            }

            Asset<Texture2D> buffAsset = TextureAssets.Buff[buffTy];
            Texture2D texture = buffAsset.Value;
            Vector2 drawPosition = new Vector2(x, y);
            int width = buffAsset.Width();
            int height = buffAsset.Height();
            Vector2 textPosition = new Vector2(x, y + height);
            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle mouseRectangle = new Rectangle(x, y, width, height);

            if (!IsLocked() && mouseRectangle.Contains(mouseX, mouseY)) {
                drawBuffText = buffSlotOnPlayer;
                // NOTE: this value is increased by 0.05f to compensate from UISystem.DrawInterface_Logic_0 substraction 0.05f from every buff every frame
                buffAlpha[buffSlotOnPlayer] += 0.15f;

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

            if (buffAlpha[buffSlotOnPlayer] > 1f) {
                buffAlpha[buffSlotOnPlayer] = 1f;
            }
            else if (buffAlpha[buffSlotOnPlayer] < Alpha) {
                buffAlpha[buffSlotOnPlayer] = Alpha;
            }

            var color = new Color(buffAlpha[buffSlotOnPlayer], buffAlpha[buffSlotOnPlayer], buffAlpha[buffSlotOnPlayer],
                buffAlpha[buffSlotOnPlayer]);
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

            if (PlayerInput.UsingGamepad && !playerInventory)
                drawBuffText = -1;

            return drawBuffText;
        }
    }
}