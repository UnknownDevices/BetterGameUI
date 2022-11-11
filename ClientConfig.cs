using System.ComponentModel;
using Terraria.ModLoader.Config;
using System;
using Humanizer;
using Newtonsoft.Json.Linq;

namespace BetterGameUI
{
    // TODO: decide on color scheme
    [Label("$Mods.BetterGameUI.Config.Title.ClientConfig")]
    public class ClientConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public override void OnLoaded() => BetterGameUI.Mod.ClientConfig = this;
        public override void OnChanged() => BetterGameUI.Mod.RaiseClientConfigChanged();

        // TODO: someway to reset individual fields to default
        // TODO: Rename mod to BetterGameUI
        // TODO: Icons horizontal order config??
        // TODO: Icons vertical order config??
        // TODO: do localization
        // TODO: make custom config fields, have input field one clamp values outside given range
        // FIXME: input field sprite looks too pixely
        [DefaultValue(KeybindMode.Hold)]
        [Label("Allow mouse scroll mode")]
        public KeybindMode AllowMouseScrollMode { get; set; }
        
        // TODO: sort into categories
        // TODO: have tooltips display the min and max value for the field, as well as the reasoning for these if not obvious
        [DefaultValue(false)]
        [Label("Never allow mouse scroll")]
        public bool NeverAllowMouseScroll { get; set; }

        // TODO: Invert mouse scroll config

        [DefaultValue(false)]
        [Label("Hover UI to allow mouse scroll")]
        public bool HoverUIToAllowMouseScroll { get; set; }

         [DefaultValue(true)]
        [Label("Smart lock vanilla mouse scroll")]
        public bool SmartLockVanillaMouseScroll { get; set; }

        [DefaultValue(true)]
        [Label("Lock when hotbar is locked")]
        public bool LockWhenHotbarIsLocked { get; set; }

        [DefaultValue(true)]
        [Label("Allow scroller dragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller hitbox width modifier")]
        public int ScrollerHitboxWidthModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller hitbox height modifier")]
        public int ScrollerHitboxHeightModifier { get; set; }

        // TODO: have ReloadRequired fields mention they are so in their tooltips
        [ReloadRequired]
        [DefaultValue(0)]
        [Range(uint.MinValue, uint.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ExtraPlayerBuffSlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ExtraPlayerBuffSlots")]
        public int ExtraPlayerBuffSlots { get; set; }
        
        // TODO: public ... SnapPoint;
        
        [DefaultValue(16)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("X")]
        public int X { get; set; }

        [DefaultValue(76)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Y")]
        public int Y { get; set; }

        [DefaultValue(2)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRowsCount")]
        public int IconRowsCount { get; set; }

        [DefaultValue(11)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconColsCount")]
        public int IconColsCount { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinScrollerHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinScrollerHeight")]
        public int MinScrollerHeight { get; set; }

        [DefaultValue(false)]
        [Label("Always show scrollbar")]
        public bool AlwaysShowScrollbar { get; set; }
    }
}
