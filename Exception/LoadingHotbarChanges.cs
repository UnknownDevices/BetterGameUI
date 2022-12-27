using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingHotbarChanges : System.Exception
    {
        public LoadingHotbarChanges() :  base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadHotbarChanges").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingHotbarChanges(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadHotbarChanges").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
