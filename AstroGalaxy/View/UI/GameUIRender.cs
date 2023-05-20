using AstroGalaxy.Model.StateMachine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AstroGalaxy.View.UI;

public class GameUiRender : UiWithFontRender
{
    private readonly Texture2D _heartTexture;
    private readonly MainGame _game;

    public GameUiRender(GraphicsDevice graphicsDevice, MainGame game) : base(graphicsDevice)
    {
        _heartTexture = AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.HeartTexturePath);
        _game = game;
    }

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch.Begin();

        var scale = AstroGalaxy.Instance.WindowScale;

        DrawHearts(scale);
        DrawScore(scale);

        SpriteBatch.End();
    }

    private void DrawHearts(Vector2 scale)
    {
        var startY = AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight / 2 -
                     _game.Player.Health * Constants.HeartSpriteSize / 2;

        for (var i = 0; i < _game.Player.Health; i++)
            SpriteBatch.Draw(_heartTexture,
                new Vector2(0, startY * scale.Y + i * Constants.HeartSpriteSize * scale.Y),
                null,
                Color.White,
                0,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0);
    }

    private void DrawScore(Vector2 scale) =>
        SpriteBatch.DrawString(SpriteFont,
            string.Format(Constants.MainGameScoreText, _game.Score),
            Vector2.Zero,
            Color.Aqua,
            0f,
            Vector2.Zero,
            scale,
            SpriteEffects.None,
            0);
}