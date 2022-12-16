using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIScroller : UIState
    {
        public bool IsActive = true;
        public int CornerHeight;
        public float Alpha;
        
        public UIScrollbar UIScrollbar => Parent as UIScrollbar;

        public override void Draw(SpriteBatch spriteBatch) {
            if (IsActive) {
                base.Draw(spriteBatch);
            }

            IsActive = true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            var texture = Assets.Scroller.Value;
            var rec = GetDimensions().ToRectangle();

            float alpha = Alpha;
            if ((UIScrollbar.IsDraggingScrollerAllowed() & UIScrollbar.IsMouseHoveringScrollerHitbox()) | 
                UIScrollbar.IsScrollerBeingDragged)
            {
                // TODO: consider doing mod by percent of default alpha instead
                alpha = Math.Min(Alpha + 0.5f, 1f);
            }

            // TODO: extract method
            var color = new Color(alpha, alpha, alpha, alpha);

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