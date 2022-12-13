using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Player : ModPlayer
    {
        public static bool MouseScrollIsFocusingBuffIconsBar;
        public static int ExtraMouseScrollForBuffIconsBarScrollbar;

        public override void ProcessTriggers(TriggersSet triggersSet) {
            ExtraMouseScrollForBuffIconsBarScrollbar = 0;
            MouseScrollIsFocusingBuffIconsBar = KeybindSystem.HoldForMouseScrollToFocusBuffIconsBar.Current;

            // NOTE: when the inventory is up, vanilla Terraria doesn't listen to MouseX1 or MouseX2
            // TODO: consider renaming MouseScroll to ScrollWheel everywhere
            // TODO: should also scroll if holding key? if so, after what delay after pressing, maybe make that a config
            if (PlayerInput.Triggers.JustPressed.MouseXButton1) {
                ExtraMouseScrollForBuffIconsBarScrollbar++;
            }
            if (PlayerInput.Triggers.JustPressed.MouseXButton2) {
                ExtraMouseScrollForBuffIconsBarScrollbar--;
            }
        }
    }
}