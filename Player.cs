using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Player : ModPlayer
    {
        private static int preselectedItem;
        private static bool hasControlUseItemStoppedSinceItemAnimationStarted;

        public static int PreselectedItem { get => preselectedItem; set => preselectedItem = value; }

        public static bool HasControlUseItemStoppedSinceItemAnimationStarted {
            get => hasControlUseItemStoppedSinceItemAnimationStarted;
            set => hasControlUseItemStoppedSinceItemAnimationStarted = value;
        }

        public static float MouseX => PlayerInput.MouseInfo.X / Main.UIScale;
        public static float MouseY => PlayerInput.MouseInfo.Y / Main.UIScale;

        public static int Scroll {
            get {
                int output = -PlayerInput.ScrollWheelDeltaForUI / 120;

                output += XButtonsWithCD;
                return output;
            }
        }

        public static int XButtonsWithCD {
            get {
                int buttons = PlayerInput.Triggers.Current.HotbarPlus.ToInt() - PlayerInput.Triggers.Current.HotbarMinus.ToInt();
                if (PlayerInput.CurrentProfile.HotbarAllowsRadial && buttons != 0 && PlayerInput.Triggers.Current.HotbarHoldTime > PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired && PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired != -1) {
                    PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed = true;
                    PlayerInput.Triggers.Current.HotbarScrollCD = 2;
                }

                if (PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired != -1) {
                    buttons = PlayerInput.Triggers.JustReleased.HotbarPlus.ToInt() - PlayerInput.Triggers.JustReleased.HotbarMinus.ToInt();
                    if (PlayerInput.Triggers.Current.HotbarScrollCD == 1)
                        buttons = 0;
                }

                if (PlayerInput.Triggers.Current.HotbarScrollCD == 0 && buttons != 0) {
                    PlayerInput.Triggers.Current.HotbarScrollCD = 8;
                    return buttons;
                }
                else {
                    return 0;
                }
            }
        }

        public override void OnEnterWorld(Terraria.Player player) {
            if (BetterGameUI.Mod.Config.Notifications_ShowStartupMessageForImportantChangeNotes_0_4_0_1) {
                var text = Language.GetTextValue("Mods.BetterGameUI.Message.ImportantChangeNotes",
                    Language.GetTextValue("Mods.BetterGameUI.CompactName"),
                    Language.GetTextValue("Mods.BetterGameUI.Version"));
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
        }
    }
}