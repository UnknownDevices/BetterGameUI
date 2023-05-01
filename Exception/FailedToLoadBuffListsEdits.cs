using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class FailedToLoadBuffListsEdits : System.Exception
    {
        public FailedToLoadBuffListsEdits() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarsEdits").
                GetTranslation(Language.ActiveCulture)) { }
        public FailedToLoadBuffListsEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarsEdits").
                GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
