using Terraria.Localization;

namespace BetterGameUI.Exception
{
    public class LoadItemSlotsChanges : System.Exception
    {
        public LoadItemSlotsChanges() :
            base(Language.GetTextValue("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsChanges")) { }
        public LoadItemSlotsChanges(System.Exception innerException) :
            base(Language.GetTextValue("Mods.BetterGameUI.ExceptionMessage.LoadItemSlotsChanges"), innerException) { }
    }
}
