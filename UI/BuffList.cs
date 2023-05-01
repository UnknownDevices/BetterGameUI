using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.DataStructures;
using ReLogic.Graphics;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public enum BuffIconsHorOrder : short
    {
        LeftToRight,
        RightToLeft,
    }
    public enum ScrollbarRelPos : short
    {
        LeftOfIcons,
        RightOfIcons,
    }

    public class BuffList
    {
        public const int IconWidth = 32;
        public const int IconHeight = 32;
        public const int IconTextHeight = 12;
        public const int IconToIconPad = 6;
        public const int ScrollbarReservedWidth = 14;

        public CalculatedStyle Dimensions;
        public bool IsHovered => Dimensions.GrowFromCenter(4).Contains(Player.MouseX, Player.MouseY);
        public virtual bool IsVisible => true;
        public virtual bool IsLocked => false;
        public virtual float Alpha => 1f;
        public virtual ushort RowsCount => 1;
        public virtual ushort ColsCount => 1;
        public virtual BuffIconsHorOrder IconsHorOrder => BuffIconsHorOrder.LeftToRight;
        public virtual ScrollbarRelPos ScrollbarRelPos => ScrollbarRelPos.LeftOfIcons;
        public bool LeftMouseCurrent { get; set; }
        public int HoveredIcon { get; set; }
        public BuffListScrollbar Scrollbar { get; set; }

        public BuffList() {
            Scrollbar = new BuffListScrollbar(this);
        }

        public virtual void Update() {
            if (!IsVisible) {
                Scrollbar.Update();
                return;
            }

            if (LeftMouseCurrent) {
                if (!PlayerInput.Triggers.Current.MouseLeft) {
                    LeftMouseCurrent = false;
                }
            }

            Dimensions.Width = ScrollbarReservedWidth + (IconWidth + IconToIconPad) * ColsCount;
            Dimensions.Height = (IconHeight + IconTextHeight + IconToIconPad) * RowsCount;
            Rectangle rec = Dimensions.ToRectangle();

            int hoveredIcon = -1;
            int iconsBegin = (int)Scrollbar.ScrolledPages * ColsCount;
            int iconsEnd = Math.Min(Mod.ActiveBuffsIndexes.Count - iconsBegin, RowsCount * ColsCount);
            for (int iconsI = 0; iconsI < iconsEnd; ++iconsI) {
                int x = 0;
                switch (IconsHorOrder) {
                    case BuffIconsHorOrder.LeftToRight:
                        x = rec.Left + (IconWidth + IconToIconPad) * (iconsI % ColsCount);
                        break;
                    case BuffIconsHorOrder.RightToLeft:
                        x = rec.Left + (IconWidth + IconToIconPad) * (ColsCount - 1 - (iconsI % ColsCount));
                        break;
                }
                
                if (ScrollbarRelPos == ScrollbarRelPos.LeftOfIcons) {
                    x += ScrollbarReservedWidth;
                }

                int y = rec.Top + (IconHeight + IconTextHeight + IconToIconPad) * (iconsI / ColsCount);

                hoveredIcon = UpdateBuffIcon(hoveredIcon, Mod.ActiveBuffsIndexes[iconsI + iconsBegin], x, y);
            }

            if (hoveredIcon >= 0) {
                int buffTy = Main.LocalPlayer.buffType[hoveredIcon];
                if (buffTy > 0) {
                    string buffName = Lang.GetBuffName(buffTy);
                    string buffTooltip = Main.GetBuffTooltip(Main.LocalPlayer, buffTy);

                    int buffRarity = 0;
                    if (Main.meleeBuff[buffTy]) {
                        buffRarity = -10;
                    }
                    if (buffTy == 147) {
                        Main.bannerMouseOver = true;
                    }

                    BuffLoader.ModifyBuffTip(buffTy, ref buffTooltip, ref buffRarity);
                    Main.instance.MouseTextHackZoom(buffName, buffRarity, 0, buffTooltip);
                }
            }

            Scrollbar.Update();
        }

        public int UpdateBuffIcon(int hoveredIcon, int buffSlotOnPlayer, int x, int y) {
            int buffTy = Main.LocalPlayer.buffType[buffSlotOnPlayer];
            if (buffTy <= 0) {
                return hoveredIcon;
            }

            Asset<Texture2D> buffAsset = TextureAssets.Buff[buffTy];
            Texture2D texture = buffAsset.Value;
            Vector2 drawPosition = new Vector2(x, y);
            int width = buffAsset.Width();
            int height = buffAsset.Height();
            Vector2 textPosition = new Vector2(x, y + height);
            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle mouseRectangle = new Rectangle(x, y, width, height);

            if (!IsLocked && mouseRectangle.Contains(Main.mouseX, Main.mouseY)) {
                hoveredIcon = buffSlotOnPlayer;
                Main.buffAlpha[buffSlotOnPlayer] += 0.15f;

                if (PlayerInput.Triggers.JustPressed.MouseLeft) {
                    LeftMouseCurrent = true;
                }

                if (!PlayerInput.Triggers.Current.MouseLeft || LeftMouseCurrent) {
                    Main.LocalPlayer.mouseInterface = true;
                    if (Main.mouseRight && Main.mouseRightRelease) {
                        if (BuffLoader.RightClick(buffTy, buffSlotOnPlayer)) {
                            Main.TryRemovingBuff(buffSlotOnPlayer, buffTy);
                        }
                    }
                }
            }

            Main.buffAlpha[buffSlotOnPlayer] = Math.Clamp(Main.buffAlpha[buffSlotOnPlayer], Alpha, 1f);

            var color = new Color(Main.buffAlpha[buffSlotOnPlayer], Main.buffAlpha[buffSlotOnPlayer], Main.buffAlpha[buffSlotOnPlayer],
                Main.buffAlpha[buffSlotOnPlayer]);
            Color drawColor = color;

            BuffDrawParams drawParams = new BuffDrawParams(texture, drawPosition, textPosition, sourceRectangle, mouseRectangle, drawColor);

            bool skipped = !BuffLoader.PreDraw(Main.spriteBatch, buffTy, buffSlotOnPlayer, ref drawParams);

            (texture, drawPosition, textPosition, sourceRectangle, mouseRectangle, drawColor) = drawParams;

            if (!skipped) {
                Main.spriteBatch.Draw(texture, drawPosition, sourceRectangle, drawColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }

            BuffLoader.PostDraw(Main.spriteBatch, buffTy, buffSlotOnPlayer, drawParams);

            if (Main.TryGetBuffTime(buffSlotOnPlayer, out int buffTimeValue) && buffTimeValue > 2) {
                string text = Lang.LocalizedDuration(new TimeSpan(0, 0, buffTimeValue / 60), abbreviated: true, showAllAvailableUnits: false);
                Main.spriteBatch.DrawString(FontAssets.ItemStack.Value, text, textPosition, color, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
            }

            if (PlayerInput.UsingGamepad && !Main.playerInventory)
                hoveredIcon = -1;

            return hoveredIcon;
        }
    }
}
