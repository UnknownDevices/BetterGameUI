using Terraria.ModLoader;

namespace BetterGameUI
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind MouseScrollToFocusBuffsBar { get; set; }
        public static ModKeybind LockHotbar { get; set; }

        public override void Load() {
            MouseScrollToFocusBuffsBar = KeybindLoader.RegisterKeybind(Mod, "Mouse Scroll Focuses Buff Icons' Bar", "LeftAlt");
            LockHotbar = KeybindLoader.RegisterKeybind(Mod, "Lock Hotbar", "N");
        }

        public override void Unload() {
            MouseScrollToFocusBuffsBar = null;
            LockHotbar = null;
        }
    }
}