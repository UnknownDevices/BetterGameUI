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
        // TODO: config to set bottom or right of an icon bar alternatively
        // TODO: config to invert icons vertical order
        // TODO: draw entire equip pages relative to the bottom right of the screen?
        // TODO: Invert mouse scroll input config
        // TODO: have tooltips display the min and max value for the field, as well as the reasoning for these if not obvious
        // TODO: have ReloadRequired fields mention they are so in their tooltips
        // TODO: do localization

        // TODO: look into making custom config fields?
        // TODO: someway to reset individual fields to default
        
        // ------------- Input Config ------------- //

        [Header("Input Config")]
        [DefaultValue(KeybindMode.Hold)]
        [Label("Allow mouse scroll keybind mode")]
        public KeybindMode AllowMouseScrollKeybindMode { get; set; }

        // TODO: use single value for both width and height
        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Buff icons bar hitbox width modifier")]
        public int BuffIconsBarHitboxWidthModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Buff icons bar hitbox height modifier")]
        public int BuffIconsBarHitboxHeightModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller hitbox width modifier")]
        public int ScrollerHitboxWidthModifier { get; set; }

        [DefaultValue(0)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("Scroller hitbox height modifier")]
        public int ScrollerHitboxHeightModifier { get; set; }

        [DefaultValue(true)]
        [Label("Allow scroller dragging")]
        public bool AllowScrollerDragging { get; set; }

        // TODO: have this specify it only affects new UIs
        [DefaultValue(true)]
        [Label("Allow mouse scroll input")]
        public bool AllowMouseScrollInput { get; set; }

        // NOTE: this could be assumed to be true if MouseHoveredUIReceivesMouseScrollInput is true and be assumed to be false otherwise
        [DefaultValue(true)]
        [Label("Mouse scroll input is exclusive")]
        [Tooltip("This makes it so only one UI at a time can receive mouse scroll input")]
        public bool MouseScrollInputIsExclusive { get; set; }
        
        [DefaultValue(true)]
        [Label("Mouse hovered UI receives mouse scroll input")]
        [Tooltip("If no UI which listens for mouse scroll input is mouse hovered, the input will then be received by\neither the hotbar if the menu is not up or by the crafting recipes bar if it is")]
        public bool MouseHoveredUIReceivesMouseScrollInput { get; set; }

        // ------------- Game's Buff Icons Bar Config ------------- //

        [Header("Game's Buff Icons Bar Config")]
        [DefaultValue(ScrollbarPosition.LeftOfIcons)]
        [Label("Scrollbar relative position")]
        public ScrollbarPosition GameScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.LeftToRight)]
        [Label("Icons horizontal order")]
        public BuffIconsHorOrder GameIconsHorOrder { get; set; }

        [DefaultValue(2)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRowsCount")]
        public int GameBarIconRowsCount { get; set; }

        [DefaultValue(11)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconColsCount")]
        public int GameBarIconColsCount { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinScrollerHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinScrollerHeight")]
        public int GameBarMinScrollerHeight { get; set; }

        [DefaultValue(true)]
        [Label("Smart hide scrollbar")]
        public bool GameBarSmartHideScrollbar { get; set; }

        [DefaultValue(true)]
        [Label("Hotbar locking also locks this")]
        public bool GameHotbarLockingAlsoLocksThis { get; set; }

        // ------------- Inventory's Buff Icons Bar Config ------------- //

        [Header("Inventory's Buff Icons Bar Config")]
        [DefaultValue(ScrollbarPosition.RightOfIcons)]
        [Label("Scrollbar relative position")]
        public ScrollbarPosition InventoryScrollbarRelPosition { get; set; }

        [DefaultValue(BuffIconsHorOrder.RightToLeft)]
        [Label("Icons horizontal order")]
        public BuffIconsHorOrder InventoryIconsHorOrder { get; set; }

        [DefaultValue(4)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconRowsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconRowsCount")]
        public int InventoryBarIconRowsCount { get; set; }

        [DefaultValue(6)]
        [Range(ushort.MinValue, ushort.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.IconColsCount")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.IconColsCount")]
        public int InventoryBarIconColsCount { get; set; }

        [DefaultValue(12)]
        [Range(int.MinValue, int.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.MinScrollerHeight")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.MinScrollerHeight")]
        public int InventoryBarMinScrollerHeight { get; set; }

        [DefaultValue(true)]
        [Label("Smart hide scrollbar")]
        public bool InventoryBarSmartHideScrollbar { get; set; }

        // ------------- Misc Config ------------- //
        
        [Header("Misc Config")]
        [ReloadRequired]
        [DefaultValue(0)]
        [Range(uint.MinValue, uint.MaxValue)]
        [Label("$Mods.BetterGameUI.Config.Label.ExtraPlayerBuffSlots")]
        [Tooltip("$Mods.BetterGameUI.Config.Tooltip.ExtraPlayerBuffSlots")]
        public int ExtraPlayerBuffSlots { get; set; }
    }
}