using System.Collections.Generic;
using AstroGalaxy.Model.StateMachine.States;
using Microsoft.Xna.Framework;

namespace AstroGalaxy.Model.StateMachine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    private readonly Dictionary<GameState, State> _enumToClassConverter;

    public StateMachine(GraphicsDeviceManager graphics)
    {
        _enumToClassConverter = new Dictionary<GameState, State>
        {
            [GameState.SplashScreen] = new SplashScreen(graphics),
            [GameState.Game] = new MainGame(graphics),
            [GameState.LoseScreen] = new LoseScreen(graphics)
        };
    }

    public void SetState(GameState state)
    {
        CurrentState?.OnChangeState();
        CurrentState = _enumToClassConverter[state];
        CurrentState.Initialize();
    }
}