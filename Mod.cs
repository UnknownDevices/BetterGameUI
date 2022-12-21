using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: work on features and tweaks list for mod's description
        // TODO: allow scrolling full screen map while using an item
        // TODO: play sound when auto use item changes
        // TODO: always play sound effect when switching to an item from auto use for the first time
        // TODO: signal auto select being active through some other, unique way
        // TODO: consider crafting item clicked on crafting window
        // TODO: consider displaying item's name on top of hotbar even while the inventory is up
        // TODO: OnServerConfigChanged is not needed at the time but do consider it
        public static event Action OnClientConfigChanged;

        // TODO: look into fasterUIs mod in steam
        // TODO: visually signal somehow when the hotbar is locked without needing to open the inventory
        // FIXME: hotbar is drawn for one frame when opening bestiary or emotes window (vanilla error)
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