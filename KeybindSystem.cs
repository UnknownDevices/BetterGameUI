using Terraria.ModLoader;

namespace BetterGameUI
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind MouseScrollToFocusBuffIconsBar { get; set; }
        public static ModKeybind LockHotbar { get; set; }

        public override void Load() {
            MouseScrollToFocusBuffIconsBar = KeybindLoader.RegisterKeybind(Mod, "Mouse Scroll Focuses Buff Icons' Bar", "LeftAlt");
            LockHotbar = KeybindLoader.RegisterKeybind(Mod, "Lock Hotbar", "N");
            // TODO: queue mouse scroll input for hotbar if an item is in use
        }

        public override void Unload() {
            MouseScrollToFocusBuffIconsBar = null;
            LockHotbar = null;
        }
    }
}