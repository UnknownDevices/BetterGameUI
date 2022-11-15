using Terraria.UI;

namespace BetterGameUI
{
    public static class TmodLoaderExtensions
    {
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