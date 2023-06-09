using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace BetterGameUI
{
    [Label("$Mods.BetterGameUI.Config.Title")]
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => BetterGameUI.Mod.Config = this;

        public override void OnChanged() => BetterGameUI.Mod.RaiseConfigChanged();

        // ------------- Notifications ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.Notifications")]
        [DefaultValue(false)]
        [Label("$Mods.BetterGameUI.Config.Label.Notifications_ShowStartupMessageForImportantChangeNotes")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Notifications_ShowStartupMessageForImportantChangeNotes")]
        public bool Notifications_ShowStartupMessageForImportantChangeNotes_0_4_0 { get; set; }

        // ------------- General ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.General")]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.General_HoverToActivateHotbarsBuffListScroll")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.General_HoverToActivateHotbarsBuffListScroll")]
        public bool General_HoverToActivateHotbarsBuffListScroll { get; set; }

        [DefaultValue(2)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.General_HotbarsBuffListRows")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.General_HotbarsBuffListRows")]
        public int General_HotbarsBuffListRows { get; set; }

        [DefaultValue(4)]
        [Range((ushort)1, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.General_EquipPagesBuffListRows")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.General_EquipPagesBuffListRows")]
        public int General_EquipPagesBuffListRows { get; set; }

        // ------------- Features Config ------------- //

        [Header("$Mods.BetterGameUI.Config.Header.Features")]
        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_BuffListsScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_BuffListsScrollbar")]
        public bool Feature_BuffListsScrollbar { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_AccessorySlotsImprovedScrollbar")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_AccessorySlotsImprovedScrollbar")]
        public bool Feature_AccessorySlotsImprovedScrollbar { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_InteractiveUIsWhileUsingItem")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_InteractiveUIsWhileUsingItem")]
        public bool Feature_InteractiveUIsWhileUsingItem { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_XButtonsScrollRecipeList")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_XButtonsScrollRecipeList")]
        public bool Feature_XButtonsScrollRecipeList { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_InvertedMouseScrollForRecipeList")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_InvertedMouseScrollForRecipeList")]
        public bool Feature_InvertedMouseScrollForRecipeList { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_DraggingOverHotbarSlotDoesntSelectItem")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_DraggingOverHotbarSlotDoesntSelectItem")]
        public bool Feature_DraggingOverHotbarSlotDoesntSelectItem { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_DraggingOverBuffIconDoesntInterruptClick")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_DraggingOverBuffIconDoesntInterruptClick")]
        public bool Feature_DraggingOverBuffIconDoesntInterruptClick { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_LockingHotbarLocksAdditionalUIs")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_LockingHotbarLocksAdditionalUIs")]
        public bool Feature_LockingHotbarLocksAdditionalUIs { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_SelectedItemIsHighlightedInInventory")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_SelectedItemIsHighlightedInInventory")]
        public bool Feature_SelectedItemIsHighlightedInInventory { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        [Label("$Mods.BetterGameUI.Config.Label.Feature_FixForHotbarFlickerUponOpeningFancyUI")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.Feature_FixForHotbarFlickerUponOpeningFancyUI")]
        public bool Feature_FixForHotbarFlickerUponOpeningFancyUI { get; set; }
    }
}