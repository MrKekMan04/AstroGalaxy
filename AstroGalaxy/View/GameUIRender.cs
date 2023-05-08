using AstroGalaxy.Model.StateMachine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View;

public class GameUiRender : DrawSystem
{
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _heartTexture;
    private readonly MainGame _game;

    public GameUiRender(GraphicsDevice graphicsDevice, MainGame game)
    {
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _heartTexture = AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.HeartTexturePath);
        _game = game;
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();

        var scale = AstroGalaxy.Instance.WindowScale;

        var startY = AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight / 2 -
                     _game.Player.Health * Constants.HeartSpriteSize / 2;

        for (var i = 0; i < _game.Player.Health; i++)
            _spriteBatch.Draw(_heartTexture,
                new Vector2(0, startY * scale.Y + i * Constants.HeartSpriteSize * scale.Y),
                null,
                Color.White,
                0,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0);

        _spriteBatch.End();
    }
}