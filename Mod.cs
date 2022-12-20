using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: OnServerConfigChanged is not needed at the time but do consider it
        public static event Action OnClientConfigChanged;

        // FIXME: hotbar is drawn for one frame when opening bestiary or emotes window
        // FIXME: text of baner buff icon has trouble displaying full text if the icon is too low on the screen
        // updated every frame by DrawInterface_Logic_0
        public static List<int> ActiveBuffsIndexes { get; set; }
        public static ClientConfig ClientConfig { get; set; }
        public static ServerConfig ServerConfig { get; set; }
        public override uint ExtraPlayerBuffSlots { get => (uint)ServerConfig.ExtraPlayerBuffSlots; }

        internal static void RaiseClientConfigChanged() => OnClientConfigChanged?.Invoke();

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