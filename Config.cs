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

        [Header("Notifications")]
        [DefaultValue(false)]
        public bool Notifications_ShowStartupMessageForImportantChangeNotes_2_1_3 { get; set; }

        // ------------- General ------------- //

        [Header("General")]
        [DefaultValue(true)]
        public bool General_HoverToActivateHotbarsBuffListScroll { get; set; }

        [DefaultValue(2)]
        [Range((ushort)1, ushort.MaxValue)]
        public int General_HotbarsBuffsBarRows { get; set; }

        [DefaultValue(4)]
        [Range((ushort)1, ushort.MaxValue)]
        public int General_EquipPagesBuffsBarRows { get; set; }

        [DefaultValue(4)]
        [Range((ushort)1, ushort.MaxValue)]
        public int General_NPCHouseIconsColumns { get; set; }

        // ------------- Features Config ------------- //

        [Header("Features")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_BuffsBarScrollbar { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_NPCHouseIconsScrollbar { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_AccessorySlotsImprovedScrollbar { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_InteractiveUIsWhileUsingItem { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_MouseWheelButtonsScrollRecipeList { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_InvertedMouseScrollForRecipeList { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_DraggingOverHotbarSlotDoesntSelectItem { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_DraggingOverBuffIconDoesntInterruptClick { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_LockingHotbarLocksAdditionalUIs { get; set; }

        //[ReloadRequired]
        //[DefaultValue(true)]
        //public bool Feature_SelectedItemIsHighlightedInInventory { get; set; }

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Feature_FixForHotbarFlickerUponOpeningFancyUI { get; set; }
    }
}