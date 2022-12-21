using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIScroller : UIBasic
    {
        public int CornerHeight;
        public float DynamicAlpha = 0f;

        public UIScrollbar UIScrollbar => Parent as UIScrollbar;

        // TODO: should this light up when scrolling?
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            if ((UIScrollbar.IsDraggingScrollerAllowed() & UIScrollbar.IsScrollerHitboxHovered()) |
                !float.IsNaN(UIScrollbar.ScrollerDraggingPointY)) 
            {
                DynamicAlpha += 0.1f;
            }
            else {
                DynamicAlpha -= 0.05f;
            }

            if (DynamicAlpha > 1f) {
                DynamicAlpha = 1f;
            }
            else if (DynamicAlpha < Alpha) {
                DynamicAlpha = Alpha;
            }

            if (IsEnabled) {
                var texture = Assets.Scroller.Value;
                var rec = GetDimensions().ToRectangle();
                var color = new Color(DynamicAlpha, DynamicAlpha, DynamicAlpha, DynamicAlpha);

                spriteBatch.Draw(texture,
                    new Rectangle(rec.X, rec.Y, rec.Width, CornerHeight),
                    new Rectangle(0, 0, rec.Width, CornerHeight),
                        color);
                spriteBatch.Draw(texture,
                    new Rectangle(
                        rec.X, rec.Y + CornerHeight,
                        rec.Width, rec.Height - CornerHeight * 2),
                    new Rectangle(
                        0, CornerHeight,
                        rec.Width, texture.Height - CornerHeight * 2),
                        color);
                spriteBatch.Draw(texture,
                        new Rectangle(
                        rec.X, rec.Y + rec.Height - CornerHeight,
                        rec.Width, CornerHeight),
                    new Rectangle(
                        0, texture.Height - CornerHeight,
                        rec.Width, CornerHeight),
                        color);
            }
        }
    }
}