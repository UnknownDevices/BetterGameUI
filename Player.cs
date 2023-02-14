using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Player : ModPlayer
    {
        public static bool MouseScrollIsFocusingBuffsBar { get; internal set; }

        public override void OnEnterWorld(Terraria.Player player) {
            // no important change notes this update
            if (BetterGameUI.Mod.ClientConfig.ShowStartupMessageForImportantChangeNotes_0_3_8_0 && false) {
                Main.NewText(Messages.ImportantChangeNotes(), Color.Yellow);
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (KeybindSystem.LockHotbar.JustPressed) {
                Main.player[Main.myPlayer].hbLocked ^= true;
                Reflection.SoundEngineReflection.PlaySound(22);
            }

            MouseScrollIsFocusingBuffsBar = KeybindSystem.MouseScrollToFocusBuffsBar.Current;
        }
    }
}
