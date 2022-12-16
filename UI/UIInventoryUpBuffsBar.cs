using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIInventoryUpBuffsBar : UIBuffsBar
    {
        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryUpIconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryUpIconColsCount;
            Left = StyleDimension.FromPixelsAndPercent(
                -84 - 38 * (IconColsCount - 1) + Mod.ClientConfig.InventoryUpXOffset, 1f);
            Top = StyleDimension.FromPixelsAndPercent(421 + Mod.ClientConfig.InventoryUpYOffset, 1f);
            ScrollbarPosition = Mod.ClientConfig.InventoryUpScrollbarRelPosition;
            IconsHorOrder = Mod.ClientConfig.InventoryUpIconsHorOrder;

            base.UpdateClientConfigDependencies();
        }
    }
}