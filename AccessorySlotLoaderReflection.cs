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

namespace BetterGameUI
{
    public class AccessorySlotLoaderReflection
    {
        public static readonly Func<AccessorySlotLoader, int, int, Texture2D> GetBackgroundTexture;
        public static readonly Action<AccessorySlotLoader, Texture2D, Vector2, Rectangle, Color, float, Vector2, float, SpriteEffects, float, int, int> DrawSlotTexture;
        
        static AccessorySlotLoaderReflection() {
            GetBackgroundTexture = typeof(AccessorySlotLoader).
                GetMethod("GetBackgroundTexture", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Func<AccessorySlotLoader, int, int, Texture2D>)) as Func<AccessorySlotLoader, int, int, Texture2D>;
            DrawSlotTexture = typeof(AccessorySlotLoader).
                GetMethod("DrawSlotTexture", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<AccessorySlotLoader, Texture2D, Vector2, Rectangle, Color, float, Vector2, float, SpriteEffects, float, int, int>)) as Action<AccessorySlotLoader, Texture2D, Vector2, Rectangle, Color, float, Vector2, float, SpriteEffects, float, int, int>;
        }
    }
}
