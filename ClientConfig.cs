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
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.ShowStartupMessageForImportantChangeNotes")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ShowStartupMessageForImportantChangeNotes")]
        public bool ShowStartupMessageForImportantChangeNotes_0_3_7_0 { get; set; }

        // ------------- Compatibility Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.CompatibilityConfig")]
        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheHotbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheHotbar")]
        public bool DisableChangesToTheHotbar { get; set; }

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheItemSlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheItemSlots")]
        public bool DisableChangesToTheItemSlots { get; set; }

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableChangesToTheAccessorySlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableChangesToTheAccessorySlots")]
        public bool DisableChangesToTheAccessorySlots { get; set; }

        // ------------- General Buffs Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.GeneralBuffsBarConfig")]
        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.BuffsBarHitboxMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.BuffsBarHitboxMod")]
        public int BuffsBarHitboxMod { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.MouseScrollFocusesHoveredBuffsBar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MouseScrollFocusesHoveredBuffsBar")]
        public bool MouseScrollFocusesHoveredBuffsBar { get; set; }

        [DefaultValue(12)]
        [Range((int)6, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinScrollerHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinScrollerHeight")]
        public int MinScrollerHeight { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.SmartHideScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.SmartHideScrollbar")]
        public bool SmartHideScrollbar { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.AllowScrollerDragging")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.AllowScrollerDragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertMouseScrollForScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.InvertMouseScrollForScrollbar")]
        public bool InvertMouseScrollForScrollbar { get; set; }

        // ------------- Accessory Slots Config ------------- //
        
        [Header("$Mods.BetterGameUI.Config.Header.AccessorySlotsConfig")]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertMouseScrollForScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.InvertMouseScrollForScrollbar")]
        public bool AccessorySlots_InvertMouseScrollForScrollbar { get; set; }

        // TODO: rename to hotbar's...??
        // ------------- Off Inventory's Buffs Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.OffInventoryBuffsBarConfig")]
        [DefaultValue(40)]
        [Range(10, 100)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public int OffInventoryAlpha { get; set; }

        [DefaultValue(2)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRows")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRows")]
        public int OffInventoryIconRows { get; set; }

        [DefaultValue(11)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconCols")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconCols")]
        public int OffInventoryIconCols { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.HotbarLockingAlsoLocksThis")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.HotbarLockingAlsoLocksThis")]
        public bool OffInventoryHotbarLockingAlsoLocksThis { get; set; }

        // ------------- Inventory's Buffs Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InventoryBuffsBarConfig")]
        [DefaultValue(65)]
        [Range(10, 100)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public int InventoryAlpha { get; set; }

        [DefaultValue(3)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRows")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRows")]
        public int InventoryIconRows { get; set; }

        [DefaultValue(6)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconCols")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconCols")]
        public int InventoryIconCols { get; set; }
    }
}