using Terraria.Localization;

namespace BetterGameUI.Exception
{
    // TODO: move to exceptions folder
    public class LoadHotbarChanges : System.Exception
    {
        public LoadHotbarChanges() : 
            base(Language.GetTextValue("Mods.BetterGameUI.ExceptionMessage.LoadHotbarChanges")) { }
        public LoadHotbarChanges(System.Exception innerException) : 
            base(Language.GetTextValue("Mods.BetterGameUI.ExceptionMessage.LoadHotbarChanges"), innerException) { }
    }
}
