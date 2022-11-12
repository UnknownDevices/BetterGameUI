using Terraria.ModLoader;

namespace BetterGameUI
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind ScrollUp { get; set; }
        public static ModKeybind ScrollDown { get; set; }
        public static ModKeybind AllowMouseScroll { get; set; }

        public override void Load() {
            ScrollUp = KeybindLoader.RegisterKeybind(Mod, "Scroll Up", "Up");
            ScrollDown = KeybindLoader.RegisterKeybind(Mod, "Scroll Down", "Down");
            AllowMouseScroll = KeybindLoader.RegisterKeybind(Mod, "Allow Mouse Scroll", "LeftAlt");
            // TODO: lock scrollbar keybind
            // TODO: queue mouse scroll if mouse left current
        }

        public override void Unload() {
            ScrollUp = null;
            ScrollDown = null;
            AllowMouseScroll = null;
        }
    }
}