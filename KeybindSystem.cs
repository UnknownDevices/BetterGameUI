using Terraria.ModLoader;

namespace BetterGameUI
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind ScrollUp { get; set; }
        public static ModKeybind ScrollDown { get; set; }
        public static ModKeybind AllowMouseScroll { get; set; }

        public override void Load() {
            // TODO: this should affect vanilla scrolling too
            ScrollUp = KeybindLoader.RegisterKeybind(Mod, "Scroll Up", "Up");
            ScrollDown = KeybindLoader.RegisterKeybind(Mod, "Scroll Down", "Down");
            // TODO: separate into game's and inventory's keybind
            AllowMouseScroll = KeybindLoader.RegisterKeybind(Mod, "Hold For Mouse Scroll To Focus Buff Icons' Bar", "LeftAlt");
            // TODO: lock scrollbar keybind
            // TODO: queue mouse scroll input if an item is in use
        }

        public override void Unload() {
            ScrollUp = null;
            ScrollDown = null;
            AllowMouseScroll = null;
        }
    }
}