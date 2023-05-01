using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace BetterGameUI
{
    public class Assets
    {
        public static Asset<Texture2D> ScrollbarBar { get; set; }
        public static Asset<Texture2D> ScrollbarScroller { get; set; }

        public static void Load() {
            ScrollbarBar = ModContent.Request<Texture2D>("BetterGameUI/Textures/ScrollbarBar");
            ScrollbarScroller = ModContent.Request<Texture2D>("BetterGameUI/Textures/ScrollbarScroller");
        }

        public static void Unload() {
            ScrollbarBar = null;
            ScrollbarScroller = null;
        }
    }
}