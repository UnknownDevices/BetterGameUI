using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
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
        Left,
        Right,
    }

    public class BuffList
    {
        public const int IconWidth = 32;
        public const int IconHeight = 32;
        public const int IconTextHeight = 12;
        public const int IconToIconPad = 6;
        public const int ScrollbarReservedWidth = 14;

        private static bool currentMouseLeftBegunInBufffList;
        public static bool CurrentMouseLeftBegunInBufffList { get => currentMouseLeftBegunInBufffList; set => currentMouseLeftBegunInBufffList = value; }

        public CalculatedStyle Dimensions;
        public bool IsHovered => Dimensions.GrowFromCenter(4).Contains(Player.MouseX, Player.MouseY);
        public virtual bool IsVisible => true;
        public virtual bool IsLocked => false;
        public virtual float Alpha => 1f;
        public virtual ushort RowsCount => 1;
        public virtual ushort ColsCount => 1;
        public virtual BuffIconsHorOrder IconsHorOrder => BuffIconsHorOrder.LeftToRight;
        public virtual ScrollbarRelPos ScrollbarRelPos => ScrollbarRelPos.Left;
        public int HoveredIcon { get; set; }
        public BuffListScrollbar Scrollbar { get; set; }

        public BuffList() {
            Scrollbar = new BuffListScrollbar(this);
        }

        // TODO: I could absolutely just inject CIL instructions instead
        // TODO: take fields as variables instead
        public virtual void Update() {
            if (!IsVisible) {
                Scrollbar.Update();
                return;
            }

            if (CurrentMouseLeftBegunInBufffList) {
                if (!PlayerInput.Triggers.Current.MouseLeft) {
                    CurrentMouseLeftBegunInBufffList = false;
                }
            }

            var activeBuffsIndexes = new List<int>(Terraria.Player.MaxBuffs);
            for (int i = 0; i < Terraria.Player.MaxBuffs; ++i) {
                if (Main.LocalPlayer.buffType[i] > 0) {
                    activeBuffsIndexes.Add(i);
                }
            }

            Dimensions.Width = ScrollbarReservedWidth + (IconWidth + IconToIconPad) * ColsCount;
            Dimensions.Height = (IconHeight + IconTextHeight + IconToIconPad) * RowsCount;
            Rectangle rec = Dimensions.ToRectangle();

            int hoveredIcon = -1;
            int iconsBegin = (int)Scrollbar.ScrolledNotches * ColsCount;
            int iconsEnd = Math.Min(activeBuffsIndexes.Count - iconsBegin, RowsCount * ColsCount);
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

                if (ScrollbarRelPos == ScrollbarRelPos.Left) {
                    x += ScrollbarReservedWidth;
                }

                int y = rec.Top + (IconHeight + IconTextHeight + IconToIconPad) * (iconsI / ColsCount);

                hoveredIcon = Main.DrawBuffIcon(hoveredIcon, activeBuffsIndexes[iconsI + iconsBegin], x, y);
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

            Scrollbar.Update(activeBuffsIndexes.Count);
        }

        public int UpdateBuffIcon(int drawBuffText, int buffSlotOnPlayer, int x, int y) {
            // aka buffType
            int num = Main.LocalPlayer.buffType[buffSlotOnPlayer];
            if (num <= 0)
                return drawBuffText;

            //Main.buffAlpha[buffSlotOnPlayer] = Math.Clamp(Main.buffAlpha[buffSlotOnPlayer], Alpha, 1f);

            var color = new Color(Main.buffAlpha[buffSlotOnPlayer], Main.buffAlpha[buffSlotOnPlayer], Main.buffAlpha[buffSlotOnPlayer],
                Main.buffAlpha[buffSlotOnPlayer]);

            Asset<Texture2D> buffAsset = TextureAssets.Buff[num];
            Texture2D texture = buffAsset.Value;
            Vector2 drawPosition = new Vector2(x, y);
            int width = buffAsset.Width();
            int height = buffAsset.Height();
            Vector2 textPosition = new Vector2(x, y + height);
            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle mouseRectangle = new Rectangle(x, y, width, height);
            Color drawColor = color;

            BuffDrawParams drawParams = new BuffDrawParams(texture, drawPosition, textPosition, sourceRectangle, mouseRectangle, drawColor);

            bool skipped = !BuffLoader.PreDraw(Main.spriteBatch, num, buffSlotOnPlayer, ref drawParams);

            (texture, drawPosition, textPosition, sourceRectangle, mouseRectangle, drawColor) = drawParams;

            if (!skipped) {
                Main.spriteBatch.Draw(texture, drawPosition, sourceRectangle, drawColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }

            BuffLoader.PostDraw(Main.spriteBatch, num, buffSlotOnPlayer, drawParams);

            if (Main.TryGetBuffTime(buffSlotOnPlayer, out int buffTimeValue) && buffTimeValue > 2) {
                string text = Lang.LocalizedDuration(new TimeSpan(0, 0, buffTimeValue / 60), abbreviated: true, showAllAvailableUnits: false);
                Main.spriteBatch.DrawString(FontAssets.ItemStack.Value, text, textPosition, color, 0f, default(Vector2), 0.8f, SpriteEffects.None, 0f);
            }

            if ((!Main.LocalPlayer.hbLocked || Main.playerInventory) && mouseRectangle.Contains(new Point(Main.mouseX, Main.mouseY))) {
                drawBuffText = buffSlotOnPlayer;
                Main.buffAlpha[buffSlotOnPlayer] += 0.1f;

                if (Main.mouseLeft && Main.mouseLeftRelease) {
                    CurrentMouseLeftBegunInBufffList = true;
                }

                bool flag = Main.mouseRight && Main.mouseRightRelease && (!Main.mouseLeft || CurrentMouseLeftBegunInBufffList);
                if (PlayerInput.UsingGamepad) {
                    flag = (Main.mouseLeft && Main.mouseLeftRelease && Main.playerInventory);
                    if (Main.playerInventory)
                        Main.player[Main.myPlayer].mouseInterface = true;
                }
                else if (!Main.mouseLeft || CurrentMouseLeftBegunInBufffList) {
                    Main.player[Main.myPlayer].mouseInterface = true;
                }

                if (flag)
                    flag &= BuffLoader.RightClick(num, buffSlotOnPlayer);

                if (flag)
                    Main.TryRemovingBuff(buffSlotOnPlayer, num);
            }
            else {
                Main.buffAlpha[buffSlotOnPlayer] -= 0.05f;
            }

            if (Main.buffAlpha[buffSlotOnPlayer] > 1f)
                Main.buffAlpha[buffSlotOnPlayer] = 1f;
            else if ((double)Main.buffAlpha[buffSlotOnPlayer] < 0.4)
                Main.buffAlpha[buffSlotOnPlayer] = 0.4f;

            if (PlayerInput.UsingGamepad && !Main.playerInventory)
                drawBuffText = -1;

            return drawBuffText;
        }
    }
}