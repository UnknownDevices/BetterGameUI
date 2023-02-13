using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BetterGameUI.Reflection
{
    public class MainReflection
    {
        public static readonly Action DrawInterface_Resources_Breath;

        static MainReflection() {
            DrawInterface_Resources_Breath = typeof(Terraria.Main).
                GetMethod("DrawInterface_Resources_Breath", BindingFlags.NonPublic | BindingFlags.Static).
                CreateDelegate(typeof(Action)) as Action;
        }
    }
}
