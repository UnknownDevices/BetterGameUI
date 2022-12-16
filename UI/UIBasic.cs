using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIBasic : UIState
    {
        public bool IsEnabled { get; set; } = true;

        public override void Update(GameTime gameTime) {
            // Disable all UIBasic children of this if disabled
            foreach (UIElement element in Elements) {
                if (element is UIBasic) {
                    (element as UIBasic).IsEnabled &= IsEnabled;
                }
            }

            // Update is still called, even if disabled
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch) {
            // If disabled, do not call call draw on this nor on any children
            if (IsEnabled) {
                base.Draw(spriteBatch);
            }

            IsEnabled = true;
        }
    }
}
