using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    // TODO: reformat
    public class UIInventoryDownBuffsBar : UIBuffsBar 
    {
        public override void UpdateBeforeDraw() {
            UIScrollbar.IsVisible &= !Mod.ClientConfig.SmartHideScrollbar | 0 < UIScrollbar.MaxScrollNotches;

            base.UpdateBeforeDraw();

            if (UIScrollbar.IsMouseScrollFocusingThis()) {
                PlayerInput.LockVanillaMouseScroll("GameBuffIconsBarUI");
            }
        }

        public override bool IsLocked() {
            return Mod.ClientConfig.InventoryDownHotbarLockingAlsoLocksThis & Main.player[Main.myPlayer].hbLocked ||
                base.IsLocked();
        }

        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryDownIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryDownIconColsCount;
            Left = StyleDimension.FromPixels(32 - ScrollbarReservedWidth + Mod.ClientConfig.InventoryDownXOffset);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Top = StyleDimension.FromPixels(76 + Mod.ClientConfig.InventoryDownYOffset);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            ScrollbarPosition = Mod.ClientConfig.InventoryDownScrollbarRelPos;
            IconsHorOrder = Mod.ClientConfig.InventoryDownIconsHorOrder;
            HitboxModifier = Mod.ClientConfig.BuffsBarHitboxModifier;

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