using System.Collections.Generic;
using System.Reflection;

namespace BetterGameUI.Reflection
{
    public class PlayerInputReflection
    {
        public static readonly FieldInfo MouseInModdedUIInfo;
        public static List<string> GetMouseInModdedUI() => MouseInModdedUIInfo.GetValue(null) as List<string>;

        static PlayerInputReflection() {
            MouseInModdedUIInfo = typeof(Terraria.GameInput.PlayerInput).
                GetField("MouseInModdedUI", BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
