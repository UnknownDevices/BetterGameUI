using System;
using System.ComponentModel;
using BetterGameUI.UI;
using Terraria.ModLoader.Config;

namespace BetterGameUI
{
    [Label("$Mods.BetterGameUI.Config.Title.ClientConfig")]
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => BetterGameUI.Mod.ClientConfig = this;

        public override void OnChanged() => BetterGameUI.Mod.RaiseClientConfigChanged();

        // TODO: buff's bar alpha config
        // TODO: do spanish translations

        // ------------- Input Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InputConfig")]
        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.BuffsBarHitboxMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.BuffsBarHitboxMod")]
        public int BuffsBarHitboxMod { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ScrollerHitboxMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ScrollerHitboxMod")]
        public int ScrollerHitboxMod { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.AllowScrollerDragging")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.AllowScrollerDragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.MouseScrollFocusesMouseHoveredUI")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MouseScrollFocusesMouseHoveredUI")]
        public bool MouseScrollFocusesMouseHoveredUI { get; set; }

        // TODO: consider if this should also affect vanilla mouse scroll
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertMouseScrollForScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.InvertMouseScrollForScrollbar")]
        public bool InvertMouseScrollForScrollbar { get; set; }

        // ------------- General UI Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.GeneralUIConfig")]
        [DefaultValue(12)]
        [Range((int)6, int.MaxValue)]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinimalScrollerHeight")]
        public int MinimalScrollerHeight { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.SmartHideScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.SmartHideScrollbar")]
        public bool SmartHideScrollbar { get; set; }

        // ------------- Inventory Down Buffs' Bar UI Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InventoryDownBuffsBarConfig")]
        [DefaultValue(ScrollbarRelPos.LeftOfIcons)]
        [Label("$Mods.BetterGameUI.Config.Label.ScrollbarRelPos")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ScrollbarRelPos")]
        public ScrollbarRelPos InventoryDownScrollbarRelPos { get; set; }

        [DefaultValue(BuffIconsHorOrder.LeftToRight)]
        [Label("$Mods.BetterGameUI.Config.Label.IconsHorOrder")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconsHorOrder")]
        public BuffIconsHorOrder InventoryDownIconsHorOrder { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.XOffset")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.XOffset")]
        public int InventoryDownXOffset { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.YOffset")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.YOffset")]
        public int InventoryDownYOffset { get; set; }

        [DefaultValue(2)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRowsCount")]
        public int InventoryDownIconRowsCount { get; set; }

        [DefaultValue(11)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconColsCount")]
        public int InventoryDownIconColsCount { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.HotbarLockingAlsoLocksThis")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.HotbarLockingAlsoLocksThis")]
        public bool InventoryDownHotbarLockingAlsoLocksThis { get; set; }

        // ------------- Inventory Up Buffs' Bar UI Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InventoryUpBuffsBarConfig")]
        [DefaultValue(ScrollbarRelPos.RightOfIcons)]
        [Label("$Mods.BetterGameUI.Config.Label.ScrollbarRelPos")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ScrollbarRelPos")]
        public ScrollbarRelPos InventoryUpScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.RightToLeft)]
        [Label("$Mods.BetterGameUI.Config.Label.IconsHorOrder")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconsHorOrder")]
        public BuffIconsHorOrder InventoryUpIconsHorOrder { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.XOffset")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.XOffset")]
        public int InventoryUpXOffset { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.YOffset")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.YOffset")]
        public int InventoryUpYOffset { get; set; }

        [DefaultValue(3)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRowsCount")]
        public int InventoryUpIconRowsCount { get; set; }

        [DefaultValue(6)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconColsCount")]
        public int InventoryUpIconColsCount { get; set; }

        // ------------- Misc. Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.MiscConfig")]
        [ReloadRequired]
        [DefaultValue(0)]
        [Range(uint.MinValue, uint.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ExtraPlayerBuffSlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ExtraPlayerBuffSlots")]
        public int ExtraPlayerBuffSlots { get; set; }
    }
}