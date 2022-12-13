using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class InventoryBuffIconsBarUI : BuffIconsBarUI
    {
        public InventoryBuffIconsBarUI() {
            ScrollbarReservedWidth = 14;

            // TODO: some of this should be done in ScrollbarUI
            Append(new ScrollbarUI
            {
                Top = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(10f),
                Height = StyleDimension.FromPixelsAndPercent(-16f, 1f),
                CornerHeight = 4,
                Alpha = 0.5f,
            });

            ScrollbarUI.Append(new ScrollerUI
            {
                Top = StyleDimension.FromPixels(0f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(6f),
                Height = StyleDimension.FromPixels(8f),
                CornerHeight = 2,
                Alpha = 0.5f,
            });

            Mod.OnClientConfigChanged += HandleClientConfigChanged;

            UpdateClientConfigDependencies();
            Recalculate();
        }
        
        // TODO: scrollbar shouldn't be accounted within this object dimensions
        public override void UpdateBeforeDraw() {
            ScrollbarUI.IsDraggingScrollerAllowed &= Mod.ClientConfig.AllowScrollerDragging;
            ScrollbarUI.IsVisible &= !Mod.ClientConfig.InventoryBarSmartHideScrollbar | 0 < ScrollbarUI.MaxScrolls;

            base.UpdateBeforeDraw();

            ScrollbarUI.IsMouseScrollAllowed &=
                !Mod.ClientConfig.NeverAllowMouseScroll &
                Player.IsMouseScrollAllowed &
                (!Mod.ClientConfig.OnlyAllowMouseScrollWhenHoveringUI | IsMouseHoveringHitbox);

            if (ScrollbarUI.IsMouseScrollAllowed & Mod.ClientConfig.SmartLockVanillaMouseScroll) {
                PlayerInput.LockVanillaMouseScroll("BuffIconsBarUI");
            }
        }

        public void UpdateClientConfigDependencies() {
            // TODO: implement offsets
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryBarIconColsCount;
            Left = StyleDimension.FromPixelsAndPercent(-84 - 38 * (IconColsCount - 1), 1f);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Top = StyleDimension.FromPixelsAndPercent(421, 1f);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            HitboxWidthModifier = Mod.ClientConfig.InventoryIconsBarHitboxWidthModifier;
            HitboxHeightModifier = Mod.ClientConfig.InventoryIconsBarHitboxHeightModifier;
            ScrollbarPosition = Mod.ClientConfig.InventoryScrollbarRelPosition;
            IconsHorOrder = Mod.ClientConfig.InventoryIconsHorOrder;

            switch (ScrollbarPosition) {
                case ScrollbarPosition.LeftOfIcons:
                    ScrollbarUI.Left = StyleDimension.FromPixels(0f);
                    break;

                case ScrollbarPosition.RightOfIcons:
                    ScrollbarUI.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.InventoryBarMinScrollerHeight);
            ScrollbarUI.ScrollerUI.HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier;
            ScrollbarUI.ScrollerUI.HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier;
        }

        public void HandleClientConfigChanged() {
            UpdateClientConfigDependencies();
            Recalculate();
        }
    }
}