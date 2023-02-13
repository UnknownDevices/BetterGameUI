using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingBuffsBarsEdits : System.Exception
    {
        public LoadingBuffsBarsEdits() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarsEdits").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingBuffsBarsEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarsEdits").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
