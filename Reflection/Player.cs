using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BetterGameUI.Reflection
{
    public class Player
    {
        public delegate void SmartSelect_GetToolStrategyDelegate(Terraria.Player player, int tX, int tY, out int toolStrategy, out bool wetTile);
        public static readonly SmartSelect_GetToolStrategyDelegate SmartSelect_GetToolStrategy;
        public delegate void SmartSelect_PickToolForStrategyDelegate(Terraria.Player player, int tX, int tY, int toolStrategy, bool wetTile);
        public static readonly SmartSelect_PickToolForStrategyDelegate SmartSelect_PickToolForStrategy;
        public static readonly FieldInfo LastSmartCursorToolStrategyInfo;
        public static int GetLastSmartCursorToolStrategy(Terraria.Player player) => (int)LastSmartCursorToolStrategyInfo.GetValue(player);
        public static void SetLastSmartCursorToolStrategy(Terraria.Player player, int value) => LastSmartCursorToolStrategyInfo.SetValue(player, (int?)value);

        static Player() {
            SmartSelect_GetToolStrategy = typeof(Terraria.Player).
                GetMethod("SmartSelect_GetToolStrategy",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).
                CreateDelegate(typeof(SmartSelect_GetToolStrategyDelegate)) as SmartSelect_GetToolStrategyDelegate;
            LastSmartCursorToolStrategyInfo = typeof(Terraria.UI.ItemSlot).
                GetField("_lastSmartCursorToolStrategy", BindingFlags.NonPublic | BindingFlags.Static);
            SmartSelect_PickToolForStrategy = typeof(Terraria.Player).
                GetMethod("SmartSelect_PickToolForStrategy",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).
                CreateDelegate(typeof(SmartSelect_PickToolForStrategyDelegate)) as SmartSelect_PickToolForStrategyDelegate;
        }
    }
}
