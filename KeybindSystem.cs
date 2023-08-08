using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind ActivateBuffsBarScroll { get; set; }
        public static ModKeybind ToggleLockingUIs { get; set; }

        public override void Load() {
            ActivateBuffsBarScroll = KeybindLoader.RegisterKeybind(
                Mod,
                "ActivateBuffsBarScroll",
                "B");
            ToggleLockingUIs = KeybindLoader.RegisterKeybind(
                Mod,
                "ToggleLockingUIs",
                "N");
        }

        public override void Unload() {
            ActivateBuffsBarScroll = null;
            ToggleLockingUIs = null;
        }
    }
}