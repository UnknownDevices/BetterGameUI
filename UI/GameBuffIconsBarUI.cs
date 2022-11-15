using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class GameBuffIconsBarUI : BuffIconsBarUI
    {
        public GameBuffIconsBarUI() {
            ScrollbarReservedWidth = 14;
            IconRowsCount = (ushort)Mod.ClientConfig.GameBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.GameBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarYPxs, Mod.ClientConfig.GameBarYPercent);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconRowsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconColsCount) - IconToIconPad);
            HitboxWidthModifier = Mod.ClientConfig.GameIconsBarHitboxWidthModifier;
            HitboxHeightModifier = Mod.ClientConfig.GameIconsBarHitboxHeightModifier;
            ScrollbarPosition = Mod.ClientConfig.GameScrollbarPosition;
            IconsHorOrder = Mod.ClientConfig.GameIconsHorOrder;

            Append(new ScrollbarUI
            {
                Top = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(10f),
                Height = StyleDimension.FromPixelsAndPercent(-16f, 1f),
                CornerHeight = 4,
                IsVisible = true,
                Alpha = 0.5f,
            });

            switch (ScrollbarPosition) {
                case ScrollbarPosition.LeftOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarXPxs - ScrollbarReservedWidth,
                        Mod.ClientConfig.GameBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixels(0f);
                    break;

                case ScrollbarPosition.RightOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarXPxs,
                        Mod.ClientConfig.GameBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

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
                Alpha = 0.5f,
            });

            Recalculate();
            Mod.OnClientConfigChanged += HandleClientConfigChanged;
        }

        public override void UpdateBeforeDraw() {
            IsLocked |= Mod.ClientConfig.LockGameIconsBarWhenHotbarLocks & Main.player[Main.myPlayer].hbLocked;
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
            IconRowsCount = (ushort)Mod.ClientConfig.GameBarIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.GameBarIconColsCount;
            Top = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarYPxs, Mod.ClientConfig.GameBarYPercent);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            ScrollbarPosition = Mod.ClientConfig.GameScrollbarPosition;
            IconsHorOrder = Mod.ClientConfig.GameIconsHorOrder;
            HitboxWidthModifier = Mod.ClientConfig.GameIconsBarHitboxWidthModifier;
            HitboxHeightModifier = Mod.ClientConfig.GameIconsBarHitboxHeightModifier;

            switch (ScrollbarPosition) {
                case ScrollbarPosition.LeftOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarXPxs - ScrollbarReservedWidth,
                        Mod.ClientConfig.GameBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixels(0f);
                    break;

                case ScrollbarPosition.RightOfIcons:
                    Left = StyleDimension.FromPixelsAndPercent(Mod.ClientConfig.GameBarXPxs,
                        Mod.ClientConfig.GameBarXPercent);
                    ScrollbarUI.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.GameBarMinScrollerHeight);
            ScrollbarUI.ScrollerUI.HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier;
            ScrollbarUI.ScrollerUI.HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier;

            Recalculate();
        }
    }
}