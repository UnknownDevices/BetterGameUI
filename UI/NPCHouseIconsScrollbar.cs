using BetterGameUI.Reflection;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class NPCHouseIconsScrollbar : Scrollbar
    {
        private uint notchesCount;
        private uint notchesPerPage;
        private CalculatedStyle parentDimensions;
        public override bool IsVisible => 0 < (int)NotchesCount - (int)NotchesPerPage;
        public override bool CanScrollerBeDragged => IsVisible;
        public override bool CanListenToWheelScroll => IsVisible && (IsParentHovered || IsBarHovered);
        public override float BarAlpha => 0.65f;
        public override float ScrollerMinAlpha => 0.65f;
        public override uint NotchesCount => notchesCount;
        public override uint NotchesPerPage => notchesPerPage;
        public CalculatedStyle ParentDimensions => parentDimensions;
        public bool IsParentHovered => ParentDimensions.GrowFromCenter(4).Contains(Player.UIMouseX, Player.UIMouseY);

        public NPCHouseIconsScrollbar()
        {
            BarDimensions = new CalculatedStyle
            {
                Width = 10f
            };

            ScrollerDimensions = new CalculatedStyle
            {
                Width = 6f,
            };
        }

        public void Update(int[] npcIndexWhoHoldsHeadIndex, int mH)
        {
            int totalHeadsHeldByAnNPC = 1;
            for (int k = 0; k < Terraria.GameContent.TextureAssets.NpcHead.Length; k++)
            {
                if (npcIndexWhoHoldsHeadIndex[k] != -1)
                {
                    totalHeadsHeldByAnNPC++;
                }
            }

            // TODO: some other code already did some of these clamps
            notchesCount = (uint)Math.Max(Math.Ceiling((float)totalHeadsHeldByAnNPC / (float)Mod.Config.General_NPCHouseIconsColumns), 0);
            notchesPerPage = (uint)Math.Ceiling((float)(Main.screenHeight - 80 - 174 - mH) / ((float)56 * Main.inventoryScale));
            ScrolledNotches = (uint)Math.Min(ScrolledNotches, Math.Max(0, (int)notchesCount - (int)notchesPerPage));

            parentDimensions = new CalculatedStyle(BarDimensions.X = Main.screenWidth - 64 - 28 - 48 * (Mod.Config.General_NPCHouseIconsColumns - 1),
                174 + mH,
                48 * Mod.Config.General_NPCHouseIconsColumns,
                (float)notchesPerPage * 56 * Main.inventoryScale);

            BarDimensions.X = ParentDimensions.X + ParentDimensions.Width;
            BarDimensions.Y = ParentDimensions.Y;
            BarDimensions.Height = ParentDimensions.Height;

            base.Update();

            if (CanListenToWheelScroll)
            {
                PlayerInput.LockVanillaMouseScroll("UIBuffsBarScrollbar");
            }
        }
    }
}