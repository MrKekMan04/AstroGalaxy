using System.Collections.Generic;
using System.Linq;
using AstroGalaxy.Model.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View;

public class ButtonRender : EntityDrawSystem
{
    private readonly SpriteBatch _spriteBatch;

    private ComponentMapper<Button> _buttonMapper;
    private SpriteFont _spriteFont;
    private Dictionary<char, SpriteFont.Glyph> _charToGlyph;

    public ButtonRender(GraphicsDevice graphicsDevice) : base(Aspect.One(typeof(Button))) =>
        _spriteBatch = new SpriteBatch(graphicsDevice);

    public override void Initialize(IComponentMapperService mapperService)
    {
        _buttonMapper = mapperService.GetMapper<Button>();
        _spriteFont = AstroGalaxy.Instance.Content.Load<SpriteFont>(Constants.ArialFontPath);
        _charToGlyph = _spriteFont.GetGlyphs();
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        var scale = AstroGalaxy.Instance.WindowScale;

        foreach (var buttonId in ActiveEntities)
        {
            var button = _buttonMapper.Get(buttonId);

            _spriteBatch.Draw(button.Sprite.TextureRegion.Texture,
                new Rectangle((int)(button.Boundaries.X * scale.X), (int)(button.Boundaries.Y * scale.Y),
                    (int)(button.Boundaries.Width * scale.X), (int)(button.Boundaries.Height * scale.Y)),
                new Rectangle(0, 0, button.Sprite.TextureRegion.Texture.Width,
                    button.Sprite.TextureRegion.Texture.Height),
                Color.White);

            var textWidth = GetStringWidth(button.Text);

            _spriteBatch.DrawString(_spriteFont, button.Text,
                new Vector2((button.Boundaries.Left + (button.Boundaries.Width - textWidth) / 2) * scale.X,
                    (button.Boundaries.Top + (button.Boundaries.Height - Constants.ArialFontHeight) / 2) * scale.Y),
                Color.Aqua, button.Transform.Rotation, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        _spriteBatch.End();
    }

    private float GetStringWidth(string s) => s.Select(c => _charToGlyph[c]).Select(glyph => glyph.Width).Sum();
}