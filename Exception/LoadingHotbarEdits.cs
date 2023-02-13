using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingHotbarEdits : System.Exception
    {
        public LoadingHotbarEdits() :  base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadHotbarEdits").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingHotbarEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadHotbarEdits").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
