using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class FailedToLoadAccessorySlotsEdits : System.Exception
    {
        public FailedToLoadAccessorySlotsEdits() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadAccessorySlotLoaderEdits").
                GetTranslation(Language.ActiveCulture)) { }
        public FailedToLoadAccessorySlotsEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadAccessorySlotLoaderEdits").
                GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
