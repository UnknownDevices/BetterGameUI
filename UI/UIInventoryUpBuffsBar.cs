using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIInventoryUpBuffsBar : UIBuffsBar
    {
        public UIInventoryUpBuffsBar() {
            Left = StyleDimension.FromPixelsAndPercent(-84 - 38 * (IconColsCount - 1), 1f);
            Top = StyleDimension.FromPixelsAndPercent(421, 1f);
            ScrollbarPosition = ScrollbarRelPos.RightOfIcons;
            IconsHorOrder = BuffIconsHorOrder.RightToLeft;
        }

        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryUpIconRows;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryUpIconCols;
            UIScrollbar.UIScroller.Alpha = UIScrollbar.Alpha = Alpha = (float)Mod.ClientConfig.InventoryUpAlpha / 100;

            base.UpdateClientConfigDependencies();
        }
    }
}