using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View;

public class GameUiRender : DrawSystem
{
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _heartTexture;

    public GameUiRender(GraphicsDevice graphicsDevice)
    {
        _spriteBatch = new SpriteBatch(graphicsDevice);
        _heartTexture = AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.HeartTexturePath);
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        var game = AstroGalaxy.Instance;
        var startY = game.Graphics.PreferredBackBufferHeight / 2 - game.Player.Health * Constants.HeartSpriteSize / 2;

        for (var i = 0; i < game.Player.Health; i++)
            _spriteBatch.Draw(_heartTexture, new Vector2(0, startY + i * Constants.HeartSpriteSize), Color.White);
        
        _spriteBatch.End();
    }
}