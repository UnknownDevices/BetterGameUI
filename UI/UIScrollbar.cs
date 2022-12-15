using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIScrollbar : UIState
    {
        public bool IsVisible = true;
        // TODO: make into functions override functionality with inheritance
        public bool IsMouseHoveringScrollerHitbox = false;
        public bool IsScrollerBeingDragged = false;
        public int ScrollerHitboxModifier;
        public int CornerHeight;
        public uint ScrolledNotches;
        public uint MaxScrollNotches;
        // TODO: consider using float.NaN to represent scroller not being dragged
        public float ScrollerDraggingPointY;
        public float Alpha;

        public UIScroller UIScroller {
            get => Elements[0] as UIScroller;
            set => Elements[0] = value;
        }

        public virtual bool IsMouseScrollFocusingThis() {
            return true;
        }

        public virtual bool IsDraggingScrollerAllowed() {
            return IsVisible;
        }

        public virtual int MouseScroll() {
            return -(PlayerInput.ScrollWheelDeltaForUI / 120);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            UpdateBeforeDraw();

            if (IsVisible) {
                base.Draw(spriteBatch);
            }

            IsVisible = true;
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

        // TODO: have scroller snap to mouse position when scrollbar is left clicked and scroller dragging is allowed
        public virtual void UpdateBeforeDraw() {
            var scrollerCalculatedMinHeight = UIScroller.MinHeight.GetValue(GetInnerDimensions().Height);
            var scrollerCalculatedMaxHeight = UIScroller.MaxHeight.GetValue(GetInnerDimensions().Height);

            var scrollerHitbox = UIScroller.GetDimensions();
            if (ScrollerHitboxModifier != 0) {
                scrollerHitbox.X -= (float)ScrollerHitboxModifier / 2;
                scrollerHitbox.Y -= (float)ScrollerHitboxModifier / 2;
                scrollerHitbox.Width += ScrollerHitboxModifier;
                scrollerHitbox.Height += ScrollerHitboxModifier;
            }

            // TODO: consider if the unrounded values are truly necessary
            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            IsMouseHoveringScrollerHitbox = scrollerHitbox.Contains(mouseX, mouseY);

            long scrolledNotchesBeforeClamp = ScrolledNotches;

            if (IsDraggingScrollerAllowed()) {
                if (IsScrollerBeingDragged) {
                    if (PlayerInput.Triggers.Current.MouseLeft) {
                        float draggedDistInPxs = mouseY - UIScroller.GetDimensions().Y - ScrollerDraggingPointY;

                        if (draggedDistInPxs != 0) {
                            if (ScrolledNotches <= 0 & draggedDistInPxs < 0) {
                                ScrollerDraggingPointY = Math.Max(mouseY - UIScroller.GetDimensions().Y, 0);
                            }
                            else if (MaxScrollNotches <= ScrolledNotches & 0 < draggedDistInPxs) {
                                ScrollerDraggingPointY = Math.Min(mouseY - UIScroller.GetDimensions().Y,
                                    UIScroller.GetDimensions().Height);
                            }

                            scrolledNotchesBeforeClamp += (long)Math.Round(
                                draggedDistInPxs / (GetInnerDimensions().Height / (MaxScrollNotches + 1)));
                        }
                    }
                    else {
                        IsScrollerBeingDragged = false;
                    }
                }
                else if (PlayerInput.Triggers.JustPressed.MouseLeft & IsMouseHoveringScrollerHitbox) {
                    IsScrollerBeingDragged = true;
                    ScrollerDraggingPointY = (float)Math.Clamp(mouseY - UIScroller.GetDimensions().Y, 0,
                        UIScroller.GetDimensions().Height);
                }

                if (IsScrollerBeingDragged | IsMouseHoveringScrollerHitbox) {
                    Main.player[Main.myPlayer].mouseInterface = true;
                }
            }
            else {
                IsScrollerBeingDragged = false;
            }

            // TODO: consider if !IsScrollerBeingDragged is necessary
            if (IsMouseScrollFocusingThis() & (!IsDraggingScrollerAllowed() | !IsScrollerBeingDragged)) {
                scrolledNotchesBeforeClamp += MouseScroll();
            }

            ScrolledNotches = (uint)Math.Clamp(scrolledNotchesBeforeClamp, 0, MaxScrollNotches);

            UIScroller.Height = (GetInnerDimensions().Height == 0) ?
                StyleDimension.FromPixels(0f) :
                    UIScroller.Height = StyleDimension.FromPixels((float)Math.Clamp(
                    Math.Ceiling(GetInnerDimensions().Height / (MaxScrollNotches + 1)),
                    scrollerCalculatedMinHeight, scrollerCalculatedMaxHeight));

            float pxsPerNotch = (GetInnerDimensions().Height - UIScroller.Height.Pixels == 0) ?
                0 :
                (GetInnerDimensions().Height - UIScroller.Height.Pixels) / MaxScrollNotches;

            UIScroller.Top = StyleDimension.FromPixels((float)Math.Round(pxsPerNotch * ScrolledNotches));
            UIScroller.Recalculate();
        }
    }
}