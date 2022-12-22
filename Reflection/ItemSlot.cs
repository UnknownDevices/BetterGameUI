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
        private static readonly FieldInfo inventoryGlowHueInfo;
        public static float[] GetInventoryGlowHue() => inventoryGlowHueInfo.GetValue(null) as float[];

        private static readonly FieldInfo inventoryGlowTimeChestInfo;
        public static int[] GetInventoryGlowTimeChest() => inventoryGlowTimeChestInfo.GetValue(null) as int[];
        private static readonly FieldInfo inventoryGlowHueChestInfo;
        public static float[] GetInventoryGlowHueChest() => inventoryGlowHueChestInfo.GetValue(null) as float[];

        static ItemSlot()
        {
            GetGamepadPointForSlot = typeof(Terraria.UI.ItemSlot).
                GetMethod("GetGamepadPointForSlot", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Func<Item[], int, int, int>)) as Func<Item[], int, int, int>;
            inventoryGlowTime = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowTime", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowHueInfo = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowHue", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowTimeChestInfo = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowTimeChest", BindingFlags.NonPublic | BindingFlags.Static);
            inventoryGlowHueChestInfo = typeof(Terraria.UI.ItemSlot).
                GetField("inventoryGlowHueChest", BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
