using System.Linq;
using AstroGalaxy.Model;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.Controller;

public class CheckBoundariesUpdate : EntityUpdateSystem
{
    private ComponentMapper<Player> _playerMapper;
    private ComponentMapper<Spike> _spikeMapper;

    public CheckBoundariesUpdate() : base(Aspect.One(typeof(Player), typeof(Spike)))
    {
    }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _playerMapper = mapperService.GetMapper<Player>();
        _spikeMapper = mapperService.GetMapper<Spike>();
    }

    public override void Update(GameTime gameTime)
    {
        var player = FindPlayer();

        foreach (var entityId in ActiveEntities.Where(entityId => !_playerMapper.Has(entityId)))
        {
            var enemy = GetEnemy(entityId);

            if (!player.Boundaries.Intersects(enemy.Boundaries)) continue;

            if (player.CanTakeDamage()) 
                player.TakeDamage();
            
            DestroyEntity(entityId);
        }
    }

    private Player FindPlayer() => ActiveEntities.Where(_playerMapper.Has).Select(_playerMapper.Get).FirstOrDefault();

    private GameObject GetEnemy(int entityId)
    {
        if (_spikeMapper.Has(entityId))
            return _spikeMapper.Get(entityId);
        // TODO: one more enemy type
        return null;
    }
}