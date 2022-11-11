﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameInput;

namespace BetterGameUI.UI {
    // TODO: refactor and generalize to TModLoaderExtensions or something
    public static class CalculatedStyleExtensions {
        public static bool Contains(this CalculatedStyle self, float x, float y) {
            if (self.X <= x && x < self.X + self.Width && self.Y <= y) {
                return y < self.Y + self.Height;
            }

            return false;
        }
    }
    
    public class ScrollbarUI : UIElement {
        public ScrollerUI ScrollerUI { 
            get => Elements[0] as ScrollerUI;
            set => Elements[0] = value;
        }
        public int CornerHeight { get; set; }
        public uint Scrolls { get; set; }
        public uint MaxScrolls { get; set; }
        public bool IsVisible { get; set; }
        public bool IsMouseScrollAllowed { get; set; }
        public bool IsDraggingScrollerAllowed { get; set; }
        public float Alpha { get; set; }

        public override void Draw(SpriteBatch spriteBatch) {
            if (IsVisible) {
                base.Draw(spriteBatch);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            var texture = Assets.Scrollbar.Value;
            var rec = GetDimensions().ToRectangle();
            var color = new Color(Alpha, Alpha, Alpha, Alpha);

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

        public override void Update(GameTime gameTime) {
            var scrollerCalculatedMinHeight = ScrollerUI.MinHeight.GetValue(GetInnerDimensions().Height);
            var scrollerCalculatedMaxHeight = ScrollerUI.MaxHeight.GetValue(GetInnerDimensions().Height);

            var scrollerHitbox = ScrollerUI.GetDimensions();
            if (ScrollerUI.HitboxWidthModifier != 0) {
                scrollerHitbox.X -= (float)ScrollerUI.HitboxWidthModifier / 2;
                scrollerHitbox.Width += ScrollerUI.HitboxWidthModifier;
            }
            if (ScrollerUI.HitboxHeightModifier != 0) {
                scrollerHitbox.Y -= (float)ScrollerUI.HitboxHeightModifier / 2;
                scrollerHitbox.Height += ScrollerUI.HitboxHeightModifier;
            }

            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            ScrollerUI.IsMouseHoveringHitbox = scrollerHitbox.Contains(mouseX, mouseY);

            long scrollsBeforeClamp = Scrolls;

            if (IsDraggingScrollerAllowed & IsVisible) {
                if (ScrollerUI.IsBeingDragged) {
                    if (PlayerInput.Triggers.Current.MouseLeft) {
                        float draggedDistanceInPxs = mouseY - ScrollerUI.GetDimensions().Y - ScrollerUI.DraggingPointY;

                        if (draggedDistanceInPxs != 0) {
                            if (Scrolls <= 0 & draggedDistanceInPxs < 0) {
                                ScrollerUI.DraggingPointY = Math.Max(mouseY - ScrollerUI.GetDimensions().Y, 0);
                            }
                            else if (MaxScrolls <= Scrolls & 0 < draggedDistanceInPxs) {
                                ScrollerUI.DraggingPointY = Math.Min(mouseY - ScrollerUI.GetDimensions().Y,
                                    ScrollerUI.GetDimensions().Height);
                            }

                            scrollsBeforeClamp += (long)Math.Round(
                                draggedDistanceInPxs / (GetInnerDimensions().Height / (MaxScrolls + 1)));
                        }
                    }
                    else {
                        ScrollerUI.IsBeingDragged = false;
                    }
                }
                else if (PlayerInput.Triggers.JustPressed.MouseLeft & ScrollerUI.IsMouseHoveringHitbox) {
                    ScrollerUI.IsBeingDragged = true;
                    ScrollerUI.DraggingPointY = (float)Math.Clamp(mouseY - ScrollerUI.GetDimensions().Y, 0,
                        ScrollerUI.GetDimensions().Height);
                }

                if (ScrollerUI.IsBeingDragged | ScrollerUI.IsMouseHoveringHitbox) {
                    Main.player[Main.myPlayer].mouseInterface = true;
                }
            }
            else {
                ScrollerUI.IsBeingDragged = false;
            }

            if (IsMouseScrollAllowed & (!IsDraggingScrollerAllowed | !ScrollerUI.IsBeingDragged)) {
                scrollsBeforeClamp += -(PlayerInput.ScrollWheelDeltaForUI / 120);
            }

            Scrolls = (uint)Math.Clamp(scrollsBeforeClamp, 0, MaxScrolls);

            ScrollerUI.Height = (GetInnerDimensions().Height == 0) ?
                StyleDimension.FromPixels(0f) :
                ScrollerUI.Height = StyleDimension.FromPixels((float)Math.Clamp(
                    Math.Ceiling(GetInnerDimensions().Height / (MaxScrolls + 1)),
                    scrollerCalculatedMinHeight, scrollerCalculatedMaxHeight));

            float pxsPerScroll = (GetInnerDimensions().Height - ScrollerUI.Height.Pixels == 0) ?
                0 :
                (GetInnerDimensions().Height - ScrollerUI.Height.Pixels) / MaxScrolls;

            ScrollerUI.Top = StyleDimension.FromPixels((float)Math.Round(pxsPerScroll * Scrolls));
            ScrollerUI.Recalculate();

            base.Update(gameTime);
        }
    }
}