using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using Terraria;

namespace BetterGameUI.Reflection
{
    public static class Main
    {
        public static readonly Action<Terraria.Main> HackForGamepadInputHell;
        public static readonly Action DrawPVPIcons;
        public static readonly Action<int, int> DrawBestiaryIcon;
        public static readonly Action<int, int> DrawEmoteBubblesButton;
        public static readonly Action<int, int> DrawTrashItemSlot;

        public delegate void GetBuilderAccsCountToShowDelegate(
            Terraria.Player plr, out int blockReplaceIcons, out int torchGodIcons, out int totalDrawnIcons);

        public static readonly GetBuilderAccsCountToShowDelegate GetBuilderAccsCountToShow;
        public static readonly Action<Terraria.Main, int, int, bool> DrawHotbarLockIcon;
        public static readonly FieldInfo achievementAdvisorInfo;
        public static readonly FieldInfo mapHeight;
        public static readonly FieldInfo cannotDrawAccessoriesHorizontally;
        public static readonly Func<int, int> DrawPageIcons;
        public static readonly Action<Terraria.Main> DrawNPCHousesInUI;
        public static readonly FieldInfo settingsButtonIsPushedToSide;
        public static readonly Action<int, int> DrawDefenseCounter;

        public delegate void DrawGuideCraftTextDelegate(int adjY, Color craftingTipColor, out int inventoryX, out int inventoryY);

        public static readonly DrawGuideCraftTextDelegate DrawGuideCraftText;
        public static readonly Action<int> HoverOverCraftingItemButton;
        public static readonly Action<int> SetRecipeMaterialDisplayName;
        public static readonly Action DrawInterface_Resources_Breath;

        static Main()
        {
            HackForGamepadInputHell = typeof(Terraria.Main).
                GetMethod("HackForGamepadInputHell", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Terraria.Main>)) as Action<Terraria.Main>;
            DrawPVPIcons = typeof(Terraria.Main).
                GetMethod("DrawPVPIcons", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action)) as Action;
            DrawBestiaryIcon = typeof(Terraria.Main).
                GetMethod("DrawBestiaryIcon", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            DrawEmoteBubblesButton = typeof(Terraria.Main).
                GetMethod("DrawEmoteBubblesButton", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            DrawTrashItemSlot = typeof(Terraria.Main).
                GetMethod("DrawTrashItemSlot", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            GetBuilderAccsCountToShow = typeof(Terraria.Main).
                GetMethod("GetBuilderAccsCountToShow", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(GetBuilderAccsCountToShowDelegate)) as GetBuilderAccsCountToShowDelegate;
            DrawHotbarLockIcon = typeof(Terraria.Main).
                GetMethod("DrawHotbarLockIcon", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Terraria.Main, int, int, bool>)) as Action<Terraria.Main, int, int, bool>;
            achievementAdvisorInfo = typeof(Terraria.Main).
                GetField("_achievementAdvisor", BindingFlags.NonPublic | BindingFlags.Instance);
            mapHeight = typeof(Terraria.Main).
                GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
            cannotDrawAccessoriesHorizontally = typeof(Terraria.Main).
                GetField("_cannotDrawAccessoriesHorizontally", BindingFlags.NonPublic | BindingFlags.Static);
            DrawPageIcons = typeof(Terraria.Main).
                GetMethod("DrawPageIcons", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Func<int, int>)) as Func<int, int>;
            DrawNPCHousesInUI = typeof(Terraria.Main).
                GetMethod("DrawNPCHousesInUI", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Terraria.Main>)) as Action<Terraria.Main>;
            settingsButtonIsPushedToSide = typeof(Terraria.Main).
                GetField("_settingsButtonIsPushedToSide", BindingFlags.NonPublic | BindingFlags.Static);
            DrawDefenseCounter = typeof(Terraria.Main).
                GetMethod("DrawDefenseCounter", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            DrawGuideCraftText = typeof(Terraria.Main).
                GetMethod("DrawGuideCraftText", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(DrawGuideCraftTextDelegate)) as DrawGuideCraftTextDelegate;
            HoverOverCraftingItemButton = typeof(Terraria.Main).
                GetMethod("HoverOverCraftingItemButton", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int>)) as Action<int>;
            SetRecipeMaterialDisplayName = typeof(Terraria.Main).
                GetMethod("SetRecipeMaterialDisplayName", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int>)) as Action<int>;
            DrawInterface_Resources_Breath = typeof(Terraria.Main).
                GetMethod("DrawInterface_Resources_Breath", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action)) as Action;
        }
    }
}