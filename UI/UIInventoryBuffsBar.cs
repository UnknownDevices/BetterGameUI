using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIInventoryBuffsBar : UIBuffsBar
    {
        public UIInventoryBuffsBar() {
            Left = StyleDimension.FromPixelsAndPercent(-84 - 38 * (IconColsCount - 1), 1f);
            Top = StyleDimension.FromPixelsAndPercent(421, 1f);
            ScrollbarPosition = ScrollbarRelPos.RightOfIcons;
            IconsHorOrder = BuffIconsHorOrder.RightToLeft;
        }

        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryIconRows;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryIconCols;
            UIScrollbar.UIScroller.Alpha = UIScrollbar.Alpha = Alpha = (float)Mod.ClientConfig.InventoryAlpha / 100;

            base.UpdateClientConfigDependencies();
        }
    }
}