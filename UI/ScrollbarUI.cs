using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class ScrollbarUI : UIState
    {
        public bool IsVisible = true;
        public bool IsMouseScrollFocusingThis = true;
        public bool IsDraggingScrollerAllowed = true;
        public int ExtraMouseScroll;
        public int CornerHeight;
        public uint ScrolledDist;
        public uint ScrollableDist;
        public float Alpha;
        public ScrollerUI ScrollerUI {
            get => Elements[0] as ScrollerUI;
            set => Elements[0] = value;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            UpdateBeforeDraw();

            if (IsVisible) {
                base.Draw(spriteBatch);
            }

            ExtraMouseScroll = 0;
            IsVisible = true;
            IsMouseScrollFocusingThis = true;
            IsDraggingScrollerAllowed = true;
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
            if (ScrollerUI.HitboxModifier != 0) {
                scrollerHitbox.X -= (float)ScrollerUI.HitboxModifier / 2;
                scrollerHitbox.Y -= (float)ScrollerUI.HitboxModifier / 2;
                scrollerHitbox.Width += ScrollerUI.HitboxModifier;
                scrollerHitbox.Height += ScrollerUI.HitboxModifier;
            }

            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            ScrollerUI.IsMouseHoveringHitbox = scrollerHitbox.Contains(mouseX, mouseY);

            long scrolledDistBeforeClamp = ScrolledDist;

            if (IsDraggingScrollerAllowed & IsVisible) {
                if (ScrollerUI.IsBeingDragged) {
                    if (PlayerInput.Triggers.Current.MouseLeft) {
                        float draggedDistanceInPxs = mouseY - ScrollerUI.GetDimensions().Y - ScrollerUI.DraggingPointY;

                        if (draggedDistanceInPxs != 0) {
                            if (ScrolledDist <= 0 & draggedDistanceInPxs < 0) {
                                ScrollerUI.DraggingPointY = Math.Max(mouseY - ScrollerUI.GetDimensions().Y, 0);
                            }
                            else if (ScrollableDist <= ScrolledDist & 0 < draggedDistanceInPxs) {
                                ScrollerUI.DraggingPointY = Math.Min(mouseY - ScrollerUI.GetDimensions().Y,
                                    ScrollerUI.GetDimensions().Height);
                            }

                            scrolledDistBeforeClamp += (long)Math.Round(
                                draggedDistanceInPxs / (GetInnerDimensions().Height / (ScrollableDist + 1)));
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

            if (IsMouseScrollFocusingThis & (!IsDraggingScrollerAllowed | !ScrollerUI.IsBeingDragged)) {
                scrolledDistBeforeClamp += ExtraMouseScroll + -(PlayerInput.ScrollWheelDeltaForUI / 120);
            }

            ScrolledDist = (uint)Math.Clamp(scrolledDistBeforeClamp, 0, ScrollableDist);

            ScrollerUI.Height = (GetInnerDimensions().Height == 0) ?
                StyleDimension.FromPixels(0f) :
                ScrollerUI.Height = StyleDimension.FromPixels((float)Math.Clamp(
                    Math.Ceiling(GetInnerDimensions().Height / (ScrollableDist + 1)),
                    scrollerCalculatedMinHeight, scrollerCalculatedMaxHeight));

            float pxsPerScroll = (GetInnerDimensions().Height - ScrollerUI.Height.Pixels == 0) ?
                0 :
                (GetInnerDimensions().Height - ScrollerUI.Height.Pixels) / ScrollableDist;

            ScrollerUI.Top = StyleDimension.FromPixels((float)Math.Round(pxsPerScroll * ScrolledDist));
            ScrollerUI.Recalculate();
        }
    }
}