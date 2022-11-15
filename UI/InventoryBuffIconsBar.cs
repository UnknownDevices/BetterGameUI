using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class InventoryBuffIconsBarUI : BuffIconsBarUI
    {
        public InventoryBuffIconsBarUI() {
            ScrollbarReservedWidth = 14;
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarYPxs, Mod.ClientConfig.InventoryBarYPercent);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconRowsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconColsCount) - IconToIconPad);
            HitboxWidthModifier = Mod.ClientConfig.InventoryIconsBarHitboxWidthModifier;
            HitboxHeightModifier = Mod.ClientConfig.InventoryIconsBarHitboxHeightModifier;
            ScrollbarPosition = Mod.ClientConfig.InventoryScrollbarPosition;
            IconsHorOrder = Mod.ClientConfig.InventoryIconsHorOrder;

            // TODO: some of this should be done in ScrollbarUI
            Append(new ScrollbarUI
            {
                Top = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(10f),
                Height = StyleDimension.FromPixelsAndPercent(-16f, 1f),
                CornerHeight = 4,
                Alpha = 0.5f,
            });

            switch (ScrollbarPosition) {
                case ScrollbarPosition.LeftOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarXPxs - ScrollbarReservedWidth,
                        Mod.ClientConfig.InventoryBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixels(0f);
                    break;

                case ScrollbarPosition.RightOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarXPxs,
                        Mod.ClientConfig.InventoryBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            ScrollbarUI.Append(new ScrollerUI
            {
                Top = StyleDimension.FromPixels(0f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(6f),
                Height = StyleDimension.FromPixels(8f),
                MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.InventoryBarMinScrollerHeight),
                HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier,
                HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier,
                CornerHeight = 2,
                Alpha = 0.5f,
            });

            Recalculate();
            Mod.OnClientConfigChanged += HandleClientConfigChanged;
        }

        public override void UpdateBeforeDraw() {
            ScrollbarUI.IsDraggingScrollerAllowed &= Mod.ClientConfig.AllowScrollerDragging;
            ScrollbarUI.IsVisible &= Mod.ClientConfig.GameBarNeverHideScrollbar | 0 < ScrollbarUI.MaxScrolls;

            base.UpdateBeforeDraw();

            ScrollbarUI.IsMouseScrollAllowed &=
                !Mod.ClientConfig.NeverAllowMouseScroll &
                Player.IsMouseScrollAllowed &
                (!Mod.ClientConfig.OnlyAllowMouseScrollWhenHoveringUI | IsMouseHoveringHitbox);

            if (ScrollbarUI.IsMouseScrollAllowed & Mod.ClientConfig.SmartLockVanillaMouseScroll) {
                PlayerInput.LockVanillaMouseScroll("BuffIconsBarUI");
            }
        }

        public void HandleClientConfigChanged() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarYPxs, Mod.ClientConfig.InventoryBarYPercent);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            HitboxWidthModifier = Mod.ClientConfig.InventoryIconsBarHitboxWidthModifier;
            HitboxHeightModifier = Mod.ClientConfig.InventoryIconsBarHitboxHeightModifier;
            ScrollbarPosition = Mod.ClientConfig.InventoryScrollbarPosition;
            IconsHorOrder = Mod.ClientConfig.InventoryIconsHorOrder;

            switch (ScrollbarPosition) {
                case ScrollbarPosition.LeftOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarXPxs - ScrollbarReservedWidth,
                        Mod.ClientConfig.InventoryBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixels(0f);
                    break;

                case ScrollbarPosition.RightOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarXPxs,
                        Mod.ClientConfig.InventoryBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.InventoryBarMinScrollerHeight);
            ScrollbarUI.ScrollerUI.HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier;
            ScrollbarUI.ScrollerUI.HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier;

            Recalculate();
        }
    }
}