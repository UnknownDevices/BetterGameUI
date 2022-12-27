using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingBuffsBarsChanges : System.Exception
    {
        public LoadingBuffsBarsChanges() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarsChanges").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingBuffsBarsChanges(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarsChanges").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
