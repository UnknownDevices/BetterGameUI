using Terraria;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class HotbarBuffList : BuffList
    {
        public override bool IsVisible => !Main.ingameOptionsWindow && !Main.playerInventory && !Main.inFancyUI;

        public override bool IsLocked => !IsVisible
            || (Mod.Config.Feature_LockingHotbarLocksAdditionalUIs && Main.LocalPlayer.hbLocked);

        public override float Alpha => 0.4f;
        public override ushort RowsCount => (ushort)Mod.Config.General_HotbarsBuffListRows;
        public override ushort ColsCount => 10;

        public HotbarBuffList() {
            Dimensions = new CalculatedStyle(32f - ScrollbarReservedWidth, 76f, 0f, 0f);
        }
    }
}