using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        public static event Action OnClientConfigChanged;

        // FIXME: text of baner buff icon has trouble displaying full text if the icon is too low on the screen
        // TODO: only the count of this will be needed
        public static List<int> ActiveBuffsIndexes { get; set; }
        public static ClientConfig ClientConfig { get; set; }
        public override uint ExtraPlayerBuffSlots { get => (uint)ClientConfig.AdditionalPlayerBuffSlots; }

        internal static void RaiseClientConfigChanged() => OnClientConfigChanged?.Invoke();

        public static void UpdateActiveBuffsIndexes() {
            ActiveBuffsIndexes = new List<int>(Terraria.Player.MaxBuffs);
            for (int i = 0; i < Terraria.Player.MaxBuffs; ++i) {
                if (Main.player[Main.myPlayer].buffType[i] > 0) {
                    ActiveBuffsIndexes.Add(i);
                }
            }
        }

        public override void Load() {
            if (Main.netMode != NetmodeID.Server) {
                BetterGameUI.Assets.Load();
            }
        }

        public override void Unload() {
            OnClientConfigChanged = null;
            ClientConfig = null;

            BetterGameUI.Assets.Unload();
        }
    }
}