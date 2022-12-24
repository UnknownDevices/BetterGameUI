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
        public static readonly Action<Terraria.Player> HandleHotbar;
        static Player() {
            HandleHotbar = typeof(Terraria.Player).
                GetMethod("HandleHotbar", BindingFlags.NonPublic | BindingFlags.Instance).
                CreateDelegate(typeof(Action<Terraria.Player>)) as Action<Terraria.Player>;
        }
    }
}
