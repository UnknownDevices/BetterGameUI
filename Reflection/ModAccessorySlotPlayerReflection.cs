using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace BetterGameUI.Reflection
{
    public class ModAccessorySlotPlayerReflection
    {
        public static readonly FieldInfo ScrollbarSlotPositionInfo;
        public static int GetScrollbarSlotPosition(ModAccessorySlotPlayer self)
            => (int)ScrollbarSlotPositionInfo.GetValue(self);
        public static void SetScrollbarSlotPosition(ModAccessorySlotPlayer self, int val)
            => ScrollbarSlotPositionInfo.SetValue(self, val);

        public static readonly FieldInfo ScrollSlotsInfo;

        public static bool GetScrollSlots(ModAccessorySlotPlayer self)
            => (bool)ScrollSlotsInfo.GetValue(self);

        static ModAccessorySlotPlayerReflection() {
            ScrollbarSlotPositionInfo = typeof(ModAccessorySlotPlayer).GetField("scrollbarSlotPosition", 
                BindingFlags.NonPublic | BindingFlags.Instance);

            ScrollSlotsInfo = typeof(ModAccessorySlotPlayer).GetField("scrollSlots",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
