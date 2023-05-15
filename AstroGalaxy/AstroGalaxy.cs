using AstroGalaxy.Model.StateMachine;
using AstroGalaxy.Model.StateMachine.States;
using Microsoft.Xna.Framework;

namespace AstroGalaxy;

public class AstroGalaxy : Game
{
    public static AstroGalaxy Instance => s_instance ??= new AstroGalaxy();
    private static AstroGalaxy s_instance;

    public Vector2 WindowScale => CalculateWindowScale();

    public readonly GraphicsDeviceManager Graphics;

    public StateMachine StateMachine { get; private set; }

    private AstroGalaxy()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        StateMachine = new StateMachine(Graphics);
        StateMachine.SetState(GameState.SplashScreen);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        StateMachine.CurrentState.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Graphics.GraphicsDevice.Clear(Color.Black);
        
        StateMachine.CurrentState.Draw(gameTime);
        
        base.Draw(gameTime);
    }

    private Vector2 CalculateWindowScale()
    {
        var windowRect = Window.ClientBounds;

        var scaleX = (float)windowRect.Width / Graphics.PreferredBackBufferWidth;
        var scaleY = (float)windowRect.Height / Graphics.PreferredBackBufferHeight;

        return new Vector2(scaleX, scaleY);
    }
}