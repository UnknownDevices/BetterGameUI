// STFU Microsoft
#pragma warning disable CA2211

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.Main;

namespace BetterGameUI.UI
{
    public class UISystem : ModSystem {
        public static UserInterface UserInterfaceInventoryDownBuffsBar;
        public static UserInterface UserInterfaceMap;
        public static GameTime LastUpdateUIGameTime;

        public static UIInventoryDownBuffsBar UIInventoryDownBuffsBar =>
            UserInterfaceInventoryDownBuffsBar.CurrentState as UIInventoryDownBuffsBar;
        public static UIBasic UIMap => UserInterfaceMap.CurrentState as UIBasic;
        public static UIInventoryUpBuffsBar UIInventoryUpBuffsBar =>
            UserInterfaceMap.CurrentState.Children.First() as UIInventoryUpBuffsBar;

        public static bool DrawInterface_Logic_0() {
            BetterGameUI.Mod.ActiveBuffsIndexes = new List<int>(Terraria.Player.MaxBuffs);
            for (int i = 0; i < Terraria.Player.MaxBuffs; ++i) {
                if (player[myPlayer].buffType[i] > 0) {
                    BetterGameUI.Mod.ActiveBuffsIndexes.Add(i);

                    buffAlpha[i] -= 0.05f;
                    if (buffAlpha[i] < 0f) {
                        buffAlpha[i] = 0f;
                    }
                }
            }

            return true;
        }

        public static void Draw_InventoryBuffsBar(int mapHeight) {
            // TODO: make mapHeight automatically call GetValue(null) on itself
            // TODO: do this in UIMap.Update
            if (UIMap.Height.Pixels != mapHeight) {
                UIMap.Height = StyleDimension.FromPixels(mapHeight);
                UserInterfaceMap.Recalculate();
            }

            UserInterfaceMap.Draw(spriteBatch, LastUpdateUIGameTime);
        }

        public static bool DrawInterface_ResourceBars()
        {
            if (!playerInventory)
            {
                recBigList = false;
            }

            ResourceSetsManager.Draw();
            // TODO: doesn't GameInterfaceLayer.Draw() already call spriteBatch.Begin? does it use the same params?
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None,
                RasterizerState.CullCounterClockwise, null, UIScaleMatrix);

            Reflection.Main.DrawInterface_Resources_Breath();
            DrawInterface_Resources_ClearBuffs();

            UserInterfaceInventoryDownBuffsBar.Draw(spriteBatch, LastUpdateUIGameTime);
            return true;
        }

        // TODO: UIMap class
        public override void Load()
        {
            if (!dedServ)
            {
                UserInterfaceInventoryDownBuffsBar = new();
                UserInterfaceInventoryDownBuffsBar.SetState(new UIInventoryDownBuffsBar());
                UIInventoryDownBuffsBar.Activate();

                var inventoryBuffIconsBarParentUI = new UIBasic();
                inventoryBuffIconsBarParentUI.Append(new UIInventoryUpBuffsBar());
                inventoryBuffIconsBarParentUI.Width = StyleDimension.FromPercent(1f);
                UserInterfaceMap = new();
                UserInterfaceMap.SetState(inventoryBuffIconsBarParentUI);
                UIInventoryUpBuffsBar.Activate();
            }
        }

        public override void Unload()
        {
            LastUpdateUIGameTime = null;
            UserInterfaceInventoryDownBuffsBar = null;
            UserInterfaceMap = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            LastUpdateUIGameTime = gameTime;
            if (LastUpdateUIGameTime != null)
            {
                UserInterfaceInventoryDownBuffsBar.Update(LastUpdateUIGameTime);
                UserInterfaceMap.Update(LastUpdateUIGameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Insert(0, new LegacyGameInterfaceLayer(
                    "BetterGameUI: Logic 0", DrawInterface_Logic_0,
                    InterfaceScaleType.None));

            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (index != -1)
            {
                layers[index] = new LegacyGameInterfaceLayer(
                    "Vanilla: Resource Bars", DrawInterface_ResourceBars,
                    InterfaceScaleType.UI);
            }

            if (!BetterGameUI.Mod.ClientConfig.DisableThisModChangesToTheHotbar) {
                index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
                if (index != -1) {
                    layers[index] = new LegacyGameInterfaceLayer(
                        "Vanilla: Hotbar", BetterGameUI.Mod.DrawInterface_Hotbar,
                        InterfaceScaleType.UI);
                }
            }
        }
    }
}