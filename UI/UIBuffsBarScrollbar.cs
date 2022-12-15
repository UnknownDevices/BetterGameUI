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
    }
}