using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace BetterGameUI
{
    public class Player : ModPlayer {
        public static bool BuffListHasWheelScrollFocus { get; internal set; }
        public static float MouseX => PlayerInput.MouseInfo.X / Main.UIScale;
        public static float MouseY => PlayerInput.MouseInfo.Y / Main.UIScale;
        public static int WheelScrollAndXButtons {
            get {
                int value = 0;
                if (PlayerInput.Triggers.JustPressed.MouseXButton1) {
                    value += 1;
                }
                if (PlayerInput.Triggers.JustPressed.MouseXButton2) {
                    value -= 1;
                }

                return value - PlayerInput.ScrollWheelDeltaForUI / 120;
            }
        }

        public override void OnEnterWorld(Terraria.Player player) {
            if (BetterGameUI.Mod.ClientConfig.Notifications_ShowStartupMessageForImportantChangeNotes_0_3_11_1) {
                var text = Messages.ImportantChangeNotes();
                if (text != "") {
                    Main.NewText(text, Color.Yellow);
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (KeybindSystem.LockHotbar.JustPressed) {
                Main.LocalPlayer.hbLocked ^= true;
                Reflection.SoundEngineReflection.PlaySound(22);
            }

            BuffListHasWheelScrollFocus = KeybindSystem.MouseScrollToFocusBuffsBar.Current;
        }

    }
}
