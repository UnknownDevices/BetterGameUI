using Terraria.GameInput;

namespace BetterGameUI.UI
{
    public class UIBuffsBarScrollbar : UIScrollbar
    {
        public UIBuffsBar UIBuffsBar => Parent as UIBuffsBar; 

        public override bool IsMouseScrollFocusingThis() {
            return Player.MouseScrollIsFocusingBuffIconsBar | 
                (Mod.ClientConfig.MouseInputFocusesMouseHoveredUI & UIBuffsBar.IsMouseHoveringHitbox) && 
                base.IsMouseScrollFocusingThis();
        }

        public override bool IsDraggingScrollerAllowed() {
            return Mod.ClientConfig.AllowScrollerDragging & !UIBuffsBar.IsLocked() && 
                base.IsDraggingScrollerAllowed();
        }

        public override int MouseScroll() {
            int output = 0;

            // NOTE: when the inventory is up, vanilla Terraria doesn't listen to MouseX1 or MouseX2
            // TODO: consider renaming MouseScroll to ScrollWheel everywhere
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
    }
}