using System;
using System.Reflection;
using Terraria;

namespace BetterGameUI.Reflection
{
    public class ItemSlot
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

        static ItemSlot()
        {
            GetGamepadPointForSlot = typeof(Terraria.UI.ItemSlot).
                GetMethod("GetGamepadPointForSlot", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Func<Item[], int, int, int>)) as Func<Item[], int, int, int>;
            inventoryGlowTime = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowTime", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowHue = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowHue", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowTimeChest = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowTimeChest", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowHueChest = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowHueChest", BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
