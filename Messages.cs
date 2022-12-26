using Terraria.Localization;
using System;
using Steamworks;

namespace BetterGameUI
{
    public static class Messages
    {
        // TODO: use GetTextValue with args overload
        public static string ImportantChangeNotes() =>
            Language.GetTextValue("Mods.BetterGameUI.Message.ImportantChangeNotes",
                Language.GetTextValue("Mods.BetterGameUI.CompactName"),
                Language.GetTextValue("Mods.BetterGameUI.Version"));

        public static string ErrorLoadingDisableableChanges(string changesTarget) =>
            Language.GetTextValue("Mods.BetterGameUI.Message.ErrorLoadingDisableableChanges",
                Language.GetTextValue("Mods.BetterGameUI.CompactName"),
                Language.GetTextValue("Mods.BetterGameUI.Version"),
                changesTarget);

        public static string ErrorLoadingChangesToTheHotbar() => ErrorLoadingDisableableChanges("the hotbar");
        public static string ErrorLoadingChangesToTheItemSlots() => ErrorLoadingDisableableChanges("the item slots");
    }
}
