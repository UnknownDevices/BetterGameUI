using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class GameBuffIconsBarUI : BuffIconsBarUI 
    {
        // TODO: consider decoupling 'Mod.ClienConfig'
        public GameBuffIconsBarUI() {
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

        public void UpdateClientConfigDependencies() {
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
        }

        public void HandleClientConfigChanged() {
            UpdateClientConfigDependencies();
            Recalculate();
        }
    }
}