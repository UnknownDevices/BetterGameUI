using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI.Exception
{
    public class FailedToLoadFeature : System.Exception
    {
        // TODO: take key to featureName
        public FailedToLoadFeature(string FeatureKey) : base(ErrorMessage(FeatureKey)) { }

        public FailedToLoadFeature(string FeatureKey, System.Exception innerException) : base(ErrorMessage(FeatureKey), innerException) {
        }

        public static string ErrorMessage(string FeatureKey) {
            return string.Format(LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.ExceptionMessage.FailedToLoadFeature").GetTranslation(Language.ActiveCulture),
                new object[] { LocalizationLoader.GetOrCreateTranslation(FeatureKey).GetTranslation(Language.ActiveCulture) });
        }
    }
}