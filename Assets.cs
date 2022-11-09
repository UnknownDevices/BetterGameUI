using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.ModLoader;

namespace BetterGameUI {
    public class Assets {
        public static Asset<Texture2D> Scrollbar { get; set; }
        public static Asset<Texture2D> Scroller { get; set; }

        public static void Load() {
            Scrollbar = ModContent.Request<Texture2D>("BetterGameUI/Textures/Scrollbar");
            Scroller = ModContent.Request<Texture2D>("BetterGameUI/Textures/Scroller");
        }

        public static void Unload() {
            Scrollbar = null;
            Scroller = null;   
        }
    }
}
