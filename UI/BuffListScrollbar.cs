using System;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class BuffListScrollbar : Scrollbar
    {
        private uint pagesCount;
        public override bool IsVisible => Parent.IsVisible && 0 < NotchesCount - NotchesPerPage;

        public override bool CanScrollerBeDragged => !Parent.IsLocked && IsVisible;

        public override bool CanListenToWheelScroll => IsVisible && KeybindSystem.BuffListScrollIsActive.Current
            || ((Mod.Config.General_HoverToActivateHotbarsBuffListScroll || !Main.playerInventory) && Parent.IsHovered);

        public override float BarAlpha => Parent.Alpha;
        public override float ScrollerMinAlpha => Parent.Alpha;
        public override uint NotchesCount => pagesCount;
        public override uint NotchesPerPage => Parent.RowsCount;
        public BuffList Parent { get; set; }

        public BuffListScrollbar(BuffList parent) {
            Parent = parent;

            BarDimensions = new CalculatedStyle
            {
                Width = 10f
            };

            ScrollerDimensions = new CalculatedStyle
            {
                Width = 6f,
            };
        }

        public void Update(int totalActiveBuffIndexes) {
            switch (Parent.ScrollbarRelPos) {
                case ScrollbarRelPos.Left:
                    BarDimensions.X = Parent.Dimensions.X;
                    break;

                case ScrollbarRelPos.Right:
                    BarDimensions.X = Parent.Dimensions.X + Parent.Dimensions.Width - BuffList.ScrollbarReservedWidth;
                    break;

                default:
                    break;
            }

            BarDimensions.Y = Parent.Dimensions.Y;
            BarDimensions.Height = Math.Max(Parent.Dimensions.Height - 16f, BarMinHeight);

            pagesCount = (totalActiveBuffIndexes <= 0)
                ? 0 : (uint)Math.Max(Math.Ceiling(
                    (double)totalActiveBuffIndexes / (double)Parent.ColsCount), 0);

            base.Update();

            if (CanListenToWheelScroll /** || IsScrollerBeingDragged() **/) {
                PlayerInput.LockVanillaMouseScroll("UIBuffsBarScrollbar");
            }
        }
    }
}