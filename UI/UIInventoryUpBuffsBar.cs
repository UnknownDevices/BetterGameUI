using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIInventoryUpBuffsBar : UIBuffsBar
    {
        public override void UpdateBeforeDraw() {
            UIScrollbar.IsVisible &= !Mod.ClientConfig.SmartHideScrollbar | 0 < UIScrollbar.MaxScrollNotches;

            base.UpdateBeforeDraw();

            // TODO: do this in UIScrollbar
            if (UIScrollbar.IsMouseScrollFocusingThis()) {
                PlayerInput.LockVanillaMouseScroll("InventoryBuffIconsBarUI");
            }
        }

        // TODO: move to UIBuffsBar what can be moved
        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryUpIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryUpIconColsCount;
            Left = StyleDimension.FromPixelsAndPercent(
                -84 - 38 * (IconColsCount - 1) + Mod.ClientConfig.InventoryUpXOffset, 1f);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Top = StyleDimension.FromPixelsAndPercent(421 + Mod.ClientConfig.InventoryUpYOffset, 1f);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            HitboxModifier = Mod.ClientConfig.BuffsBarHitboxModifier;
            ScrollbarPosition = Mod.ClientConfig.InventoryUpScrollbarRelPosition;
            IconsHorOrder = Mod.ClientConfig.InventoryUpIconsHorOrder;

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