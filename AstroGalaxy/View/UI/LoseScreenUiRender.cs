using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroGalaxy.View.UI;

public class LoseScreenUiRender : UiWithFontRender
{
    private readonly float _score;

    public LoseScreenUiRender(GraphicsDevice graphicsDevice, float score) : base(graphicsDevice) => _score = score;

    public override void Draw(GameTime gameTime)
    {
        var scale = Scale;
        
        SpriteBatch.Begin();

        DrawTitle(scale);
        DrawReceivedScore(scale);

        SpriteBatch.End();
    }

    private void DrawTitle(Vector2 scale)
    {
        var loseTitleWidth = GetStringWidth(Constants.LoseScreenTitle);

        SpriteBatch.DrawString(SpriteFont, Constants.LoseScreenTitle, new Vector2(
                (AstroGalaxy.Instance.Graphics.PreferredBackBufferWidth - loseTitleWidth) / 2 * scale.X,
                AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight * scale.Y / 3), Color.Aqua,
            0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }

    private void DrawReceivedScore(Vector2 scale)
    {
        var scoreReceivedText = string.Format(Constants.LoseScreenScoreReceivedText, _score);
        var textWidth = GetStringWidth(scoreReceivedText);
        
        SpriteBatch.DrawString(SpriteFont, scoreReceivedText, new Vector2(
            (AstroGalaxy.Instance.Graphics.PreferredBackBufferWidth - textWidth) / 2 * scale.X,
            AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight * scale.Y / 2), Color.Aqua,
            0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}