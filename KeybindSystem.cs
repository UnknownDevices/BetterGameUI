using Terraria.ModLoader;

namespace BetterGameUI
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind HoldForMouseScrollToFocusBuffIconsBar { get; set; }

        public override void Load() {
            HoldForMouseScrollToFocusBuffIconsBar = KeybindLoader.RegisterKeybind(Mod, "Hold For Mouse Scroll To Focus Buff Icons' Bar", "LeftAlt");
            // TODO: lock scrollbar keybind
            // TODO: queue mouse scroll input if an item is in use
        }

        public override void Unload() {
            HoldForMouseScrollToFocusBuffIconsBar = null;
        }
    }
}