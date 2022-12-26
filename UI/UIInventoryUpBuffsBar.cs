﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIInventoryUpBuffsBar : UIBuffsBar
    {
        public UIInventoryUpBuffsBar() {
            ScrollbarPosition = ScrollbarRelPos.RightOfIcons;
            IconsHorOrder = BuffIconsHorOrder.RightToLeft;
        }

        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryUpIconRows;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryUpIconCols;
            Left = StyleDimension.FromPixelsAndPercent(
                -84 - 38 * (IconColsCount - 1) + Mod.ClientConfig.InventoryUpXPosMod, 1f);
            Top = StyleDimension.FromPixelsAndPercent(421 + Mod.ClientConfig.InventoryUpYPosMod, 1f);
            Alpha = Mod.ClientConfig.InventoryUpAlpha;
            UIScrollbar.Alpha = Mod.ClientConfig.InventoryUpAlpha;
            UIScrollbar.UIScroller.Alpha = Mod.ClientConfig.InventoryUpAlpha;

            base.UpdateClientConfigDependencies();
        }
    }
}