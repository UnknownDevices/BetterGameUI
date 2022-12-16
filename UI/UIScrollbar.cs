using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIScrollbar : UIBasic
    {
        // TODO: consider moving some of these fields to UIScroller
        public bool IsScrollerBeingDragged = false;
        public int CornerHeight;
        public uint ScrolledNotches;
        public uint MaxScrollNotches;
        // TODO: consider using float.NaN to represent scroller not being dragged
        public float ScrollerDraggingPointY;
        // TODO: consider moving to UIBasic
        public float Alpha;

        public UIScroller UIScroller {
            get => Elements[0] as UIScroller;
            set => Elements[0] = value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            if (!IsEnabled) {
                return;
            }

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
        public override void Update(GameTime gameTime) {
            if (IsEnabled) {
                long scrolledNotchesBeforeClamp = ScrolledNotches;

                if (IsDraggingScrollerAllowed()) {
                    bool isMouseHoveringScrollerHitbox = IsMouseHoveringScrollerHitbox();
                    float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;

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
                    else if (PlayerInput.Triggers.JustPressed.MouseLeft & isMouseHoveringScrollerHitbox) {
                        IsScrollerBeingDragged = true;
                        ScrollerDraggingPointY = (float)Math.Clamp(mouseY - UIScroller.GetDimensions().Y, 0,
                            UIScroller.GetDimensions().Height);
                    }

                    if (IsScrollerBeingDragged | isMouseHoveringScrollerHitbox) {
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

                var scrollerCalculatedMinHeight = UIScroller.MinHeight.GetValue(GetInnerDimensions().Height);
                var scrollerCalculatedMaxHeight = UIScroller.MaxHeight.GetValue(GetInnerDimensions().Height);
                UIScroller.Height = (GetInnerDimensions().Height <= 0) ? StyleDimension.FromPixels(0f) :
                    StyleDimension.FromPixels((float)Math.Clamp(
                        Math.Ceiling(GetInnerDimensions().Height / (MaxScrollNotches + 1)),
                        scrollerCalculatedMinHeight, scrollerCalculatedMaxHeight));

                var scrollerMovementRange = GetInnerDimensions().Height - UIScroller.Height.Pixels;
                float pxsPerNotch = (scrollerMovementRange <= 0 | MaxScrollNotches <= 0) ? 0 : 
                    scrollerMovementRange / MaxScrollNotches;

                ScrolledNotches = (uint)Math.Clamp(scrolledNotchesBeforeClamp, 0, MaxScrollNotches);

                UIScroller.Top = StyleDimension.FromPixels((float)Math.Round(pxsPerNotch * ScrolledNotches));
                UIScroller.Recalculate();
            }
            else {
                ScrolledNotches = Math.Clamp(ScrolledNotches, 0, MaxScrollNotches);
            }

            base.Update(gameTime);
        }

        public virtual bool IsMouseScrollFocusingThis() {
            return true;
        }

        public virtual bool IsDraggingScrollerAllowed() {
            return true;
        }

        public virtual int MouseScroll() {
            return -(PlayerInput.ScrollWheelDeltaForUI / 120);
        }

        // TODO: consider moving to UIScroller
        public virtual bool IsMouseHoveringScrollerHitbox() {
            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            return UIScroller.GetDimensions().Contains(mouseX, mouseY);
        }

    }
}