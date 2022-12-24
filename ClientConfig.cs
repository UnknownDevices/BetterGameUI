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
        
        // ------------- Input Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.InputConfig")]
        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.BuffsBarHitboxMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.BuffsBarHitboxMod")]
        public int BuffsBarHitboxMod { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.AllowScrollerDragging")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.AllowScrollerDragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.AllowScrollerSnappingToCursor")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.AllowScrollerSnappingToCursor")]
        public bool AllowScrollerSnappingToCursor { get; set; }

        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.MouseScrollFocusesMouseHoveredUI")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MouseScrollFocusesMouseHoveredUI")]
        public bool MouseScrollFocusesMouseHoveredUI { get; set; }
        
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.InvertMouseScrollForScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.InvertMouseScrollForScrollbar")]
        public bool InvertMouseScrollForScrollbar { get; set; }

        // ------------- General UI Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.GeneralUIConfig")]
        [DefaultValue(12)]
        [Range((int)6, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinScrollerHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinScrollerHeight")]
        public int MinScrollerHeight { get; set; }

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

        [DefaultValue(0.4f)]
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public float InventoryDownAlpha { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.XPosMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.XPosMod")]
        public int InventoryDownXPosMod { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.YPosMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.YPosMod")]
        public int InventoryDownYPosMod { get; set; }

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

        [DefaultValue(0.65f)]
        [Range(0f, 1f)]
        [Increment(0.01f)]
        [Label("$Mods.BetterGameUI.Config.Label.Alpha")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Alpha")]
        public float InventoryUpAlpha { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.XPosMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.XPosMod")]
        public int InventoryUpXPosMod { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.YPosMod")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.YPosMod")]
        public int InventoryUpYPosMod { get; set; }

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

        // ------------- MiscConfig ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.MiscConfig")]
        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableThisModChangesToTheHotbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableThisModChangesToTheHotbar")]
        public bool DisableThisModChangesToTheHotbar { get; set; }

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.DisableThisModChangesToTheItemSlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.DisableThisModChangesToTheItemSlots")]
        public bool DisableThisModChangesToTheItemSlots { get; set; }

    }
}