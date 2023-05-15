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
        var buttonTopY = Graphics.PreferredBackBufferHeight * 3 / 5;
        var buttonHeight = Graphics.PreferredBackBufferHeight / 5;

        var oneWidthUnit = Graphics.PreferredBackBufferWidth * 15 / 100;
        var buttonTexture = AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.ButtonTexturePath);

        var states = new[] { GameState.SplashScreen, GameState.Game };
        var titles = new[] { Constants.LoseScreenMenuButtonText, Constants.LoseScreenRepeatButtonText };

        for (var i = 0; i < 2; i++)
        {
            var index = i;
            var buttonLeftX = index == 0 ? oneWidthUnit : Graphics.PreferredBackBufferWidth - oneWidthUnit * 3;
            var button = World.CreateEntity();

            button.Attach(new Button(
                new Transform2(buttonLeftX, buttonTopY), new Sprite(buttonTexture), 
                new RectangleF(buttonLeftX, buttonTopY, oneWidthUnit * 2, buttonHeight),
                titles[index]));

            button.Get<Button>().Click += () => AstroGalaxy.Instance.StateMachine.SetState(states[index]);
        }
    }
}