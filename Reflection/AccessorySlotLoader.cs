using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterGameUI.Reflection
{
    public class AccessorySlotLoader
    {
        public static readonly Func<Terraria.ModLoader.AccessorySlotLoader, int, int, Texture2D> GetBackgroundTexture;
        public static readonly Action<Terraria.ModLoader.AccessorySlotLoader, Texture2D, Vector2, Rectangle, Color, float, Vector2, float, SpriteEffects, float, int, int> DrawSlotTexture;

        static AccessorySlotLoader()
        {
            GetBackgroundTexture = typeof(Terraria.ModLoader.AccessorySlotLoader).
                GetMethod("GetBackgroundTexture", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Func<Terraria.ModLoader.AccessorySlotLoader, int, int, Texture2D>)) as Func<Terraria.ModLoader.AccessorySlotLoader, int, int, Texture2D>;
            DrawSlotTexture = typeof(Terraria.ModLoader.AccessorySlotLoader).
                GetMethod("DrawSlotTexture", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Terraria.ModLoader.AccessorySlotLoader, Texture2D, Vector2, Rectangle, Color, float, Vector2, float, SpriteEffects, float, int, int>)) as Action<Terraria.ModLoader.AccessorySlotLoader, Texture2D, Vector2, Rectangle, Color, float, Vector2, float, SpriteEffects, float, int, int>;
        }
    }
}
