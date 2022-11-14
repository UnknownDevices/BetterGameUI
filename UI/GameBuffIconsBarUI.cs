using System.Buffers.Text;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class GameBuffIconsBarUI : BuffIconsBarUI
    {
        public GameBuffIconsBarUI() {
            ScrollbarReservedWidth = 16;
            IconRowsCount = (ushort)Mod.ClientConfig.GameBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.GameBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarYPxs, Mod.ClientConfig.GameBarYPercent);
            Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarXPxs, Mod.ClientConfig.GameBarXPercent);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconRowsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconColsCount) - IconToIconPad);
            IconsHorOrder = Mod.ClientConfig.GameBarOrderIconsFromRightToLeft ?
                BuffIconsHorOrder.RightToLeft : BuffIconsHorOrder.LeftToRight;

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
                MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.GameBarMinScrollerHeight),
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

            UIElem.IsLocked |= Mod.ClientConfig.LockGameIconsBarWhenHotbarLocks & Main.player[Main.myPlayer].hbLocked;
            UIElem.ScrollbarUI.IsMouseScrollAllowed &=
                !Mod.ClientConfig.NeverAllowMouseScroll &
                Player.IsMouseScrollAllowed &
                (!Mod.ClientConfig.OnlyAllowMouseScrollWhenHoveringUI | UIElem.IsMouseHovering);
            UIElem.ScrollbarUI.IsDraggingScrollerAllowed &= Mod.ClientConfig.AllowScrollerDragging;
            UIElem.ScrollbarUI.IsVisible &= Mod.ClientConfig.GameBarNeverHideScrollbar | 0 < UIElem.ScrollbarUI.MaxScrolls;

            BuffIconsBarUI.HandleUpdate(affectedElement);

            if (UIElem.ScrollbarUI.IsMouseScrollAllowed & Mod.ClientConfig.SmartLockVanillaMouseScroll) {
                PlayerInput.LockVanillaMouseScroll("BuffIconsBarUI");
            }
        }

        public void HandleClientConfigChanged() {
            IconRowsCount = (ushort)Mod.ClientConfig.GameBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.GameBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarYPxs, Mod.ClientConfig.GameBarYPercent);
            Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarXPxs, Mod.ClientConfig.GameBarXPercent);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            IconsHorOrder = Mod.ClientConfig.GameBarOrderIconsFromRightToLeft ?
                BuffIconsHorOrder.RightToLeft : BuffIconsHorOrder.LeftToRight;

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.GameBarMinScrollerHeight);
            ScrollbarUI.ScrollerUI.HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier;
            ScrollbarUI.ScrollerUI.HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier;

            Recalculate();
        }
    }
}