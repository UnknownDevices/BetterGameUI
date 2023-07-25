using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    // TODO: consider making overridable methods protected
    public abstract class Scrollbar
    {
        public const int BarCornerHeight = 4;
        public const int ScrollerCornerHeight = 2;
        public const uint BarMinHeight = 10;
        public const uint ScrollerMinHeight = 6;

        public CalculatedStyle BarDimensions;
        public CalculatedStyle ScrollerDimensions;
        public bool IsBarHovered => BarDimensions.GrowFromCenter(4).Contains(Player.MouseX, Player.MouseY);
        public bool IsScrollerHovered => ScrollerDimensions.GrowFromCenter(4).Contains(Player.MouseX, Player.MouseY);
        public virtual bool IsVisible => true;
        public virtual bool CanScrollerBeDragged => true;
        public virtual bool CanListenToWheelScroll => true;
        public virtual bool InvertedMouseScroll => false;
        public virtual float BarAlpha => 0.5f;
        public virtual float ScrollerMinAlpha => 0.5f;
        public virtual float ScrollerMaxAlpha => 1f;
        public virtual uint NotchesCount => 1;
        public virtual uint NotchesPerPage => 1;
        public bool IsScrollerBeingDragged { get; set; }
        public bool CurrentMouseLeftBegunInScrollbar { get; set; }
        public float ScrollerAlpha { get; set; }
        public uint ScrolledNotches { get; set; }

        public virtual void Update() {
            if (!IsVisible) {
                IsScrollerBeingDragged = false;
                CurrentMouseLeftBegunInScrollbar = false;
                ScrollerAlpha = ScrollerMinAlpha;
                return;
            }

            if (CurrentMouseLeftBegunInScrollbar) {
                if (!PlayerInput.Triggers.Current.MouseLeft) {
                    CurrentMouseLeftBegunInScrollbar = false;
                }
            }
            else if (PlayerInput.Triggers.JustPressed.MouseLeft && IsBarHovered) {
                CurrentMouseLeftBegunInScrollbar = true;
            }

            ScrollerDimensions.Height = (BarDimensions.Height <= 0)
                ? 0f : (float)Math.Clamp(Math.Ceiling(
                    BarDimensions.Height / NotchesCount * NotchesPerPage),
                    ScrollerMinHeight, BarDimensions.Height);

            ScrollerDimensions.X = BarDimensions.X + 2F;

            long unclampedScrolledNotches = ScrolledNotches;
            unclampedScrolledNotches += HandleScrollerDragging();
            unclampedScrolledNotches += HandleWheelScroll();
            ScrolledNotches = (uint)Math.Clamp(unclampedScrolledNotches, 0, NotchesCount - NotchesPerPage);

            // Don't divide by 0
            var scrollerMovementRange = BarDimensions.Height - ScrollerDimensions.Height;
            float pxsPerNotch = (scrollerMovementRange <= 0 || NotchesCount - NotchesPerPage <= 0)
                ? 0 : scrollerMovementRange / (NotchesCount - NotchesPerPage);

            ScrollerDimensions.Y = BarDimensions.Y + (float)Math.Round(pxsPerNotch * ScrolledNotches);

            if (IsBarHovered && (!PlayerInput.Triggers.Current.MouseLeft || CurrentMouseLeftBegunInScrollbar)) {
                Main.LocalPlayer.mouseInterface = true;
            }

            ScrollerAlpha += CanScrollerBeDragged && (IsScrollerHovered || IsScrollerBeingDragged)
                ? 0.1f : -0.05f;
            ScrollerAlpha = Math.Clamp(ScrollerAlpha, ScrollerMinAlpha, ScrollerMaxAlpha);

            Draw(Assets.ScrollbarBar.Value, BarDimensions.ToRectangle(), BarAlpha, BarCornerHeight);
            Draw(Assets.ScrollbarScroller.Value, ScrollerDimensions.ToRectangle(), ScrollerAlpha, ScrollerCornerHeight);
        }

        private long HandleWheelScroll() {
            if (!CanListenToWheelScroll || IsScrollerBeingDragged) {
                return 0;
            }

            return InvertedMouseScroll ? -Player.Scroll : Player.Scroll;
        }

        private long HandleScrollerDragging() {
            if (CanScrollerBeDragged) {
                if (IsScrollerBeingDragged) {
                    if (!PlayerInput.Triggers.Current.MouseLeft) {
                        IsScrollerBeingDragged = false;
                        return 0;
                    }

                    float draggedDistInPxs;
                    if (Player.MouseY < ScrollerDimensions.Y) {
                        draggedDistInPxs = Player.MouseY - ScrollerDimensions.Y;
                    }
                    else if (ScrollerDimensions.Y + ScrollerDimensions.Height < Player.MouseY) {
                        draggedDistInPxs = Player.MouseY - ScrollerDimensions.Y - ScrollerDimensions.Height;
                    }
                    else {
                        return 0;
                    }

                    // Don't divide by 0
                    return draggedDistInPxs == 0 ? 0 : (long)Math.Round(draggedDistInPxs / BarDimensions.Height * NotchesCount);
                }
                if (PlayerInput.Triggers.Current.MouseLeft && IsScrollerHovered) {
                    IsScrollerBeingDragged = true;
                    return 0;
                }
                if (CurrentMouseLeftBegunInScrollbar && IsBarHovered) {
                    if (Player.MouseY < ScrollerDimensions.Y) {
                        return -1;
                    }
                    if (ScrollerDimensions.Y + ScrollerDimensions.Height < Player.MouseY) {
                        return 1;
                    }
                    IsScrollerBeingDragged = true;
                    return 0;
                }
            }

            IsScrollerBeingDragged = false;
            return 0;
        }

        private static void Draw(Texture2D texture, Rectangle rec, float alpha, int cornerHeight) {
            var color = new Color(alpha, alpha, alpha, alpha);
            Main.spriteBatch.Draw(texture,
                new Rectangle(rec.X, rec.Y, rec.Width, cornerHeight),
                new Rectangle(0, 0, rec.Width, cornerHeight),
                    color);
            Main.spriteBatch.Draw(texture,
                new Rectangle(
                    rec.X, rec.Y + cornerHeight,
                    rec.Width, rec.Height - cornerHeight * 2),
            new Rectangle(
            0, cornerHeight,
                    rec.Width, texture.Height - cornerHeight * 2),
                    color);
            Main.spriteBatch.Draw(texture,
                    new Rectangle(
                    rec.X, rec.Y + rec.Height - cornerHeight,
                    rec.Width, cornerHeight),
            new Rectangle(
                    0, texture.Height - cornerHeight,
                    rec.Width, cornerHeight),
                    color);
        }
    }
}