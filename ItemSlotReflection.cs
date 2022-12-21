using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.UI;

namespace BetterGameUI
{
    // TODO: move reflection classes to Reflection folder
    public class ItemSlotReflection
    {
        public static readonly Func<Item[], int, int, int> GetGamepadPointForSlot;
        private static readonly FieldInfo inventoryGlowTime;
        public static int[] GetInventoryGlowTime() => inventoryGlowTime.GetValue(null) as int[];
        private static readonly FieldInfo inventoryGlowHue;
        public static float[] GetInventoryGlowHue() => inventoryGlowHue.GetValue(null) as float[];

        private static readonly FieldInfo inventoryGlowTimeChest;
        public static int[] GetInventoryGlowTimeChest() => inventoryGlowTimeChest.GetValue(null) as int[];
        private static readonly FieldInfo inventoryGlowHueChest;
        public static float[] GetInventoryGlowHueChest() => inventoryGlowHueChest.GetValue(null) as float[];

        static ItemSlotReflection() {
            GetGamepadPointForSlot = typeof(ItemSlot).
                GetMethod("GetGamepadPointForSlot", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Func<Item[], int, int, int>)) as Func<Item[], int, int, int>;
            inventoryGlowTime = typeof(ItemSlot).
                GetField("inventoryGlowTime", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowHue = typeof(ItemSlot).
                GetField("inventoryGlowHue", BindingFlags.NonPublic | BindingFlags.Static); 
            inventoryGlowTimeChest = typeof(ItemSlot).
                GetField("inventoryGlowTimeChest", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowHueChest = typeof(ItemSlot).
                GetField("inventoryGlowHueChest", BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
