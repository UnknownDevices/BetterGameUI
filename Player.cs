using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Player : ModPlayer
    {
        // TODO: reconsider name
        public static bool IsMouseScrollAllowed { get; set; }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            IsMouseScrollAllowed = KeybindSystem.AllowMouseScroll.Current;

            // TODO: shouldn't work if dragging scroller
            if (KeybindSystem.ScrollUp.JustPressed) {
                UISystem.GameBuffIconsBarUI.ScrollbarUI.Scrolls++;
            }

            if (KeybindSystem.ScrollDown.JustPressed) {
                UISystem.GameBuffIconsBarUI.ScrollbarUI.Scrolls--;
            }
        }
    }
}