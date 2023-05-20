using System;
using AstroGalaxy.Controller;
using AstroGalaxy.Model.UI;
using AstroGalaxy.View;
using AstroGalaxy.View.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.StateMachine.States;

public class SplashScreen : State
{
    public SplashScreen(GraphicsDeviceManager graphics) : base(graphics)
    {
    }

    public override void Initialize()
    {
        World = new WorldBuilder()
            .AddSystem(new ButtonUpdate())
            .AddSystem(new SplashScreenUiRender(Graphics.GraphicsDevice))
            .AddSystem(new ButtonRender(Graphics.GraphicsDevice))
            .Build();

        InitButtons();

        base.Initialize();
    }

    public override void Update(GameTime gameTime) => World.Update(gameTime);

    public override void Draw(GameTime gameTime) => World.Draw(gameTime);

    private void InitButtons()
    {
        var oneWidthUnit = Graphics.PreferredBackBufferWidth * 15 / 100;
        var buttonHeight = Graphics.PreferredBackBufferHeight / 10;
        var buttonTexture = AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.ButtonTexturePath);

        var titles = new[] { Constants.SplashScreenPlayButtonText, Constants.SplashScreenExitButtonText };
        Action[] actions =
            { () => AstroGalaxy.Instance.StateMachine.SetState(GameState.Game), AstroGalaxy.Instance.Exit };

        for (var i = 0; i < 2; i++)
        {
            var buttonTopY = i == 0
                ? Graphics.PreferredBackBufferHeight * 3 / 10
                : Graphics.PreferredBackBufferHeight * 45 / 100;
            var button = World.CreateEntity();

            button.Attach(new Button(
                new Transform2(oneWidthUnit, buttonTopY), new Sprite(buttonTexture),
                new RectangleF(oneWidthUnit, buttonTopY, oneWidthUnit * 2, buttonHeight),
                titles[i]));

            button.Get<Button>().Click += actions[i];
        }
    }
}