using Terraria;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class HotbarBuffList : BuffList
    {
        public override bool IsVisible => !Main.ingameOptionsWindow && !Main.playerInventory && !Main.inFancyUI;
        public override bool IsLocked =>  !IsVisible 
            || (Mod.ClientConfig.HotbarsBuffsBar_LockWhenHotbarIsLocked && Main.LocalPlayer.hbLocked);
        public override float Alpha => (float)Mod.ClientConfig.HotbarsBuffsBar_Alpha / 100;
        public override ushort RowsCount => (ushort)Mod.ClientConfig.HotbarsBuffsBar_RowsCount;
        public override ushort ColsCount => (ushort)Mod.ClientConfig.HotbarsBuffsBar_ColumnsCount;

        public HotbarBuffList() {
            Dimensions = new CalculatedStyle(32f - ScrollbarReservedWidth, 76f, 0f, 0f);
        }
    }
}
