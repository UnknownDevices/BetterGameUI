using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Player : ModPlayer
    {
        public static bool IsMouseScrollAllowed { get; set; }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (KeybindSystem.AllowMouseScroll.GetAssignedKeys().Count <= 0) {
                IsMouseScrollAllowed = true;
            }
            else switch (BetterGameUI.Mod.ClientConfig.AllowMouseScrollKeybindMode) {
                    case KeybindMode.Hold: {
                            IsMouseScrollAllowed = KeybindSystem.AllowMouseScroll.Current;
                        }
                        break;

                    case KeybindMode.Toggle: {
                            if (KeybindSystem.AllowMouseScroll.JustPressed) {
                                IsMouseScrollAllowed ^= true;
                            }
                        }
                        break;
                }

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