using System;
using System.ComponentModel;
using BetterGameUI.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetterGameUI
{
    [Label("$Mods.BetterGameUI.Config.Title.ClientConfig")]
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public override void OnLoaded() => BetterGameUI.Mod.ClientConfig = this;
        public override void OnChanged() => BetterGameUI.Mod.RaiseClientConfigChanged();

        // ------------- Notifications Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.NotificationsConfig")]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.ShowStartupMessageForImportantChangeNotes")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ShowStartupMessageForImportantChangeNotes")]
        public bool Notifications_ShowStartupMessageForImportantChangeNotes_0_3_9_0 { get; set; }

        // ------------- Compatibility Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.CompatibilityConfig")]
        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheBuffsBars")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheBuffsBars")]
        public bool Compatibility_DisableChangesToTheBuffsBars { get; set; }

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheHotbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheHotbar")]
        public bool Compatibility_DisableChangesToTheHotbar { get; set; }

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheItemSlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheItemSlots")]
        public bool Compatibility_DisableChangesToTheItemSlots { get; set; }

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheAccessorySlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheAccessorySlots")]
        public bool Compatibility_DisableChangesToTheAccessorySlots { get; set; }

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheMinimap")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheMinimap")]
        public bool Compatibility_DisableChangesToTheMinimap { get; set; }

        // ------------- Buffs Bars Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.BuffsBarsConfig")]
        [DefaultValue(12)]
        [Range((int)6, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinimalScrollersHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinimalScrollersHeight")]
        public int BuffsBars_MinimalScrollersHeight { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.HoverCursorToFocusWheelScroll")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.HoverCursorToFocusWheelScroll")]
        public bool BuffsBars_HoverCursorToFocusWheelScroll { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.HideScrollbarWhenNotNeeded")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.HideScrollbarWhenNotNeeded")]
        public bool BuffsBars_HideScrollbarWhenNotNeeded { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.AllowDraggingScroller")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.AllowDraggingScroller")]
        public bool BuffsBars_AllowDraggingScroller { get; set; }

        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertWheelScroll")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.InvertWheelScroll")]
        public bool BuffsBars_InvertWheelScroll { get; set; }

        // ------------- Accessory Slots Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.AccessorySlotsConfig")]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertWheelScroll")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.InvertWheelScroll")]
        public bool AccessorySlots_InvertWheelScroll { get; set; }

        // ------------- Minimap Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.MinimapConfig")]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.LockWhenHotbarIsLocked")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.LockWhenHotbarIsLocked")]
        public bool Minimap_LockWhenHotbarIsLocked { get; set; }

        // ------------- Recipes List Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.RecipesListConfig")]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertWheelScroll")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.InvertWheelScroll")]
        public bool RecipesList_InvertWheelScroll { get; set; }

        // ------------- Hotbar's Buffs Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.OffInventoryBuffsBarConfig")]
        [DefaultValue(40)]
        [Range(5, 100)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public int HotbarsBuffsBar_Alpha { get; set; }

        [DefaultValue(2)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.RowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.RowsCount")]
        public int HotbarsBuffsBar_RowsCount { get; set; }

        [DefaultValue(11)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ColumnsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ColumnsCount")]
        public int HotbarsBuffsBar_ColumnsCount { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.LockWhenHotbarIsLocked")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.LockWhenHotbarIsLocked")]
        public bool HotbarsBuffsBar_LockWhenHotbarIsLocked { get; set; }

        // ------------- Inventory's Buffs Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InventoryBuffsBarConfig")]
        [DefaultValue(65)]
        [Range(10, 100)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public int InventorysBuffsBar_Alpha { get; set; }

        [DefaultValue(3)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.RowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.RowsCount")]
        public int InventorysBuffsBar_RowsCount { get; set; }

        [DefaultValue(6)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ColumnsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ColumnsCount")]
        public int InventorysBuffsBar_ColumnsCount { get; set; }
    }
}