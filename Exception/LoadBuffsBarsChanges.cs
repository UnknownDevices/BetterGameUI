using Terraria.Localization;

namespace BetterGameUI.Exception
{
    public class LoadBuffsBarsChanges : System.Exception
    {
        public LoadBuffsBarsChanges() : 
            base(Language.GetTextValue("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarChanges")) { }
        public LoadBuffsBarsChanges(System.Exception innerException) : 
            base(Language.GetTextValue("Mods.BetterGameUI.ExceptionMessage.LoadBuffsBarChanges"), innerException) { }
    }
}
