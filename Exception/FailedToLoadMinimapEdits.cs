using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class FailedToLoadMinimapEdits : System.Exception
    {
        public FailedToLoadMinimapEdits() :  base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadMinimapEdits").
                GetTranslation(Language.ActiveCulture)) { }
        public FailedToLoadMinimapEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadMinimapEdits").
                GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
