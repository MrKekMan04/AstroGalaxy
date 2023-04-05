using AstroGalaxy.Controller;
using AstroGalaxy.Model.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy;

public class AstroGalaxy : Game
{
    public static AstroGalaxy Instance => s_instance ??= new AstroGalaxy();
    private static AstroGalaxy s_instance;

    public Vector2 WindowScale => CalculateWindowScale();

    private readonly GraphicsDeviceManager _graphics;
    
    private GameWindow GameWindow => Window;

    private World _world;

    private AstroGalaxy()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        _world = new WorldBuilder()
            .AddSystem(new PlayerProcessing())
            .AddSystem(new PlayerUpdate())
            .AddSystem(new EntityRender(_graphics.GraphicsDevice))
            .Build();

        Components.Add(_world);

        InitPlayer();

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _world.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _world.Draw(gameTime);

        base.Draw(gameTime);
    }

    private void InitPlayer()
    {
        var entity = _world.CreateEntity();

        var spawnPointX = GameWindow.ClientBounds.Center.X / 2;
        var spawnPointY = GameWindow.ClientBounds.Center.Y;

        entity.Attach(new Player(new Transform2(spawnPointX, spawnPointY, Constants.PlayerDefaultRotation),
            new Sprite(Content.Load<Texture2D>(Constants.PlayerTexturePath)),
            Constants.PlayerDefaultHealth, Constants.PlayerDefaultSpeed));
    }

    private Vector2 CalculateWindowScale()
    {
        var windowRect = GameWindow.ClientBounds;
        var windowCenter = windowRect.Center;

        var scaleX = (float)windowRect.Width / (windowCenter.X * 2);
        var scaleY = (float)windowRect.Height / (windowCenter.Y * 2);

        return new Vector2(scaleX, scaleY);
    }
}