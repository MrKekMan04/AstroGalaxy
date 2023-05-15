using AstroGalaxy.Controller;
using AstroGalaxy.Model.UI;
using AstroGalaxy.View;
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
        var playButton = World.CreateEntity();
        var exitButton = World.CreateEntity();
        
        var oneWidthUnit = Graphics.PreferredBackBufferWidth * 15 / 100;
        
        var buttonXStart = oneWidthUnit;
        var buttonWidth = oneWidthUnit * 2;

        var playYStart = Graphics.PreferredBackBufferHeight * 3 / 10;
        var exitYStart = Graphics.PreferredBackBufferHeight * 45 / 100;
        var buttonHeight = Graphics.PreferredBackBufferHeight / 10;
        var buttonTexture = AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.ButtonTexturePath);

        playButton.Attach(new Button(
            new Transform2(buttonXStart, playYStart),
            new Sprite(buttonTexture), new RectangleF(buttonXStart, buttonXStart, buttonWidth, buttonHeight),
            Constants.SplashScreenPlayButtonText));

        playButton.Get<Button>().Click += () => AstroGalaxy.Instance.StateMachine.SetState(GameState.Game);
        
        exitButton.Attach(new Button(
            new Transform2(buttonXStart, exitYStart),
            new Sprite(buttonTexture), new RectangleF(buttonXStart, exitYStart, buttonWidth, buttonHeight),
            Constants.SplashScreenExitButtonText));

        exitButton.Get<Button>().Click += () => AstroGalaxy.Instance.Exit();
    }
}