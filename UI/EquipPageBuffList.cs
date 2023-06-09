using Terraria;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class EquipPageBuffList : BuffList
    {
        public override bool IsVisible => Main.playerInventory && Main.EquipPage == 2;
        public override float Alpha => 0.65f;
        public override ushort RowsCount => (ushort)Mod.Config.General_EquipPagesBuffListRows;
        public override ushort ColsCount => 5;
        public override BuffIconsHorOrder IconsHorOrder => BuffIconsHorOrder.RightToLeft;
        public override ScrollbarRelPos ScrollbarRelPos => ScrollbarRelPos.RightOfIcons;

        public EquipPageBuffList() {
            Dimensions = new CalculatedStyle();
        }
    }
}