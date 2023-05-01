using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class FailedToLoadItemSlotsEdits : System.Exception
    {
        public FailedToLoadItemSlotsEdits() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsEdits").
                GetTranslation(Language.ActiveCulture)) { }
        public FailedToLoadItemSlotsEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsEdits").
                GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
