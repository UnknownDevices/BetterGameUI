using Terraria;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class EquipPageBuffList : BuffList
    {
        public override bool IsVisible => Main.playerInventory && Main.EquipPage == 2;
        public override float Alpha => (float)Mod.ClientConfig.InventorysBuffsBar_Alpha / 100;
        public override ushort RowsCount => (ushort)Mod.ClientConfig.InventorysBuffsBar_RowsCount;
        public override ushort ColsCount => (ushort)Mod.ClientConfig.InventorysBuffsBar_ColumnsCount;
        public override BuffIconsHorOrder IconsHorOrder => BuffIconsHorOrder.RightToLeft;
        public override ScrollbarRelPos ScrollbarRelPos => ScrollbarRelPos.RightOfIcons;

        public EquipPageBuffList() {
            Dimensions = new CalculatedStyle();
        }
    }
}
