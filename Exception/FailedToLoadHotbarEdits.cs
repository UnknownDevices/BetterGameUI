using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class FailedToLoadHotbarEdits : System.Exception
    {
        public FailedToLoadHotbarEdits() :  base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadHotbarEdits").
                GetTranslation(Language.ActiveCulture)) { }
        public FailedToLoadHotbarEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadHotbarEdits").
                GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
