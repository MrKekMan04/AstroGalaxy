using System;
using Microsoft.Xna.Framework;

namespace AstroGalaxy.Model.StateMachine.States;

public abstract class State
{
    protected readonly GraphicsDeviceManager Graphics;

    protected State(GraphicsDeviceManager graphics) => Graphics = graphics;

    public event Action ChangeState;
    
    public abstract void Initialize();

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(GameTime gameTime);

    public void OnChangeState() => ChangeState?.Invoke();
}