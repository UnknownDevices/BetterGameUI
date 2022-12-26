// STFU Microsoft
#pragma warning disable CA2211

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BetterGameUI
{
    public enum DisableableFeatures {
        Hotbar,
        ItemSlots,
        Minimap,
    }

    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: scrollbar alfa config
        // TODO: play sound when auto use item changes
        // TODO: signal auto select being active through some other, unique way
        // TODO: consider crafting item clicked on crafting window
        // TODO: consider displaying item's name on top of hotbar even while the inventory is up
        // TODO: OnServerConfigChanged is not needed at the time but do consider it
        // TODO: look into fasterUIs mod in steam
        // TODO: visually signal somehow when the hotbar is locked without needing to open the inventory
        // FIXME: text of baner buff icon has trouble displaying full text if the icon is too low on the screen - 12/22/22: can't replicate bug
        public static event Action OnClientConfigChanged;
        public static ClientConfig ClientConfig { get; internal set; }
        public static List<int> ActiveBuffsIndexes { get; internal set; }
        public static Func<string> LatestLoadFirstErrorMessage { get; internal set; }

        internal static void RaiseClientConfigChanged() => OnClientConfigChanged?.Invoke();

        public override void Load() {
            BuffsBarsChanges.Load();
            if (!ClientConfig.DisableChangesToTheItemSlots) {
                ItemSlotsChanges.Load();
            }
            if (!ClientConfig.DisableChangesToTheHotbar) {
                HotbarChanges.Load();
            }

            if (Main.netMode != NetmodeID.Server) {
                BetterGameUI.Assets.Load();
            }
        }

        public override void Unload() {
            OnClientConfigChanged = null;
            ClientConfig = null;
            ActiveBuffsIndexes = null;
            LatestLoadFirstErrorMessage = null;

            BetterGameUI.Assets.Unload();
        }

        public static bool TrySetLatestLoadFirstErrorMessage(Func<string> message) {
            if (LatestLoadFirstErrorMessage != null) {
                return false;
            }

            LatestLoadFirstErrorMessage = message;
            return true;
        }

    }
}