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
    private readonly MainGame _game;

    public PlayerProcessing(MainGame game) : base(Aspect.All(typeof(Player))) => _game = game;

    public override void Initialize(IComponentMapperService mapperService) =>
        _playerMapper = mapperService.GetMapper<Player>();

    public override void Process(GameTime gameTime, int entityId)
    {
        var player = _playerMapper.Get(entityId);
        
        if (!player.IsInJump && Keyboard.GetState().IsKeyDown(Keys.Space))
            player.Jump();

        var elapsedSeconds = gameTime.GetElapsedSeconds();
        
        player.Update(elapsedSeconds);
        _game.AddScore(elapsedSeconds);

        if (player.IsDead())
            AstroGalaxy.Instance.StateMachine.SetState(GameState.LoseScreen);
    }
}