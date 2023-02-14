using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingMinimapEdits : System.Exception
    {
        public LoadingMinimapEdits() :  base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadMinimapEdits").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingMinimapEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadMinimapEdits").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
