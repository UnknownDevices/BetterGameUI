using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace BetterGameUI.Reflection
{
    public class AccessorySlotLoaderReflection
    {
        public static readonly PropertyInfo DrawVerticalAlignmentInfo;
        public static int GetDrawVerticalAlignment()
            => (int)DrawVerticalAlignmentInfo.GetValue(null);
        public static void SetDrawVerticalAlignment(int val) 
            => DrawVerticalAlignmentInfo.SetValue(null, val);

        public static readonly PropertyInfo DefenseIconPositionInfo;
        public static void SetDefenseIconPosition(Vector2 val)
            => DefenseIconPositionInfo.SetValue(null, val);

        public static readonly FieldInfo ListInfo;
        public static List<ModAccessorySlot> GetList(AccessorySlotLoader self)
            => ListInfo.GetValue(self) as List<ModAccessorySlot>;

        public static readonly Func<Terraria.Player, ModAccessorySlotPlayer> ModSlotPlayer;
        public static readonly Func<AccessorySlotLoader, int> GetAccessorySlotPerColumn;
        public static readonly Action<AccessorySlotLoader> DrawScrollSwitch;

        static AccessorySlotLoaderReflection() {
            DrawVerticalAlignmentInfo = typeof(AccessorySlotLoader).GetProperty("DrawVerticalAlignment", 
                BindingFlags.Public | BindingFlags.Static);
            DefenseIconPositionInfo = typeof(AccessorySlotLoader).GetProperty("DefenseIconPosition", 
                BindingFlags.Public | BindingFlags.Static);
            ListInfo = typeof(AccessorySlotLoader).GetField("list", BindingFlags.NonPublic | 
                BindingFlags.Instance);
            ModSlotPlayer = typeof(AccessorySlotLoader).
                GetMethod("ModSlotPlayer", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Func<Terraria.Player, ModAccessorySlotPlayer>)) as Func<Terraria.Player, 
                ModAccessorySlotPlayer>;
            GetAccessorySlotPerColumn = typeof(AccessorySlotLoader).
                GetMethod("GetAccessorySlotPerColumn", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Func<AccessorySlotLoader, int>)) as Func<AccessorySlotLoader, int>;
            DrawScrollSwitch = typeof(AccessorySlotLoader).
                GetMethod("DrawScrollSwitch", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<AccessorySlotLoader>)) as Action<AccessorySlotLoader>;
        }
    }
}
