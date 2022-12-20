using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIBuffsBarScrollbar : UIScrollbar
    {
        public UIBuffsBar UIBuffsBar => Parent as UIBuffsBar;

        public UIBuffsBarScrollbar() {
            Top = StyleDimension.FromPixels(2f);
            Width = StyleDimension.FromPixels(10f);
            Height = StyleDimension.FromPixelsAndPercent(-16f, 1f);
            CornerHeight = 4;

            // TODO: do this in a custom UIScroller class
            Append(new UIScroller
            {
                Top = StyleDimension.FromPixels(0f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(6f),
                Height = StyleDimension.FromPixels(8f),
                CornerHeight = 2,
            });
        }

        public override bool IsMouseScrollFocusingThis() {
            return Player.MouseScrollIsFocusingBuffsBar | 
                (Mod.ClientConfig.MouseScrollFocusesMouseHoveredUI & UIBuffsBar.IsMouseHoveringHitbox()) && 
                base.IsMouseScrollFocusingThis();
        }

        public override bool IsDraggingScrollerAllowed() {
            return Mod.ClientConfig.AllowScrollerDragging & !UIBuffsBar.IsLocked() && 
                base.IsDraggingScrollerAllowed();
        }

        public override int MouseScroll() {
            int output = 0;

            if (PlayerInput.Triggers.JustPressed.MouseXButton1) {
                output += 1;
            }
            if (PlayerInput.Triggers.JustPressed.MouseXButton2) {
                output -= 1;
            }

            output += base.MouseScroll();
            return Mod.ClientConfig.InvertMouseScrollForScrollbar ? -output : output;
        }

        public override bool IsMouseHoveringScrollerHitbox() {
            float mouseX = PlayerInput.MouseInfo.X / Main.UIScale;
            float mouseY = PlayerInput.MouseInfo.Y / Main.UIScale;
            return UIScroller.GetDimensions().GrowFromCenter(Mod.ClientConfig.ScrollerHitboxMod).
                Contains(mouseX, mouseY);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            MaxScrollNotches = (Mod.ActiveBuffsIndexes.Count <= 0) ? 0 : 
                (uint)Math.Max(Math.Ceiling((double)Mod.ActiveBuffsIndexes.Count / (double)UIBuffsBar.IconColsCount) - 
                    UIBuffsBar.IconRowsCount, 0);

            MaybeDisable(Mod.ClientConfig.SmartHideScrollbar & MaxScrollNotches <= 0);

            if (IsEnabled) {
                if (IsMouseScrollFocusingThis()) {
                    PlayerInput.LockVanillaMouseScroll("UIBuffsBarScrollbar");
                }
            }

            base.Draw(spriteBatch);
        }
    }
}