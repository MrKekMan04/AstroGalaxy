using AstroGalaxy.Controller;
using AstroGalaxy.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.StateMachine.States;

public class MainGame : State
{
    public Player Player { get; private set; }
    private Sprite _playerSprite;

    private bool _isPauseButtonPressed;
    private bool _isGamePaused;

    public MainGame(GraphicsDeviceManager graphics) : base(graphics)
    {
    }

    public override void Initialize()
    {
        _playerSprite = new Sprite(AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.PlayerTexturePath))
            { Origin = Vector2.Zero };

        World = new WorldBuilder()
            .AddSystem(new PlayerProcessing())
            .AddSystem(new SpikeUpdate(this))
            .AddSystem(new CheckBoundariesUpdate())
            .AddSystem(new EntityRender(Graphics.GraphicsDevice))
            .AddSystem(new GameUiRender(Graphics.GraphicsDevice, this))
            .Build();

        InitPlayer();

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        if (!_isGamePaused)  
            World.Update(gameTime);
        
        CheckForPause();
    }

    public override void Draw(GameTime gameTime) => World.Draw(gameTime);

    private void InitPlayer()
    {
        var entity = World.CreateEntity();

        entity.Attach(new Player(
            new Transform2(Graphics.PreferredBackBufferWidth / 3f - Constants.PlayerSpriteFrameSize / 2f, 0),
            _playerSprite));

        Player = entity.Get<Player>();
    }

    private void CheckForPause()
    {
        var isPauseButtonPressed = IsPauseButtonPressed();
        
        if (!isPauseButtonPressed && _isPauseButtonPressed)
            _isGamePaused = !_isGamePaused;
        
        _isPauseButtonPressed = isPauseButtonPressed;
    }

    private static bool IsPauseButtonPressed() => Keyboard.GetState().IsKeyDown(Keys.Escape);
}