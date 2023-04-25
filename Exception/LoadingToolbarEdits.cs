using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingToolbarEdits : System.Exception
    {
        public LoadingToolbarEdits() :  base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadToolsBarEdits").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingToolbarEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadToolsBarEdits").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
