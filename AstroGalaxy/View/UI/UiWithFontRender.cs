using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View.UI;

public abstract class UiWithFontRender : DrawSystem
{
    protected readonly SpriteBatch SpriteBatch;
    protected readonly SpriteFont SpriteFont;
    private readonly Dictionary<char, SpriteFont.Glyph> _charToGlyph;
    
    protected static Vector2 Scale => AstroGalaxy.Instance.WindowScale;

    protected UiWithFontRender(GraphicsDevice graphicsDevice)
    {
        SpriteBatch = new SpriteBatch(graphicsDevice);
        SpriteFont = AstroGalaxy.Instance.Content.Load<SpriteFont>(Constants.ArialFontPath);
        _charToGlyph = SpriteFont.GetGlyphs();
    }
    
    protected float GetStringWidth(string s) => s.Select(c => _charToGlyph[c]).Select(glyph => glyph.Width).Sum();
}