using Microsoft.Xna.Framework;
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
            Alpha = 0.5f;

            Append(new UIScroller
            {
                Top = StyleDimension.FromPixels(0f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(6f),
                Height = StyleDimension.FromPixels(8f),
                CornerHeight = 2,
                Alpha = 0.5f,
            });
        }

        public override bool IsMouseScrollFocusingThis() {
            return Player.MouseScrollIsFocusingBuffIconsBar | 
                (Mod.ClientConfig.MouseScrollFocusesMouseHoveredUI & UIBuffsBar.IsMouseHoveringHitbox()) && 
                base.IsMouseScrollFocusingThis();
        }

        public override bool IsDraggingScrollerAllowed() {
            return Mod.ClientConfig.AllowScrollerDragging & !UIBuffsBar.IsLocked() && 
                base.IsDraggingScrollerAllowed();
        }

        public override int MouseScroll() {
            int output = 0;

            // TODO: should also scroll if holding key? if so, after what delay after pressing, maybe make that a config
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

        public override void Update(GameTime gameTime) {
            if (Mod.ActiveBuffsIndexes.Count <= 0) {
                MaxScrollNotches = 0;
            }
            else {
                MaxScrollNotches = (uint)Math.Max(
                    Math.Ceiling((double)Mod.ActiveBuffsIndexes.Count /
                    (double)UIBuffsBar.IconColsCount) - UIBuffsBar.IconRowsCount, 0);
            }

            IsActive &= !Mod.ClientConfig.SmartHideScrollbar | 0 < MaxScrollNotches;

            if (IsActive) {
                if (IsMouseScrollFocusingThis()) {
                    PlayerInput.LockVanillaMouseScroll("UIInventoryUpBuffsBar");
                }
            }

            base.Update(gameTime);
        }
    }
}