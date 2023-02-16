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
            Left = StyleDimension.FromPixelsAndPercent(-84 - 38 * (ColumnsCountCount - 1), 1f);
            Top = StyleDimension.FromPixelsAndPercent(421, 1f);
            ScrollbarPosition = ScrollbarRelPos.RightOfIcons;
            IconsHorOrder = BuffIconsHorOrder.RightToLeft;
        }

        public override void UpdateClientConfigDependencies() {
            RowsCountCount = (ushort)Mod.ClientConfig.InventorysBuffsBar_RowsCount;
            ColumnsCountCount = (ushort)Mod.ClientConfig.InventorysBuffsBar_ColumnsCount;
            UIScrollbar.UIScroller.Alpha = UIScrollbar.Alpha = Alpha = (float)Mod.ClientConfig.InventorysBuffsBar_Alpha / 100;

            base.UpdateClientConfigDependencies();
        }
    }
}