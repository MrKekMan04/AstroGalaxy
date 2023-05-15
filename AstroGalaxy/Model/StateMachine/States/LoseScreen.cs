using AstroGalaxy.Controller;
using AstroGalaxy.Model.UI;
using AstroGalaxy.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.StateMachine.States;

public class LoseScreen : State
{
    public LoseScreen(GraphicsDeviceManager graphics) : base(graphics)
    {
    }

    public override void Initialize()
    {
        World = new WorldBuilder()
            .AddSystem(new ButtonUpdate())
            .AddSystem(new LoseScreenUiRender(Graphics.GraphicsDevice))
            .AddSystem(new ButtonRender(Graphics.GraphicsDevice))
            .Build();

        InitButtons();
        
        base.Initialize();
    }

    public override void Update(GameTime gameTime) => World.Update(gameTime);

    public override void Draw(GameTime gameTime) => World.Draw(gameTime);

    private void InitButtons()
    {
        var toMenuButton = World.CreateEntity();
        var reRunButton = World.CreateEntity();

        var yStart = Graphics.PreferredBackBufferHeight * 3 / 5;
        var yEnd = Graphics.PreferredBackBufferHeight * 4 / 5;

        var oneWidthUnit = Graphics.PreferredBackBufferWidth * 15 / 100;
        var buttonTexture = AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.ButtonTexturePath);

        toMenuButton.Attach(new Button(
            new Transform2(oneWidthUnit, yStart),
            new Sprite(buttonTexture), new RectangleF(oneWidthUnit, yStart, oneWidthUnit * 2, yEnd - yStart),
            Constants.LoseScreenMenuButtonText));

        toMenuButton.Get<Button>().Click += () => AstroGalaxy.Instance.StateMachine.SetState(GameState.SplashScreen);

        reRunButton.Attach(new Button(
            new Transform2(Graphics.PreferredBackBufferWidth - oneWidthUnit * 3, yStart),
            new Sprite(buttonTexture),
            new RectangleF(Graphics.PreferredBackBufferWidth - oneWidthUnit * 3, yStart, oneWidthUnit * 2,
                yEnd - yStart), Constants.LoseScreenRepeatButtonText));

        reRunButton.Get<Button>().Click += () => AstroGalaxy.Instance.StateMachine.SetState(GameState.Game);
    }
}