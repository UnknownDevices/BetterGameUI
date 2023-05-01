using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class FailedToLoadToolbarEdits : System.Exception
    {
        public FailedToLoadToolbarEdits() :  base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadToolsBarEdits").
                GetTranslation(Language.ActiveCulture)) { }
        public FailedToLoadToolbarEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadToolsBarEdits").
                GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
