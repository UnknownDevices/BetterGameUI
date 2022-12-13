using BetterGameUI.UI;
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

        // TODO: consider config profiles
        // TODO: do invert mouse scroll config
        // TODO: have tooltips display the min and max value for the field, as well as the reasoning for these if not obvious
        // TODO: have ReloadRequired fields mention they are so in their tooltips
        // TODO: do localization

        // TODO: look into making custom config fields?
        // TODO: someway to reset individual fields to default
        
        // ------------- Input Config ------------- //
        
        [Header("Input Config")]
        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Buff Icons Bar Hitbox Modifier")]
        public int BuffIconsBarHitboxModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller Hitbox Modifier")]
        public int ScrollerHitboxModifier { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinimalScrollerHeight")]
        public int MinimalScrollerHeight { get; set; }

        [DefaultValue(true)]
        [Label("Allow Scroller Dragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(true)]
        [Label("Mouse Scroll Focuses Mouse Hovered UI")]
        public bool MouseInputFocusesMouseHoveredUI { get; set; }

        // ------------- Game's Buff Icons' Bar Config ------------- //

        // TODO: do position offsets
        // TODO: come up with a better naming scheme to distinguish Game's and Inventory's configs
        [Header("Game's Buff Icons' Bar Config")]
        [DefaultValue(ScrollbarPosition.LeftOfIcons)]
        [Label("Scrollbar Relative Position")]
        public ScrollbarPosition GameScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.LeftToRight)]
        [Label("Icons Horizontal Order")]
        public BuffIconsHorOrder GameIconsHorOrder { get; set; }

        [DefaultValue(2)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        public int GameIconRowsCount { get; set; }

        [DefaultValue(11)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        public int GameIconColsCount { get; set; }

        [DefaultValue(true)]
        [Label("Smart Hide Scrollbar")]
        public bool GameSmartHideScrollbar { get; set; }

        [DefaultValue(true)]
        [Label("Hotbar Locking Also Locks This")]
        public bool GameHotbarLockingAlsoLocksThis { get; set; }

        // ------------- Inventory's Buff Icons' Bar Config ------------- //

        [Header("Inventory's Buff Icons' Bar Config")]
        [DefaultValue(ScrollbarPosition.RightOfIcons)]
        [Label("Scrollbar Relative Position")]
        public ScrollbarPosition InventoryScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.RightToLeft)]
        [Label("Icons Horizontal Order")]
        public BuffIconsHorOrder InventoryIconsHorOrder { get; set; }

        [DefaultValue(4)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        public int InventoryIconRowsCount { get; set; }

        [DefaultValue(6)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        public int InventoryIconColsCount { get; set; }

        [DefaultValue(true)]
        [Label("Smart Hide Scrollbar")]
        public bool InventorySmartHideScrollbar { get; set; }

        // ------------- Misc Config ------------- //
        
        [Header("Misc Config")]
        [ReloadRequired]
        [DefaultValue(0)]
        [Range(uint.MinValue, uint.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ExtraPlayerBuffSlots")]
        public int ExtraPlayerBuffSlots { get; set; }
    }
}