using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;

namespace BetterGameUI.UI
{
    public class UIBasic : UIState
    {
        public bool IsEnabled { get; private set; } = true;
        public float Alpha { get; set; } = 1f;

        public override void Update(GameTime gameTime) {
            // if disabled, disable all UIBasic children of self as well
            if (!IsEnabled) {
                foreach (UIElement element in Elements) {
                    if (element is UIBasic) {
                        (element as UIBasic).IsEnabled = false;
                    }
                }
            }

            base.Update(gameTime);
        }

        public void MaybeDisable(bool condition) {
            IsEnabled &= !condition;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            
            IsEnabled = true;
        }
    }
}
