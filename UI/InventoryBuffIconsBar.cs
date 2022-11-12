using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class InventoryBuffIconsBarUI : BuffIconsBarUI
    {
        public InventoryBuffIconsBarUI() {
            ScrollbarReservedWidth = 16;
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarY, 1f);
            Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarX, 1f);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconRowsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconColsCount) - IconToIconPad);
            IconsHorOrder = Mod.ClientConfig.InventoryBarOrderIconsFromRightToLeft ?
                BuffIconsHorOrder.RightToLeft : BuffIconsHorOrder.LeftToRight;

            // TODO: this should be done in BuffIconsBarUI
            Append(new ScrollbarUI
            {
                Top = StyleDimension.FromPixels(2f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(10f),
                Height = StyleDimension.FromPixelsAndPercent(-16f, 1f),
                CornerHeight = 4,
                IsVisible = true,
                Alpha = 0.5f,
            });

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
                // TODO: this should be set on OnActivate or something like that
                IsVisible = true,
                Alpha = 0.5f,
            });

            Recalculate();
            OnUpdate -= BuffIconsBarUI.HandleUpdate;
            OnUpdate += HandleUpdate;
            Mod.OnClientConfigChanged += HandleClientConfigChanged;
        }

        public new static void HandleUpdate(UIElement affectedElement) {
            var UIElem = affectedElement as BuffIconsBarUI;

            UIElem.ScrollbarUI.IsMouseScrollAllowed =
                !Mod.ClientConfig.NeverAllowMouseScroll &
                Player.IsMouseScrollAllowed &
                (!Mod.ClientConfig.OnlyAllowMouseScrollWhenHoveringUI | UIElem.IsMouseHovering);

            BuffIconsBarUI.HandleUpdate(affectedElement);

            UIElem.ScrollbarUI.IsDraggingScrollerAllowed &= Mod.ClientConfig.AllowScrollerDragging;
            UIElem.ScrollbarUI.IsVisible = Mod.ClientConfig.GameBarNeverHideScrollbar | 0 < UIElem.ScrollbarUI.MaxScrolls;

            if (UIElem.ScrollbarUI.IsMouseScrollAllowed & Mod.ClientConfig.SmartLockVanillaMouseScroll) {
                PlayerInput.LockVanillaMouseScroll("BuffIconsBarUI");
            }
        }

        // TODO: add configs to toggle automatic calculation of X, Y, IconColsCount and IconsRowsCount
        public void HandleClientConfigChanged() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarY, 1f);
            Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.InventoryBarX, 1f);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            IconsHorOrder = Mod.ClientConfig.InventoryBarOrderIconsFromRightToLeft ?
                BuffIconsHorOrder.RightToLeft : BuffIconsHorOrder.LeftToRight;

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.InventoryBarMinScrollerHeight);
            ScrollbarUI.ScrollerUI.HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier;
            ScrollbarUI.ScrollerUI.HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier;

            Recalculate();
        }
    }
}