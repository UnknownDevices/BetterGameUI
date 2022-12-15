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

        // TODO: have tooltips display the min and max value for the field, as well as the reasoning for these if not obvious
        // TODO: have ReloadRequired fields mention they are so in their tooltips
        // TODO: do localization for spanish

        // ------------- Input Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InputConfig")]
        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.BuffsBarHitboxModifier")]
        public int BuffsBarHitboxModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ScrollerHitboxModifier")]
        public int ScrollerHitboxModifier { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinimalScrollerHeight")]
        public int MinimalScrollerHeight { get; set; }

        // TODO: consider if this should also affect vanilla mouse scroll
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertMouseScrollForScrollbar")]
        public bool InvertMouseScrollForScrollbar { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.AllowScrollerDragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.MouseScrollFocusesMouseHoveredUI")]
        public bool MouseScrollFocusesMouseHoveredUI { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.SmartHideScrollbar")]
        public bool SmartHideScrollbar { get; set; }

        // ------------- Inventory Down Buffs' Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InventoryDownBuffsBarConfig")]
        [DefaultValue(ScrollbarPosition.LeftOfIcons)]
        [Label("$Mods.BetterGameUI.Config.Label.ScrollbarRelPos")]
        public ScrollbarPosition InventoryDownScrollbarRelPos { get; set; }

        [DefaultValue(BuffIconsHorOrder.LeftToRight)]
        [Label("$Mods.BetterGameUI.Config.Label.IconsHorOrder")]
        public BuffIconsHorOrder InventoryDownIconsHorOrder { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.XOffset")]
        public int InventoryDownXOffset { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.YOffset")]
        public int InventoryDownYOffset { get; set; }

        [DefaultValue(2)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        public int InventoryDownIconRowsCount { get; set; }

        [DefaultValue(11)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        public int InventoryDownIconColsCount { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.HotbarLockingAlsoLocksThis")]
        public bool InventoryDownHotbarLockingAlsoLocksThis { get; set; }

        // ------------- Inventory Up Buffs' Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InventoryUpBuffsBarConfig")]
        [DefaultValue(ScrollbarPosition.RightOfIcons)]
        [Label("$Mods.BetterGameUI.Config.Label.ScrollbarRelPos")]
        public ScrollbarPosition InventoryUpScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.RightToLeft)]
        [Label("$Mods.BetterGameUI.Config.Label.IconsHorOrder")]
        public BuffIconsHorOrder InventoryUpIconsHorOrder { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.XOffset")]
        public int InventoryUpXOffset { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.YOffset")]
        public int InventoryUpYOffset { get; set; }

        [DefaultValue(4)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        public int InventoryUpIconRowsCount { get; set; }

        [DefaultValue(6)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        public int InventoryUpIconColsCount { get; set; }

        // ------------- Misc Config ------------- //
        
        [Header("$Mods.BetterGameUI.Config.Header.MiscConfig")]
        [ReloadRequired]
        [DefaultValue(0)]
        [Range(uint.MinValue, uint.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ExtraPlayerBuffSlots")]
        public int ExtraPlayerBuffSlots { get; set; }
    }
}