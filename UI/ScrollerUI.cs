﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace BetterGameUI.UI {
    public class ScrollerUI : UIElement {
        public int CornerHeight { get; set; }
        public bool IsVisible { get; set; }
        public bool IsMouseHoveringHitbox { get; set; }
        public bool IsBeingDragged { get; set; }
        public float DraggingPointY { get; set; }
        public float HitboxWidthModifier { get; set; }
        public float HitboxHeightModifier { get; set; }
        public float Alpha { get; set; }

        public override void Draw(SpriteBatch spriteBatch) {
            if (!IsVisible) {
                base.Draw(spriteBatch);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            var texture = Assets.Scroller.Value;
            var rec = GetDimensions().ToRectangle();

            float alpha = Alpha;
            if (IsMouseHoveringHitbox | IsBeingDragged) {
                // TODO: mod by % of current alpha??
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