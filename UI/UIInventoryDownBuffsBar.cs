using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIInventoryDownBuffsBar : UIBuffsBar 
    {
        public override bool IsLocked() {
            return Mod.ClientConfig.InventoryDownHotbarLockingAlsoLocksThis & Main.player[Main.myPlayer].hbLocked ||
                base.IsLocked();
        }

        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryDownIconRows;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryDownIconCols;
            Left = StyleDimension.FromPixels(32 - ScrollbarReservedWidth + Mod.ClientConfig.InventoryDownXPosMod);
            Top = StyleDimension.FromPixels(76 + Mod.ClientConfig.InventoryDownYPosMod);
            ScrollbarPosition = Mod.ClientConfig.InventoryDownScrollbarRelPos;
            IconsHorOrder = Mod.ClientConfig.InventoryDownIconsHorOrder;

            base.UpdateClientConfigDependencies();
        }
    }
}