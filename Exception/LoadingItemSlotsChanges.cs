using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingItemSlotsEdits : System.Exception
    {
        public LoadingItemSlotsEdits() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsEdits").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingItemSlotsEdits(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsEdits").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
