using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Player : ModPlayer
    {
        public static bool MouseScrollIsFocusingBuffsBar;

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (KeybindSystem.LockHotbar.JustPressed) {
                Main.player[Main.myPlayer].hbLocked ^= true;
                Reflection.SoundEngine.PlaySound(22);
            }

            MouseScrollIsFocusingBuffsBar = KeybindSystem.MouseScrollToFocusBuffsBar.Current;
        }
    }
}