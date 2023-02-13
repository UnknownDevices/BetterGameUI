using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingAccessorySlotsEdits : System.Exception
    {
        public LoadingAccessorySlotsEdits() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadAccessorySlotLoaderEdits").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingAccessorySlotsEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadAccessorySlotLoaderEdits").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
