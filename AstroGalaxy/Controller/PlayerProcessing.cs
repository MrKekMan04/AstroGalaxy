using AstroGalaxy.Model;
using AstroGalaxy.Model.StateMachine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.Controller;

public class PlayerProcessing : EntityProcessingSystem
{
    private ComponentMapper<Player> _playerMapper;

    public PlayerProcessing() : base(Aspect.All(typeof(Player)))
    {
    }
    
    public override void Initialize(IComponentMapperService mapperService) =>
        _playerMapper = mapperService.GetMapper<Player>();

    public override void Process(GameTime gameTime, int entityId)
    {
        var player = _playerMapper.Get(entityId);
        
        if (!player.IsInJump && Keyboard.GetState().IsKeyDown(Keys.Space))
            player.Jump();

        player.Update(gameTime.GetElapsedSeconds());

        if (player.IsDead())
            AstroGalaxy.Instance.StateMachine.SetState(GameState.LoseScreen);
    }
}