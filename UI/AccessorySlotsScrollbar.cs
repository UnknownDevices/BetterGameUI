using BetterGameUI.Reflection;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class AccessorySlotsScrollbar : Scrollbar
    {
        private uint pagesCount;
        private uint pagesShowAtOnce;
        private CalculatedStyle parentDimensions;

        public override bool IsVisible => 0 < NotchesCount - NotchesPerPage
            && ModAccessorySlotPlayerReflection.GetScrollSlots(AccessorySlotLoaderReflection.ModSlotPlayer(Main.LocalPlayer));

        public override bool CanScrollerBeDragged => IsVisible;

        // TODO: Parent dimensions should consider bar dimensions as well
        public override bool CanListenToWheelScroll => IsVisible && (IsParentHovered || IsBarHovered);

        public override float BarAlpha => 0.65f;
        public override float ScrollerMinAlpha => 0.65f;
        public override uint NotchesCount => pagesCount;
        public override uint NotchesPerPage => pagesShowAtOnce;

        public CalculatedStyle ParentDimensions => parentDimensions;

        public bool IsParentHovered => ParentDimensions.GrowFromCenter(4).Contains(Player.MouseX, Player.MouseY);

        public AccessorySlotsScrollbar() {
            BarDimensions = new CalculatedStyle
            {
                Width = 10f
            };

            ScrollerDimensions = new CalculatedStyle
            {
                Width = 6f,
            };
        }

        public void Update(int accessoryPerColumn, int slotsToRender) {
            int parentBottom = ((int)((float)(AccessorySlotLoader.DrawVerticalAlignment) +
                (float)(((accessoryPerColumn) + 3) * 56) * Main.inventoryScale) + 4);
            int parentTop = (int)((float)(AccessorySlotLoader.DrawVerticalAlignment) +
                (float)((0 + 3) * 56) * Main.inventoryScale) + 4;

            parentDimensions = new CalculatedStyle(Main.screenWidth - 64 - 28 - 47 * 2,
                parentTop,
                47 * 3,
                parentBottom - parentTop);

            BarDimensions.X = Main.screenWidth - 64 - 28 + 47 + 6;
            BarDimensions.Y = ParentDimensions.Y;
            BarDimensions.Height = ParentDimensions.Height;

            pagesCount = (uint)Math.Max(slotsToRender, 0);
            pagesShowAtOnce = (uint)Math.Max(accessoryPerColumn, 0);

            base.Update();

            if (CanListenToWheelScroll) {
                PlayerInput.LockVanillaMouseScroll("UIBuffsBarScrollbar");
            }
        }
    }
}