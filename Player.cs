using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Player : ModPlayer
    {
        public static bool MouseScrollIsFocusingBuffIconsBar;

        public override void ProcessTriggers(TriggersSet triggersSet) {
            Main.player[Main.myPlayer].hbLocked ^= KeybindSystem.LockHotbar.JustPressed;

            MouseScrollIsFocusingBuffIconsBar = KeybindSystem.MouseScrollToFocusBuffIconsBar.Current;
        }
    }
}