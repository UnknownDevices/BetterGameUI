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
        [Label("ShowStartupMessageForImportantChangeNotes")]
        [Tooltip("New updates may re-enable this feature")]
        public bool ShowStartupMessageForImportantChangeNotes_0_3_6_0 { get; set; }

        [DefaultValue(true)]
        [Label("ShowErrorMessages")]
        public bool ShowErrorMessages { get; set; }

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

        // ------------- General Scrollbar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.GeneralScrollbarConfig")]
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

        // ------------- Off Inventory's Buffs Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.OffInventoryBuffsBarConfig")]
        [DefaultValue(40)]
        [Range(10, 100f)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public int InventoryDownAlpha { get; set; }

        [DefaultValue(2)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRows")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRows")]
        public int InventoryDownIconRows{ get; set; }

        [DefaultValue(11)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconCols")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconCols")]
        public int InventoryDownIconCols { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.HotbarLockingAlsoLocksThis")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.HotbarLockingAlsoLocksThis")]
        public bool InventoryDownHotbarLockingAlsoLocksThis { get; set; }

        // ------------- Inventory's Buffs Bar Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InventoryBuffsBarConfig")]
        [DefaultValue(65)]
        [Range(10, 100)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public int InventoryUpAlpha { get; set; }

        [DefaultValue(3)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRows")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRows")]
        public int InventoryUpIconRows { get; set; }

        [DefaultValue(6)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconCols")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconCols")]
        public int InventoryUpIconCols { get; set; }
    }
}