using Terraria.UI;

namespace BetterGameUI
{
    public static class TmodLoaderExtensions
    {
        public static CalculatedStyle GrowFromCenter(this CalculatedStyle self, float Mod) {
            return self.GrowFromCenter(Mod, Mod);
        }

        public static CalculatedStyle GrowFromCenter(this CalculatedStyle self, float WidthMod, float HeightMod) {
            if (WidthMod != 0) {
                self.X -= (float)WidthMod / 2;
                self.Width += WidthMod;
            }

            if (HeightMod != 0) {
                self.Y -= (float)HeightMod / 2;
                self.Height += HeightMod;
            }

            return self;
        }

        public static bool Contains(this CalculatedStyle self, float x, float y)
        {
            if (self.X <= x && x < self.X + self.Width && self.Y <= y)
            {
                return y < self.Y + self.Height;
            }

            return false;
        }
    }
}