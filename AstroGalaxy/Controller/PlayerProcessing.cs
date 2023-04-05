using AstroGalaxy.Model.Entity;
using Microsoft.Xna.Framework;
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

        //TODO: GameOver

        if (player.IsDead())
            DestroyEntity(entityId);
    }
}