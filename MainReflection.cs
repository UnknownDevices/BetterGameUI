using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;
using static Terraria.Main; 

namespace BetterGameUI
{
    public static class MainReflection {
        public static readonly Action<Main> HackForGamepadInputHell;
        public static readonly Action DrawPVPIcons;
        public static readonly Action<int, int> DrawBestiaryIcon;
        public static readonly Action<int, int> DrawEmoteBubblesButton;
        public static readonly Action<int, int> DrawTrashItemSlot;
        public delegate void GetBuilderAccsCountToShowDelegate(
            Terraria.Player plr, out int blockReplaceIcons, out int torchGodIcons, out int totalDrawnIcons);
        public static readonly GetBuilderAccsCountToShowDelegate GetBuilderAccsCountToShow;
        public static readonly Action<Main, int, int, bool> DrawHotbarLockIcon;
        public static readonly FieldInfo achievementAdvisorInfo;
        public static readonly FieldInfo mH;
        public static readonly FieldInfo cannotDrawAccessoriesHorizontally;
        public static readonly Func<int, int> DrawPageIcons;
        public static readonly Action<Main> DrawNPCHousesInUI;
        public static readonly FieldInfo settingsButtonIsPushedToSide;
        public static readonly Action<int, int> DrawDefenseCounter;
        public delegate void DrawGuideCraftTextDelegate(int adjY, Color craftingTipColor, out int inventoryX, out int inventoryY);
        public static readonly DrawGuideCraftTextDelegate DrawGuideCraftText;
        public static readonly Action<int> HoverOverCraftingItemButton;
        public static readonly Action<int> SetRecipeMaterialDisplayName;

        static MainReflection() {
            HackForGamepadInputHell = typeof(Main).
                GetMethod("HackForGamepadInputHell", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Main>)) as Action<Main>;
            DrawPVPIcons = typeof(Main).
                GetMethod("DrawPVPIcons", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action)) as Action;
            DrawBestiaryIcon = typeof(Main).
                GetMethod("DrawBestiaryIcon", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            DrawEmoteBubblesButton = typeof(Main).
                GetMethod("DrawEmoteBubblesButton", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            DrawTrashItemSlot = typeof(Main).
                GetMethod("DrawTrashItemSlot", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            GetBuilderAccsCountToShow = typeof(Main).
                GetMethod("GetBuilderAccsCountToShow", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(GetBuilderAccsCountToShowDelegate)) as GetBuilderAccsCountToShowDelegate;
            DrawHotbarLockIcon = typeof(Main).
                GetMethod("DrawHotbarLockIcon", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Main, int, int, bool>)) as Action<Main, int, int, bool>;
            achievementAdvisorInfo = typeof(Main).
                GetField("_achievementAdvisor", BindingFlags.NonPublic | BindingFlags.Instance);
            mH = typeof(Main).
                GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
            cannotDrawAccessoriesHorizontally = typeof(Main).
                GetField("_cannotDrawAccessoriesHorizontally", BindingFlags.NonPublic | BindingFlags.Static);
            DrawPageIcons = typeof(Main).
                GetMethod("DrawPageIcons", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Func<int, int>)) as Func<int, int>;
            DrawNPCHousesInUI = typeof(Main).
                GetMethod("DrawNPCHousesInUI", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Main>)) as Action<Main>;
            settingsButtonIsPushedToSide = typeof(Main).
                GetField("_settingsButtonIsPushedToSide", BindingFlags.NonPublic | BindingFlags.Static);
            DrawDefenseCounter = typeof(Main).
                GetMethod("DrawDefenseCounter", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int, int>)) as Action<int, int>;
            DrawGuideCraftText = typeof(Main).
                GetMethod("DrawGuideCraftText", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(DrawGuideCraftTextDelegate)) as DrawGuideCraftTextDelegate;
            HoverOverCraftingItemButton = typeof(Main).
                GetMethod("HoverOverCraftingItemButton", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int>)) as Action<int>;
            SetRecipeMaterialDisplayName = typeof(Main).
                GetMethod("SetRecipeMaterialDisplayName", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action<int>)) as Action<int>;
        }
    }
}
