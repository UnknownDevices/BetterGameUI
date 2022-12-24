using System.Collections.Generic;
using System.Reflection;

namespace BetterGameUI.Reflection
{
    public class PlayerInput
    {
        public static readonly FieldInfo MouseInModdedUIInfo;
        public static List<string> GetMouseInModdedUI() => MouseInModdedUIInfo.GetValue(null) as List<string>;

        static PlayerInput() {
            MouseInModdedUIInfo = typeof(Terraria.GameInput.PlayerInput).
                GetField("MouseInModdedUI", BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
