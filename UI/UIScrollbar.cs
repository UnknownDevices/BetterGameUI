using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIScrollbar : UIBasic
    {
        // TODO: consider moving some of these fields to UIScroller
        // TODO: consider a similar field for the scroller
        // represents a click that started with the cursor inside this UI and has not been released nor has the cursor left the UI
        public bool HoldingLeftMouse;
        public int CornerHeight;
        public uint ScrolledNotches;
        public uint MaxScrollNotches;
        public float ScrollerDraggingPointY;

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

        public override void Draw(SpriteBatch spriteBatch) {
            if (IsEnabled) {
                if (HoldingLeftMouse) {
                    if (!PlayerInput.Triggers.Current.MouseLeft || !IsHovered()) {
                        HoldingLeftMouse = false;
                    }
                } else if (PlayerInput.Triggers.JustPressed.MouseLeft && IsHovered()) {
                    HoldingLeftMouse = true;
                }

                long scrolledNotchesBeforeClamp = ScrolledNotches;

                // TODO: refactor
                float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
                if (IsDraggingScrollerAllowed()) {
                    if (!float.IsNaN(ScrollerDraggingPointY)) {
                        if (PlayerInput.Triggers.Current.MouseLeft) {
                            float draggedDistInPxs = mouseY - UIScroller.GetDimensions().Y - ScrollerDraggingPointY;

                            if (draggedDistInPxs != 0) {
                                if (ScrolledNotches <= 0 && draggedDistInPxs < 0) {
                                    ScrollerDraggingPointY = Math.Max(mouseY - UIScroller.GetDimensions().Y, 0);
                                }
                                else if (MaxScrollNotches <= ScrolledNotches && 0 < draggedDistInPxs) {
                                    ScrollerDraggingPointY = Math.Min(mouseY - UIScroller.GetDimensions().Y,
                                        UIScroller.GetDimensions().Height);
                                }

                                scrolledNotchesBeforeClamp += (long)Math.Round(
                                    draggedDistInPxs / (GetInnerDimensions().Height / (MaxScrollNotches + 1)));
                            }
                        }
                        else {
                            ScrollerDraggingPointY = float.NaN;
                        }
                    }
                    else if (PlayerInput.Triggers.JustPressed.MouseLeft && IsScrollerHitboxHovered()) {
                        ScrollerDraggingPointY = (float)Math.Clamp(mouseY - UIScroller.GetDimensions().Y, 0,
                            UIScroller.GetDimensions().Height);
                    }
                    else if (HoldingLeftMouse && AllowScrollerSnappingToCursor()) { 
                        if (0 < mouseY - UIScroller.GetDimensions().Y) {
                            scrolledNotchesBeforeClamp++;
                        }
                        else if (mouseY - UIScroller.GetDimensions().Y < 0) {
                            scrolledNotchesBeforeClamp--;
                        }
                    }
                }
                else {
                    ScrollerDraggingPointY = float.NaN;
                }

                if (IsMouseScrollFocusingThis() && float.IsNaN(ScrollerDraggingPointY)) {
                    scrolledNotchesBeforeClamp += MouseScroll();
                }

                ScrolledNotches = (uint)Math.Clamp(scrolledNotchesBeforeClamp, 0, MaxScrollNotches);

                var scrollerCalculatedMinHeight = UIScroller.MinHeight.GetValue(GetInnerDimensions().Height);
                var scrollerCalculatedMaxHeight = UIScroller.MaxHeight.GetValue(GetInnerDimensions().Height);
                UIScroller.Height = (GetInnerDimensions().Height <= 0) ? StyleDimension.FromPixels(0f) :
                    StyleDimension.FromPixels((float)Math.Clamp(
                        Math.Ceiling(GetInnerDimensions().Height / (MaxScrollNotches + 1)),
                        scrollerCalculatedMinHeight, scrollerCalculatedMaxHeight));

                var scrollerMovementRange = GetInnerDimensions().Height - UIScroller.Height.Pixels;
                float pxsPerNotch = (scrollerMovementRange <= 0 || MaxScrollNotches <= 0) ? 0 :
                    scrollerMovementRange / MaxScrollNotches;
                UIScroller.Top = StyleDimension.FromPixels((float)Math.Round(pxsPerNotch * ScrolledNotches));

                UIScroller.Recalculate();

                if (HoldingLeftMouse && IsScrollerHitboxHovered()) {
                    ScrollerDraggingPointY = (float)Math.Clamp(mouseY - UIScroller.GetDimensions().Y, 0,
                            UIScroller.GetDimensions().Height);
                }
                if (!float.IsNaN(ScrollerDraggingPointY) || IsScrollerHitboxHovered() |
                    (Mod.ClientConfig.AllowScrollerSnappingToCursor && IsHovered())) 
                {
                    Main.player[Main.myPlayer].mouseInterface = true;
                }
            }
            else {
                ScrolledNotches = Math.Clamp(ScrolledNotches, 0, MaxScrollNotches);
                ScrollerDraggingPointY = float.NaN;
                HoldingLeftMouse = false;
            }

            base.Draw(spriteBatch);
        }

        public virtual bool IsMouseScrollFocusingThis() {
            return true;
        }

        public virtual bool IsDraggingScrollerAllowed() {
            return true;
        }

        public virtual bool AllowScrollerSnappingToCursor() {
            return true;
        }

        public virtual int MouseScroll() {
            return -(PlayerInput.ScrollWheelDeltaForUI / 120);
        }

        public virtual bool IsScrollerHitboxHovered() {
            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            // TODO: instead of a hard coded 4, the width should be set to be the same as this UI's
            return UIScroller.GetDimensions().GrowFromCenter(4, 0).Contains(mouseX, mouseY);
        }

        public virtual bool IsHovered() {
            return IsMouseHovering;
        }
    }
}