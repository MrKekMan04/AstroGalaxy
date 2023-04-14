using AstroGalaxy.Controller;
using AstroGalaxy.Model.Entity;
using AstroGalaxy.View;
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

    public readonly GraphicsDeviceManager Graphics;
    
    private World _world;

    private AstroGalaxy()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        _world = new WorldBuilder()
            .AddSystem(new PlayerProcessing())
            .AddSystem(new PlayerUpdate())
            .AddSystem(new EntityRender(Graphics.GraphicsDevice))
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
        var spawnPointX = Graphics.PreferredBackBufferWidth / 3 - Constants.PlayerSpriteFrameSize / 2;
        const int spawnPointY = 0;

        entity.Attach(new Player(new Transform2(spawnPointX, spawnPointY),
            new Sprite(Content.Load<Texture2D>(Constants.PlayerTexturePath))));
    }

    private Vector2 CalculateWindowScale()
    {
        var windowRect = Window.ClientBounds;

        var scaleX = (float)windowRect.Width / Graphics.PreferredBackBufferWidth;
        var scaleY = (float)windowRect.Height / Graphics.PreferredBackBufferHeight;

        return new Vector2(scaleX, scaleY);
    }
}