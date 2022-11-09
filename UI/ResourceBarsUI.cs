using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
using static Terraria.Main;

namespace BetterGameUI.UI {
    public class ResourceBarsUI : UIState {
        // TODO: get rid of this reference
        public BuffIconsBarUI BuffIconsBarUI { get; set; }

        public ResourceBarsUI() {
            BuffIconsBarUI = BuffIconsBarUI.Default();
            BuffIconsBarUI.Recalculate();
            Append(BuffIconsBarUI);
        }

        // taken from Terraria's source
        public static void DrawBreath() {
            bool flag = false;
            if (player[myPlayer].dead)
                return;

            if (player[myPlayer].lavaTime < player[myPlayer].lavaMax && player[myPlayer].lavaWet)
                flag = true;
            else if (player[myPlayer].lavaTime < player[myPlayer].lavaMax && player[myPlayer].breath == player[myPlayer].breathMax)
                flag = true;

            Vector2 value = player[myPlayer].Top + new Vector2(0f, player[myPlayer].gfxOffY);
            if (playerInventory && screenHeight < 1000)
                value.Y += player[myPlayer].height - 20;

            value = Vector2.Transform(value - screenPosition, GameViewMatrix.ZoomMatrix);
            if (!playerInventory || screenHeight >= 1000)
                value.Y -= 100f;

            value /= UIScale;
            if (ingameOptionsWindow || InGameUI.IsVisible) {
                value = new Vector2(screenWidth / 2, screenHeight / 2 + 236);
                if (InGameUI.IsVisible)
                    value.Y = screenHeight - 64;
            }

            if (player[myPlayer].breath < player[myPlayer].breathMax && !player[myPlayer].ghost && !flag) {
                _ = player[myPlayer].breathMax / 20;
                int num = 20;
                for (int i = 1; i < player[myPlayer].breathMax / num + 1; i++) {
                    int num2 = 255;
                    float num3 = 1f;
                    if (player[myPlayer].breath >= i * num) {
                        num2 = 255;
                    }
                    else {
                        float num4 = (player[myPlayer].breath - (i - 1) * num) / (float)num;
                        num2 = (int)(30f + 225f * num4);
                        if (num2 < 30)
                            num2 = 30;

                        num3 = num4 / 4f + 0.75f;
                        if ((double)num3 < 0.75)
                            num3 = 0.75f;
                    }

                    int num5 = 0;
                    int num6 = 0;
                    if (i > 10) {
                        num5 -= 260;
                        num6 += 26;
                    }

                    spriteBatch.Draw(TextureAssets.Bubble.Value, value + new Vector2(26 * (i - 1) + num5 - 125f, 32f + (TextureAssets.Bubble.Height() - TextureAssets.Bubble.Height() * num3) / 2f + num6), new Rectangle(0, 0, TextureAssets.Bubble.Width(), TextureAssets.Bubble.Height()), new Color(num2, num2, num2, num2), 0f, default, num3, SpriteEffects.None, 0f);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            ResourceSetsManager.Draw();
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None,
               RasterizerState.CullCounterClockwise, null, UIScaleMatrix);

            DrawBreath();
            DrawInterface_Resources_ClearBuffs();
            if (!ingameOptionsWindow && !playerInventory && !inFancyUI) {
                BuffIconsBarUI.Draw(spriteBatch);
            }
        }
    }
}