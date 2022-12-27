using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class LoadingItemSlotsChanges : System.Exception
    {
        public LoadingItemSlotsChanges() : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsChanges").
            GetTranslation(Language.ActiveCulture)) { }
        public LoadingItemSlotsChanges(System.Exception innerException) : base(
            LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsChanges").
            GetTranslation(Language.ActiveCulture), innerException) { }
    }
}
