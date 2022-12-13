using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class ScrollerUI : UIState
    {
        public bool IsVisible = true;
        public bool IsMouseHoveringHitbox;
        public bool IsBeingDragged;
        public int CornerHeight;
        public int HitboxModifier;
        public float DraggingPointY;
        public float Alpha;

        public override void Draw(SpriteBatch spriteBatch) {
            if (IsVisible) {
                base.Draw(spriteBatch);
            }

            IsVisible = true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            var texture = Assets.Scroller.Value;
            var rec = GetDimensions().ToRectangle();

            float alpha = Alpha;
            if (((Parent as ScrollbarUI).IsDraggingScrollerAllowed & IsMouseHoveringHitbox) | IsBeingDragged) {
                // TODO: mod by percent of current alpha instead?
                alpha = Math.Min(Alpha + 0.5f, 1f);
            }

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