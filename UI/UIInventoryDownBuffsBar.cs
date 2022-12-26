using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIInventoryDownBuffsBar : UIBuffsBar 
    {
        public UIInventoryDownBuffsBar() {
            ScrollbarPosition = ScrollbarRelPos.LeftOfIcons;
            IconsHorOrder = BuffIconsHorOrder.LeftToRight;
        }
        
        public override bool IsLocked() {
            return Mod.ClientConfig.InventoryDownHotbarLockingAlsoLocksThis && Main.player[Main.myPlayer].hbLocked;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            MaybeDisable(Main.ingameOptionsWindow | Main.playerInventory | Main.inFancyUI);
            base.Draw(spriteBatch);
        }
        
        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.InventoryDownIconRows;
            IconColsCount = (ushort)Mod.ClientConfig.InventoryDownIconCols;
            Left = StyleDimension.FromPixels(32 - ScrollbarReservedWidth + Mod.ClientConfig.InventoryDownXPosMod);
            Top = StyleDimension.FromPixels(76 + Mod.ClientConfig.InventoryDownYPosMod);
            Alpha = Mod.ClientConfig.InventoryDownAlpha;
            UIScrollbar.Alpha = Mod.ClientConfig.InventoryDownAlpha;
            UIScrollbar.UIScroller.Alpha = Mod.ClientConfig.InventoryDownAlpha;

            base.UpdateClientConfigDependencies();
        }
    }
}