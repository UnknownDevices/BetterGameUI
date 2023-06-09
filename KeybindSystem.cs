using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind BuffListScrollIsActive { get; set; }
        public static ModKeybind LockHotbar { get; set; }

        public override void Load() {
            BuffListScrollIsActive = KeybindLoader.RegisterKeybind(
                Mod,
                LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.Keybinds.ActivateBuffListScroll").GetTranslation(Language.ActiveCulture),
                "B");
            LockHotbar = KeybindLoader.RegisterKeybind(
                Mod,
                LocalizationLoader.GetOrCreateTranslation("Mods.BetterGameUI.Keybinds.ToggleUILock").GetTranslation(Language.ActiveCulture),
                "N");
        }

        public override void Unload() {
            BuffListScrollIsActive = null;
            LockHotbar = null;
        }
    }
}