using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace BetterGameUI.UI {
    public class RegularBuffIconsBarUI : BuffIconsBarUI {
        public RegularBuffIconsBarUI() {
            ScrollbarReservedWidth = 16;
            IconRowsCount = (ushort)Mod.ClientConfig.IconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.IconColsCount;
            Top = StyleDimension.FromPixels(Mod.ClientConfig.Y);
            Left = StyleDimension.FromPixels(Mod.ClientConfig.X);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconRowsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconColsCount) - IconToIconPad);
            IconsHorOrder = Mod.ClientConfig.OrderIconsFromRightToLeft ?
                BuffIconsHorOrder.RightToLeft : BuffIconsHorOrder.LeftToRight;

            Append(new ScrollbarUI {
                Top = StyleDimension.FromPixels(2f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(10f),
                Height = StyleDimension.FromPixelsAndPercent(-16f, 1f),
                CornerHeight = 4,
                IsVisible = true,
                Alpha = 0.5f,
            });

            ScrollbarUI.Append(new ScrollerUI {
                Top = StyleDimension.FromPixels(0f),
                Left = StyleDimension.FromPixels(2f),
                Width = StyleDimension.FromPixels(6f),
                Height = StyleDimension.FromPixels(8f),
                MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinScrollerHeight),
                HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier,
                HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier,
                CornerHeight = 2,
                // TODO: this should be set on OnActivate or something like that
                IsVisible = true, 
                Alpha = 0.5f,
            });

            Recalculate();
            OnUpdate += HandleUpdate;
            Mod.OnClientConfigChanged += HandleClientConfigChanged;
        }

        // TODO: make static??
        public new void HandleUpdate(UIElement affectedElement) {
            // TODO: just use active/deactive
            ScrollbarUI.IsVisible = Mod.ClientConfig.AlwaysShowScrollbar | 0 < ScrollbarUI.MaxScrolls;

            ScrollbarUI.IsMouseScrollAllowed =
                !Mod.ClientConfig.NeverAllowMouseScroll &
                Player.IsMouseScrollAllowed &
                (!Mod.ClientConfig.HoverUIToAllowMouseScroll | IsMouseHovering);
            ScrollbarUI.IsDraggingScrollerAllowed = Mod.ClientConfig.AllowScrollerDragging &&
                (!Mod.ClientConfig.LockWhenHotbarIsLocked | !Main.player[Main.myPlayer].hbLocked);

            if (ScrollbarUI.IsMouseScrollAllowed & Mod.ClientConfig.SmartLockVanillaMouseScroll) {
                PlayerInput.LockVanillaMouseScroll("BuffIconsBarUI");
            }
        }

        public void HandleClientConfigChanged() {
            IconRowsCount = (ushort)Mod.ClientConfig.IconRowsCount;
            IconColsCount = (ushort)Mod.ClientConfig.IconColsCount;
            Top = StyleDimension.FromPixels(Mod.ClientConfig.Y);
            Left = StyleDimension.FromPixels(Mod.ClientConfig.X);
            Width = StyleDimension.FromPixels(((IconWidth + IconToIconPad) *
                IconColsCount) - IconToIconPad + ScrollbarReservedWidth);
            Height = StyleDimension.FromPixels(((IconHeight + IconTextHeight + IconToIconPad) *
                IconRowsCount) - IconToIconPad);
            IconsHorOrder = Mod.ClientConfig.OrderIconsFromRightToLeft ?
                BuffIconsHorOrder.RightToLeft : BuffIconsHorOrder.LeftToRight;

            ScrollbarUI.ScrollerUI.MinHeight = StyleDimension.FromPixels(Mod.ClientConfig.MinScrollerHeight);
            ScrollbarUI.ScrollerUI.HitboxWidthModifier = Mod.ClientConfig.ScrollerHitboxWidthModifier;
            ScrollbarUI.ScrollerUI.HitboxHeightModifier = Mod.ClientConfig.ScrollerHitboxHeightModifier;

            Recalculate();
        }
    }
}
