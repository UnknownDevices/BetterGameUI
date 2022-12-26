using Terraria.Localization;

namespace BetterGameUI
{
    public static class Messages
    {
        public static string ImportantChangeNotes() =>
            Language.GetTextValue("Mods.BetterGameUI.Message.ImportantChangeNotes",
                Language.GetTextValue("Mods.BetterGameUI.CompactName"),
                Language.GetTextValue("Mods.BetterGameUI.Version"));
    }
}
