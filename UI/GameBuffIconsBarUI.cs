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
            Left = StyleDimension.FromPixels(32 - ScrollbarReservedWidth);
            Top = StyleDimension.FromPixels(76);

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
            IsLocked |= Mod.ClientConfig.GameHotbarLockingAlsoLocksThis & Main.player[Main.myPlayer].hbLocked;
            ScrollbarUI.IsDraggingScrollerAllowed &= Mod.ClientConfig.AllowScrollerDragging;
            ScrollbarUI.IsVisible &= !Mod.ClientConfig.SmartHideScrollbar | 0 < ScrollbarUI.ScrollableDist;

            base.UpdateBeforeDraw();

            ScrollbarUI.IsMouseScrollFocusingThis &= Player.MouseScrollIsFocusingBuffIconsBar |
                (Mod.ClientConfig.MouseInputFocusesMouseHoveredUI & IsMouseHoveringHitbox);

            if (ScrollbarUI.IsMouseScrollFocusingThis) {
                PlayerInput.LockVanillaMouseScroll("GameBuffIconsBarUI");
            }

            ScrollbarUI.ExtraMouseScroll += Player.ExtraMouseScrollForBuffIconsBarScrollbar;
        }

        public void UpdateClientConfigDependencies() {
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
                    ScrollbarUI.Left = StyleDimension.FromPixels(0f);
                    break;
                case ScrollbarPosition.RightOfIcons:
                    ScrollbarUI.Left = StyleDimension.FromPixelsAndPercent(-ScrollbarReservedWidth + 4, 1f);
                    break;
            }

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinimalScrollerHeight);
            ScrollbarUI.ScrollerUI.HitboxModifier = Mod.ClientConfig.ScrollerHitboxModifier;
        }

        public void HandleClientConfigChanged() {
            UpdateClientConfigDependencies();
            Recalculate();
        }
    }
}