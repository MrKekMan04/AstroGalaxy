using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View;

public class SplashScreenUiRender : DrawSystem
{
    private readonly SpriteBatch _spriteBatch;
    private readonly SpriteFont _spriteFont;
    private readonly Dictionary<char, SpriteFont.Glyph> _charToGlyph;

    public SplashScreenUiRender(GraphicsDevice graphicsDevice)
    {
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _spriteFont = AstroGalaxy.Instance.Content.Load<SpriteFont>(Constants.ArialFontPath);
        _charToGlyph = _spriteFont.GetGlyphs();
    }

    public override void Draw(GameTime gameTime)
    {
        var scale = AstroGalaxy.Instance.WindowScale;
        var loseTitleWidth = GetStringWidth(Constants.SplashScreenTitle);

        _spriteBatch.Begin();

        _spriteBatch.DrawString(_spriteFont, Constants.SplashScreenTitle,
            new Vector2((AstroGalaxy.Instance.Graphics.PreferredBackBufferWidth - loseTitleWidth) * scale.X / 2,
                AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight * scale.Y / 10), Color.Yellow, 0, Vector2.Zero,
            scale, SpriteEffects.None, 0);

        _spriteBatch.End();
    }

    private float GetStringWidth(string s) => s.Select(c => _charToGlyph[c]).Select(glyph => glyph.Width).Sum();
}