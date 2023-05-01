// STFU Microsoft
#pragma warning disable CA2211

using BetterGameUI.Edits;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.Main;

namespace BetterGameUI.UI
{
    public class System : ModSystem {
        public static GameTime LastUpdateUIGameTime;

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

        public override void Unload()
        {
            LastUpdateUIGameTime = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            LastUpdateUIGameTime = gameTime;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            layers.Insert(0, new LegacyGameInterfaceLayer(
                "BetterGameUI: Logic 0", DrawInterface_Logic_0,
                InterfaceScaleType.None));

            int index = 0;
            //if (!BetterGameUI.Mod.ClientConfig.Compatibility_DisableChangesToTheBuffsBars) {
            //    index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            //    if (index != -1) {
            //        layers[index] = new LegacyGameInterfaceLayer(
            //            "Vanilla: Resource Bars", DrawInterface_ResourceBars,
            //            InterfaceScaleType.UI);
            //    }
            //}

            if (!BetterGameUI.Mod.ClientConfig.Compatibility_DisableChangesToTheHotbar) {
                index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
                if (index != -1) {
                    layers[index] = new LegacyGameInterfaceLayer(
                        "Vanilla: Hotbar", HotbarEdits.DrawInterface_Hotbar,
                        InterfaceScaleType.UI);
                }
            }
        }
    }
}