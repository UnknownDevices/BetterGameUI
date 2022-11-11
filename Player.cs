using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetterGameUI {
    public class Player : ModPlayer {
        public static bool IsMouseScrollAllowed { get; set; }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (KeybindSystem.AllowMouseScroll.GetAssignedKeys().Count <= 0) {
                IsMouseScrollAllowed = true;
            } else switch (BetterGameUI.Mod.ClientConfig.AllowMouseScrollMode) {
                case KeybindMode.Hold: {
                    IsMouseScrollAllowed = KeybindSystem.AllowMouseScroll.Current;
                } break;
                case KeybindMode.Toggle: {
                    if (KeybindSystem.AllowMouseScroll.JustPressed) {
                        IsMouseScrollAllowed ^= true;
                    }
                } break;
            }

            //if (KeybindSystem.ScrollUp.JustPressed) {
            //    // TODO: shouldn't scroll if dragging??
            //    // FIXME: wut
            //    UISystem.BuffIconsBarUI.ScrollbarUI.Scrolls++;
            //}

            //if (KeybindSystem.ScrollDown.JustPressed) {
            //    UISystem.BuffIconsBarUI.ScrollbarUI.Scrolls--;
            //}
        }
    }
}
