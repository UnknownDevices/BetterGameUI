using BetterGameUI.UI;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace BetterGameUI
{
    [Label("Client Config")]
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public override void OnLoaded() => BetterGameUI.Mod.ClientConfig = this;

        public override void OnChanged() => BetterGameUI.Mod.RaiseClientConfigChanged();

        // TODO: have tooltips display the min and max value for the field, as well as the reasoning for these if not obvious
        // TODO: have ReloadRequired fields mention they are so in their tooltips
        // TODO: do localization

        // ------------- Input Config ------------- //
        
        [Header("Input Config")]
        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Buffs' Bar's Hitbox Modifier")]
        public int BuffIconsBarHitboxModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller's Hitbox Modifier")]
        public int ScrollerHitboxModifier { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinimalScrollerHeight")]
        public int MinimalScrollerHeight { get; set; }

        // TODO: consider if this should also affect vanilla mouse scroll
        [DefaultValue(false)]
        [Label("Invert Mouse Scroll For Scrollbar")]
        public bool InvertMouseScrollForScrollbar { get; set; }

        [DefaultValue(true)]
        [Label("Allow Scroller Dragging")]
        public bool AllowScrollerDragging { get; set; }

        [DefaultValue(true)]
        [Label("Mouse Scroll Focuses Mouse Hovered UI")]
        public bool MouseInputFocusesMouseHoveredUI { get; set; }

        [DefaultValue(true)]
        [Label("Smart Hide Scrollbar")]
        public bool SmartHideScrollbar { get; set; }

        // ------------- Inventory Down Buffs' Bar Config ------------- //

        [Header("Inventory Down Buffs' Bar Config")]
        [DefaultValue(ScrollbarPosition.LeftOfIcons)]
        [Label("Scrollbar Relative Position")]
        public ScrollbarPosition InventoryDownScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.LeftToRight)]
        [Label("Icons Horizontal Order")]
        public BuffIconsHorOrder InventoryDownIconsHorOrder { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("X Offset")]
        public int InventoryDownXOffset { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Y Offset")]
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
        [Label("Hotbar Locking Also Locks This")]
        public bool InventoryDownHotbarLockingAlsoLocksThis { get; set; }

        // ------------- Inventory Up Buffs' Bar Config ------------- //

        [Header("Inventory Up Buffs' Bar Config")]
        [DefaultValue(ScrollbarPosition.RightOfIcons)]
        [Label("Scrollbar Relative Position")]
        public ScrollbarPosition InventoryUpScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.RightToLeft)]
        [Label("Icons Horizontal Order")]
        public BuffIconsHorOrder InventoryUpIconsHorOrder { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("X Offset")]
        public int InventoryUpXOffset { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Y Offset")]
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
        
        [Header("Misc Config")]
        [ReloadRequired]
        [DefaultValue(0)]
        [Range(uint.MinValue, uint.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ExtraPlayerBuffSlots")]
        public int ExtraPlayerBuffSlots { get; set; }
    }
}