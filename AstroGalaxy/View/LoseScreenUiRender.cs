using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View;

public class LoseScreenUiRender : DrawSystem
{
    private readonly SpriteBatch _spriteBatch;
    private readonly SpriteFont _spriteFont;
    private readonly Dictionary<char, SpriteFont.Glyph> _charToGlyph;
    private readonly float _score;

    public LoseScreenUiRender(GraphicsDevice graphicsDevice, float score)
    {
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _spriteFont = AstroGalaxy.Instance.Content.Load<SpriteFont>(Constants.ArialFontPath);
        _charToGlyph = _spriteFont.GetGlyphs();
        _score = score;
    }

    public override void Draw(GameTime gameTime)
    {
        var scale = AstroGalaxy.Instance.WindowScale;

        _spriteBatch.Begin();

        DrawTitle(scale);
        DrawReceivedScore(scale);

        _spriteBatch.End();
    }

    private void DrawTitle(Vector2 scale)
    {
        var loseTitleWidth = GetStringWidth(Constants.LoseScreenTitle);

        _spriteBatch.DrawString(_spriteFont, Constants.LoseScreenTitle, new Vector2(
                (AstroGalaxy.Instance.Graphics.PreferredBackBufferWidth - loseTitleWidth) / 2 * scale.X,
                AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight * scale.Y / 3), Color.Yellow,
            0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }

    private void DrawReceivedScore(Vector2 scale)
    {
        var scoreReceivedText = string.Format(Constants.LoseScreenScoreReceivedText, _score);
        var textWidth = GetStringWidth(scoreReceivedText);
        
        _spriteBatch.DrawString(_spriteFont, scoreReceivedText, new Vector2(
            (AstroGalaxy.Instance.Graphics.PreferredBackBufferWidth - textWidth) / 2 * scale.X,
            AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight * scale.Y / 2), Color.Aqua,
            0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }

    private float GetStringWidth(string s) => s.Select(c => _charToGlyph[c]).Select(glyph => glyph.Width).Sum();
}