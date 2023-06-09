// STFU Microsoft
#pragma warning disable CA2211

using BetterGameUI.IL;
using System;
using Terraria;
using Terraria.ID;

namespace BetterGameUI
{
    public class Mod : Terraria.ModLoader.Mod
    {
        // TODO: highlight the selected item even when this is in a coin or ammo slot
        // TODO: play sound when auto use item changes
        // TODO: signal auto select being active through some other, unique way
        // FIXME: text of baner buff icon has trouble displaying full text if the icon is too low on the screen - 12/22/22: can't replicate bug
        public static event Action OnConfigChanged;

        public static Config Config { get; internal set; }

        internal static void RaiseConfigChanged() => OnConfigChanged?.Invoke();

        public override void Load() {
            if (Main.netMode != NetmodeID.Server) {
                BetterGameUI.Assets.Load();
                BetterGameUI.UISystem.Load();

                if (Config.Feature_BuffListsScrollbar) {
                    BuffListScrollbarLoader.Load();
                }

                if (Config.Feature_AccessorySlotsImprovedScrollbar) {
                    AccessorySlotsImprovedScrollbarLoader.Load();
                }

                if (Config.Feature_InteractiveUIsWhileUsingItem) {
                    InteractiveUIsWhileUsingItemLoader.Load();
                }

                if (Config.Feature_XButtonsScrollRecipeList) {
                    XButtonsScrollRecipeListLoader.Load();
                }

                if (Config.Feature_InvertedMouseScrollForRecipeList) {
                    InvertedMouseScrollForRecipeListLoader.Load();
                }

                if (Config.Feature_DraggingOverHotbarSlotDoesntSelectItem) {
                    DraggingOverHotbarSlotDoesntSelectItemLoader.Load();
                }

                if (Config.Feature_DraggingOverBuffIconDoesntInterruptClick) {
                    DraggingOverBuffIconDoesntInterruptClickLoader.Load();
                }

                if (Config.Feature_LockingHotbarLocksAdditionalUIs) {
                    LockingHotbarLocksAdditionalUIsLoader.Load();
                }

                if (Config.Feature_SelectedItemIsHighlightedInInventory) {
                    SelectedItemIsHighlightedInInventoryLoader.Load();
                }

                if (Config.Feature_FixForHotbarFlickerUponOpeningFancyUI) {
                    FixForHotbarFlickerUponOpeningFancyUILoader.Load();
                }
            }
        }

        public override void Unload() {
            OnConfigChanged = null;
            Config = null;

            BetterGameUI.Assets.Unload();
            BetterGameUI.UISystem.Unload();
        }
    }
}