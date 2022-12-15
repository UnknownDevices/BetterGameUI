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

        public UIScroller ScrollerUI {
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
            var scrollerCalculatedMinHeight = ScrollerUI.MinHeight.GetValue(GetInnerDimensions().Height);
            var scrollerCalculatedMaxHeight = ScrollerUI.MaxHeight.GetValue(GetInnerDimensions().Height);

            var scrollerHitbox = ScrollerUI.GetDimensions();
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
                        float draggedDistanceInPxs = mouseY - ScrollerUI.GetDimensions().Y - ScrollerDraggingPointY;

                        if (draggedDistanceInPxs != 0) {
                            if (ScrolledNotches <= 0 & draggedDistanceInPxs < 0) {
                                ScrollerDraggingPointY = Math.Max(mouseY - ScrollerUI.GetDimensions().Y, 0);
                            }
                            else if (MaxScrollNotches <= ScrolledNotches & 0 < draggedDistanceInPxs) {
                                ScrollerDraggingPointY = Math.Min(mouseY - ScrollerUI.GetDimensions().Y,
                                    ScrollerUI.GetDimensions().Height);
                            }

                            scrolledNotchesBeforeClamp += (long)Math.Round(
                                draggedDistanceInPxs / (GetInnerDimensions().Height / (MaxScrollNotches + 1)));
                        }
                    }
                    else {
                        IsScrollerBeingDragged = false;
                    }
                }
                else if (PlayerInput.Triggers.JustPressed.MouseLeft & IsMouseHoveringScrollerHitbox) {
                    IsScrollerBeingDragged = true;
                    ScrollerDraggingPointY = (float)Math.Clamp(mouseY - ScrollerUI.GetDimensions().Y, 0,
                        ScrollerUI.GetDimensions().Height);
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

            ScrollerUI.Height = (GetInnerDimensions().Height == 0) ?
                StyleDimension.FromPixels(0f) :
                ScrollerUI.Height = StyleDimension.FromPixels((float)Math.Clamp(
                    Math.Ceiling(GetInnerDimensions().Height / (MaxScrollNotches + 1)),
                    scrollerCalculatedMinHeight, scrollerCalculatedMaxHeight));

            float pxsPerScroll = (GetInnerDimensions().Height - ScrollerUI.Height.Pixels == 0) ?
                0 :
                (GetInnerDimensions().Height - ScrollerUI.Height.Pixels) / MaxScrollNotches;

            ScrollerUI.Top = StyleDimension.FromPixels((float)Math.Round(pxsPerScroll * ScrolledNotches));
            ScrollerUI.Recalculate();
        }
    }
}