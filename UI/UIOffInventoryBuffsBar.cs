using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public sealed class UIOffInventoryBuffsBar : UIBuffsBar 
    {
        public UIOffInventoryBuffsBar() {
            Left = StyleDimension.FromPixels(32 - ScrollbarReservedWidth);
            Top = StyleDimension.FromPixels(76);
            ScrollbarPosition = ScrollbarRelPos.LeftOfIcons;
            IconsHorOrder = BuffIconsHorOrder.LeftToRight;
        }
        
        public override bool IsLocked() {
            return Mod.ClientConfig.OffInventoryHotbarLockingAlsoLocksThis && Main.player[Main.myPlayer].hbLocked;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            MaybeDisable(Main.ingameOptionsWindow | Main.playerInventory | Main.inFancyUI);
            base.Draw(spriteBatch);
        }
        
        public override void UpdateClientConfigDependencies() {
            IconRowsCount = (ushort)Mod.ClientConfig.OffInventoryIconRows;
            IconColsCount = (ushort)Mod.ClientConfig.OffInventoryIconCols;
            UIScrollbar.UIScroller.Alpha = UIScrollbar.Alpha = Alpha = (float)Mod.ClientConfig.OffInventoryAlpha / 100;

            base.UpdateClientConfigDependencies();
        }
    }
}