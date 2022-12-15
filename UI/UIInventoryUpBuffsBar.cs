using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIInventoryUpBuffsBar : UIBuffsBar
    {
        // TODO: scrollbar shouldn't be accounted within this object dimensions
        public override void UpdateBeforeDraw() {
            UIScrollbar.IsVisible &= !Mod.ClientConfig.SmartHideScrollbar | 0 < UIScrollbar.MaxScrollNotches;

            base.UpdateBeforeDraw();

            // TODO: do this in UIScrollbar
            if (UIScrollbar.IsMouseScrollFocusingThis()) {
                PlayerInput.LockVanillaMouseScroll("InventoryBuffIconsBarUI");
            }

            UIScrollbar.ExtraMouseScroll += Player.ExtraMouseScrollForUI;
        }

        // TODO: move to UIBuffsBar what be moved
        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryIconColsCount;
            Left = StyleDimension.FromPixelsAndPercent(-84 - 38 * (IconColsCount - 1), 1f);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Top = StyleDimension.FromPixelsAndPercent(421, 1f);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            HitboxModifier = Mod.ClientConfig.BuffIconsBarHitboxModifier;
            ScrollbarPosition = Mod.ClientConfig.InventoryScrollbarRelPosition;
            IconsHorOrder = Mod.ClientConfig.InventoryIconsHorOrder;

            switch (ScrollbarPosition) {
                case ScrollbarPosition.LeftOfIcons:
                    UIScrollbar.Left = StyleDimension.FromPixels(0f);
                    break;

                case ScrollbarPosition.RightOfIcons:
                    UIScrollbar.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            UIScrollbar.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinimalScrollerHeight);
            UIScrollbar.ScrollerHitboxModifier = Mod.ClientConfig.ScrollerHitboxModifier;
        }

        public override void HandleClientConfigChanged() {
            UpdateClientConfigDependencies();
            Recalculate();
        }
    }
}