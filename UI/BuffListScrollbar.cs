using System;
using Terraria.GameInput;
using Terraria;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class BuffListScrollbar : Scrollbar {
        private uint _pagesCount;
        public override bool IsVisible => Parent.IsVisible && 0 < PagesCount - PagesShownAtOnce;
        public override bool CanScrollerBeDragged => !Parent.IsLocked && IsVisible 
            && Mod.ClientConfig.BuffsBars_AllowDraggingScroller;
        public override bool CanListenToWheelScroll => !Parent.IsLocked && IsVisible 
            && (Player.BuffListHasWheelScrollFocus 
            || (Mod.ClientConfig.BuffsBars_HoverCursorToFocusWheelScroll && Parent.IsHovered));
        public override bool InvertWheelScroll => Mod.ClientConfig.BuffsBars_InvertWheelScroll;
        public override float BarAlpha => Parent.Alpha;
        public override float ScrollerMinAlpha => Parent.Alpha;
        public override uint PagesCount => _pagesCount;
        public override uint PagesShownAtOnce  => Parent.RowsCount;
        public BuffList Parent { get; set; }

        public BuffListScrollbar(BuffList parent) {
            Parent = parent;
            BarDimensions = new CalculatedStyle {
                Width = 10f
            };
            ScrollerDimensions = new CalculatedStyle {
                Width = 6f,
            };
        }

        public override void Update() {
            switch (Parent.ScrollbarRelPos) {
                case ScrollbarRelPos.LeftOfIcons:
                    BarDimensions.X = Parent.Dimensions.X;
                    break;
                case ScrollbarRelPos.RightOfIcons:
                    BarDimensions.X = Parent.Dimensions.X + Parent.Dimensions.Width - BuffList.ScrollbarReservedWidth;
                    break;
                default:
                    break;
            }
            BarDimensions.Y = Parent.Dimensions.Y;
            BarDimensions.Height = Math.Max(Parent.Dimensions.Height - 16f, BarMinHeight);

            _pagesCount = (Mod.ActiveBuffsIndexes.Count <= 0) 
                ? 0 : (uint)Math.Max(Math.Ceiling(
                    (double)Mod.ActiveBuffsIndexes.Count / (double)Parent.ColsCount), 0);

            base.Update();

            if (CanListenToWheelScroll /** || IsScrollerBeingDragged() **/) {
                PlayerInput.LockVanillaMouseScroll("UIBuffsBarScrollbar");
            }
        }
    }
}
