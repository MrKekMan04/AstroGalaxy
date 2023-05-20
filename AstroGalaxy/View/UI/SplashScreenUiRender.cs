using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroGalaxy.View.UI;

public class SplashScreenUiRender : UiWithFontRender
{
    public SplashScreenUiRender(GraphicsDevice graphicsDevice) : base(graphicsDevice)
    {
    }

    public override void Draw(GameTime gameTime)
    {
        var scale = AstroGalaxy.Instance.WindowScale;
        var loseTitleWidth = GetStringWidth(Constants.SplashScreenTitle);

        SpriteBatch.Begin();

        SpriteBatch.DrawString(SpriteFont, Constants.SplashScreenTitle,
            new Vector2((AstroGalaxy.Instance.Graphics.PreferredBackBufferWidth - loseTitleWidth) * scale.X / 2,
                AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight * scale.Y / 10), Color.Aqua, 0, Vector2.Zero,
            scale, SpriteEffects.None, 0);

        SpriteBatch.End();
    }
}