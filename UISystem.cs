using BetterGameUI.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.Main;

namespace BetterGameUI {
    public class UISystem : ModSystem {
        // TODO: remove this field if possible
        public static GameTime LastUpdateUIGameTime { get; set; }
        public static ResourceBarsUI ResourceBarsUI { get; set; }
        public static UserInterface UserInterface { get; set; }
         
        public static bool IsVisible() {
            return UserInterface.CurrentState != null;
        }

        public override void Load() {
            if (!dedServ) {
                ResourceBarsUI = new();
                ResourceBarsUI.Activate();

                UserInterface = new();
                UserInterface.SetState(ResourceBarsUI);
            }
        }

        public override void Unload() {
            LastUpdateUIGameTime = null;
            ResourceBarsUI = null;
            UserInterface = null;
        }

        public override void UpdateUI(GameTime gameTime) {
            LastUpdateUIGameTime = gameTime;
            if (IsVisible())
                UserInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int index1 = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (index1 != -1) {
                // TODO: insert every resource bar as its own element
                layers[index1] = new LegacyGameInterfaceLayer(
                    "Enchantments: Resource Bars",
                    delegate {
                        if (LastUpdateUIGameTime != null && IsVisible()) {
                            UserInterface.Draw(spriteBatch, LastUpdateUIGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI);
            }
        }
    }
}