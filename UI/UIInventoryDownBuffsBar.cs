using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    // TODO: reformat
    public class UIInventoryDownBuffsBar : UIBuffsBar 
    {
        // TODO: consider decoupling 'Mod.ClienConfig'
        public UIInventoryDownBuffsBar() {
            Left = StyleDimension.FromPixels(32 - ScrollbarReservedWidth);
            Top = StyleDimension.FromPixels(76);
        }
         
        public override void UpdateBeforeDraw() {
            UIScrollbar.IsVisible &= !Mod.ClientConfig.SmartHideScrollbar | 0 < UIScrollbar.MaxScrollNotches;

            base.UpdateBeforeDraw();

            if (UIScrollbar.IsMouseScrollFocusingThis()) {
                PlayerInput.LockVanillaMouseScroll("GameBuffIconsBarUI");
            }
        }

        public override bool IsLocked() {
            return Mod.ClientConfig.GameHotbarLockingAlsoLocksThis & Main.player[Main.myPlayer].hbLocked ||
                base.IsLocked();
        }

        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.GameIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.GameIconColsCount;
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            ScrollbarPosition = Mod.ClientConfig.GameScrollbarRelPosition;
            IconsHorOrder = Mod.ClientConfig.GameIconsHorOrder;
            HitboxModifier = Mod.ClientConfig.BuffIconsBarHitboxModifier;

            switch (ScrollbarPosition) {
                case ScrollbarPosition.LeftOfIcons:
                    UIScrollbar.Left = StyleDimension.FromPixels(0f);
                    break;
                case ScrollbarPosition.RightOfIcons:
                    UIScrollbar.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            UIScrollbar.UIScroller.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinimalScrollerHeight);
            UIScrollbar.ScrollerHitboxModifier = Mod.ClientConfig.ScrollerHitboxModifier;
        }

        public override void HandleClientConfigChanged() {
            UpdateClientConfigDependencies();
            Recalculate();
        }
    }
}