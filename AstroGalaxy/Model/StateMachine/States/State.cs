using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;

namespace AstroGalaxy.Model.StateMachine.States;

public abstract class State
{
    protected readonly GraphicsDeviceManager Graphics;
    public World World { get; protected set; }

    protected State(GraphicsDeviceManager graphics) => Graphics = graphics;

    public event Action ChangeState;

    public virtual void Initialize()
    {
        AstroGalaxy.Instance.Components.Add(World);

        ChangeState += () => AstroGalaxy.Instance.Components.Remove(World);
    }

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(GameTime gameTime);

    public void OnChangeState() => ChangeState?.Invoke();
}