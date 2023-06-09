using BetterGameUI.Reflection;
using BetterGameUI.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public static class UISystem
    {
        public static HotbarBuffList HotbarBuffList;
        public static EquipPageBuffList EquipPageBuffList;
        public static AccessorySlotsScrollbar AccessorySlotsScrollbar;

        public static void Load() {
            HotbarBuffList = new HotbarBuffList();
            EquipPageBuffList = new EquipPageBuffList();
            AccessorySlotsScrollbar = new AccessorySlotsScrollbar();
        }

        public static void Unload() {
            HotbarBuffList = null;
            EquipPageBuffList = null;
            AccessorySlotsScrollbar = null;
        }

        public static void DrawAccSlots(AccessorySlotLoader self, int num20) {
            int skip = 0;
            AccessorySlotLoaderReflection.SetDrawVerticalAlignment(num20);
            Color color = Main.inventoryBack;

            for (int vanillaSlot = 3; vanillaSlot < Main.LocalPlayer.dye.Length; vanillaSlot++) {
                if (!self.Draw(skip, false, vanillaSlot, color)) {
                    skip++;
                }
            }

            for (int modSlot = 0; modSlot < AccessorySlotLoaderReflection.GetList(self).Count; modSlot++) {
                if (!self.Draw(skip, true, modSlot, color))
                    skip++;
            }

            //if (skip == AccessorySlotLoader.MaxVanillaSlotCount + AccessorySlotLoaderReflection.GetList(self).Count) {
            //    ModAccessorySlotPlayerReflection.SetScrollbarSlotPosition(
            //        AccessorySlotLoaderReflection.ModSlotPlayer(Main.LocalPlayer), 0);
            //    return;
            //}

            int accessoryPerColumn = AccessorySlotLoaderReflection.GetAccessorySlotPerColumn(self);
            int slotsToRender = AccessorySlotLoaderReflection.GetList(self).Count + AccessorySlotLoader.MaxVanillaSlotCount - skip;

            AccessorySlotLoaderReflection.SetDefenseIconPosition(new Vector2(Main.screenWidth - 64 - 28,
                AccessorySlotLoader.DrawVerticalAlignment + (accessoryPerColumn + 2) * 56 * Main.inventoryScale + 4));

            if (slotsToRender - accessoryPerColumn > 0) {
                AccessorySlotLoaderReflection.DrawScrollSwitch(self);
            }

            BetterGameUI.UISystem.AccessorySlotsScrollbar.Update(accessoryPerColumn, slotsToRender);

            ModAccessorySlotPlayerReflection.SetScrollbarSlotPosition(AccessorySlotLoaderReflection.ModSlotPlayer
                (Main.LocalPlayer), (int)BetterGameUI.UISystem.AccessorySlotsScrollbar.ScrolledPages);
        }
    }
}