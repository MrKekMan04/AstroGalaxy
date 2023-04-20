using AstroGalaxy.Controller;
using AstroGalaxy.Model;
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

    public World World { get; private set; }

    private AstroGalaxy()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        World = new WorldBuilder()
            .AddSystem(new PlayerProcessing())
            .AddSystem(new SpikeUpdate())
            .AddSystem(new EntityRender(Graphics.GraphicsDevice))
            .Build();

        Components.Add(World);

        InitPlayer();

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        World.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        World.Draw(gameTime);

        base.Draw(gameTime);
    }

    private void InitPlayer()
    {
        var entity = World.CreateEntity();

        entity.Attach(new Player(
            new Transform2(Graphics.PreferredBackBufferWidth / 3 - Constants.PlayerSpriteFrameSize / 2, 0),
            new Sprite(Content.Load<Texture2D>(Constants.PlayerTexturePath)) { Origin = Vector2.Zero }));
    }

    private Vector2 CalculateWindowScale()
    {
        var windowRect = Window.ClientBounds;

        var scaleX = (float)windowRect.Width / Graphics.PreferredBackBufferWidth;
        var scaleY = (float)windowRect.Height / Graphics.PreferredBackBufferHeight;

        return new Vector2(scaleX, scaleY);
    }
}