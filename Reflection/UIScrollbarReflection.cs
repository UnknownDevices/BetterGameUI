using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BetterGameUI.Reflection
{
    public class UIScrollbarReflection
    {
        public static readonly Action<UIScrollbar, SpriteBatch, Texture2D, Rectangle, Color> DrawBar;

        static UIScrollbarReflection() {
            DrawBar = typeof(UIScrollbar).
                GetMethod("DrawBar", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<UIScrollbar, SpriteBatch, Texture2D, Rectangle, Color>)) as 
                Action<UIScrollbar, SpriteBatch, Texture2D, Rectangle, Color>;
        }
    }
}
