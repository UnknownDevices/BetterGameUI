﻿using BetterGameUI.UI;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace BetterGameUI
{
    [Label("$Mods.BetterGameUI.Config.Title.ClientConfig")]
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => BetterGameUI.Mod.ClientConfig = this;

        public override void OnChanged() => BetterGameUI.Mod.RaiseClientConfigChanged();

        // TODO: config to set bottom or right of an icon bar alternatively
        // TODO: config to invert icons vertical order
        // TODO: draw entire equip pages relative to the bottom right of the screen?
        // TODO: Invert mouse scroll input config
        // TODO: have tooltips display the min and max value for the field, as well as the reasoning for these if not obvious
        // TODO: have ReloadRequired fields mention they are so in their tooltips
        // TODO: do localization

        // TODO: look into making custom config fields?
        // TODO: someway to reset individual fields to default
        [Header("Input Config")]
        [DefaultValue(KeybindMode.Hold)]
        [Label("Allow mouse scroll keybind mode")]
        public KeybindMode AllowMouseScrollKeybindMode { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Game's icons bar hitbox width modifier")]
        public int GameIconsBarHitboxWidthModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Game's icons bar hitbox height modifier")]
        public int GameIconsBarHitboxHeightModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Inventory's icons bar hitbox width modifier")]
        public int InventoryIconsBarHitboxWidthModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Inventory's icons bar hitbox height modifier")]
        public int InventoryIconsBarHitboxHeightModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller hitbox width modifier")]
        public int ScrollerHitboxWidthModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller hitbox height modifier")]
        public int ScrollerHitboxHeightModifier { get; set; }

        [DefaultValue(true)]
        [Label("Allow scroller dragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(false)]
        [Label("Only allow mouse scroll when hovering UI")]
        public bool OnlyAllowMouseScrollWhenHoveringUI { get; set; }

        [DefaultValue(false)]
        [Label("Never allow mouse scroll")]
        public bool NeverAllowMouseScroll { get; set; }

        [DefaultValue(true)]
        [Label("Smart lock vanilla mouse scroll")]
        public bool SmartLockVanillaMouseScroll { get; set; }

        [DefaultValue(true)]
        [Label("Lock game's icons bar when hotbar locks")]
        public bool LockGameIconsBarWhenHotbarLocks { get; set; }

        [Header("Game's Buff Icons Bar Config")]
        [DefaultValue(ScrollbarPosition.LeftOfIcons)]
        [Label("Scrollbar position")]
        public ScrollbarPosition GameScrollbarPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.LeftToRight)]
        [Label("Icons horizontal order")]
        public BuffIconsHorOrder GameIconsHorOrder { get; set; }

        [DefaultValue(false)]
        [Label("Never hide scrollbar")]
        public bool GameNeverHideScrollbar { get; set; }

        [DefaultValue(32)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("X pixels")]
        public int GameBarXPxs { get; set; }

        [DefaultValue(0f)]
        [Range(float.MinValue, float.MaxValue)]
        [Label("X percent")]
        public int GameBarXPercent { get; set; }

        [DefaultValue(76)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Y pixels")]
        public int GameBarYPxs { get; set; }

        [DefaultValue(0f)]
        [Range(float.MinValue, float.MaxValue)]
        [Label("Y percent")]
        public int GameBarYPercent { get; set; }

        [DefaultValue(2)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRowsCount")]
        public int GameBarIconRowsCount { get; set; }

        [DefaultValue(11)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconColsCount")]
        public int GameBarIconColsCount { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinScrollerHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinScrollerHeight")]
        public int GameBarMinScrollerHeight { get; set; }

        [DefaultValue(false)]
        [Label("Never hide scrollbar")]
        public bool GameBarNeverHideScrollbar { get; set; }

        [Header("Inventory's Buff Icons Bar Config")]
        [DefaultValue(ScrollbarPosition.RightOfIcons)]
        [Label("Scrollbar position")]
        public ScrollbarPosition InventoryScrollbarPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.RightToLeft)]
        [Label("Icons horizontal order")]
        public BuffIconsHorOrder InventoryIconsHorOrder { get; set; }

        [DefaultValue(-272)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("X pixels")]
        public int InventoryBarXPxs { get; set; }

        [DefaultValue(1f)]
        [Range(float.MinValue, float.MaxValue)]
        [Label("X percent")]
        public int InventoryBarXPercent { get; set; }

        [DefaultValue(-252)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Y pixels")]
        public int InventoryBarYPxs { get; set; }

        [DefaultValue(1f)]
        [Range(float.MinValue, float.MaxValue)]
        [Label("Y percent")]
        public int InventoryBarYPercent { get; set; }

        [DefaultValue(3)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRowsCount")]
        public int InventoryBarIconRowsCount { get; set; }

        [DefaultValue(6)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconColsCount")]
        public int InventoryBarIconColsCount { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinScrollerHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinScrollerHeight")]
        public int InventoryBarMinScrollerHeight { get; set; }

        [DefaultValue(false)]
        [Label("Never hide scrollbar")]
        public bool InventoryBarNeverHideScrollbar { get; set; }

        [Header("Misc Config")]
        [ReloadRequired]
        [DefaultValue(0)]
        [Range(uint.MinValue, uint.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ExtraPlayerBuffSlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ExtraPlayerBuffSlots")]
        public int ExtraPlayerBuffSlots { get; set; }
    }
}