// STFU Microsoft
#pragma warning disable CA2211

using BetterGameUI.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using BetterGameUI.Edits;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: highlight the selected item even when this is in a coin or ammo slot
        // TODO: play sound when auto use item changes
        // TODO: signal auto select being active through some other, unique way
        // TODO: consider crafting item clicked on crafting window
        // TODO: consider displaying item's name on top of hotbar even while the inventory is up
        // TODO: OnServerConfigChanged is not needed at the time but do consider it
        // TODO: visually signal somehow when the hotbar is locked without needing to open the inventory
        // FIXME: text of baner buff icon has trouble displaying full text if the icon is too low on the screen - 12/22/22: can't replicate bug
        public static event Action OnClientConfigChanged;
        public static ClientConfig ClientConfig { get; internal set; }
        public static List<int> ActiveBuffsIndexes { get; internal set; }

        internal static void RaiseClientConfigChanged() => OnClientConfigChanged?.Invoke();

        public override void Load() {
            if (!ClientConfig.Compatibility_DisableChangesToTheBuffsBars) {
                BuffsBarsEdits.Load();
            }
            if (!ClientConfig.Compatibility_DisableChangesToTheHotbar) {
                HotbarEdits.Load();
            }
            if (!ClientConfig.Compatibility_DisableChangesToTheItemSlots) {
                ItemSlotsEdits.Load();
            }
            if (!ClientConfig.Compatibility_DisableChangesToTheAccessorySlots) {
                AccessorySlotsEdits.Load();
            }
            if (!ClientConfig.Compatibility_DisableChangesToTheMinimap) {
                MinimapEdits.Load(); ;
            }

            if (Main.netMode != NetmodeID.Server) {
                BetterGameUI.Assets.Load();
            }
        }

        public override void Unload() {
            OnClientConfigChanged = null;
            ClientConfig = null;
            ActiveBuffsIndexes = null;

            BetterGameUI.Assets.Unload();
        }
    }
}