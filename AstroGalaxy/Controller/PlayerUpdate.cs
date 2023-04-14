using AstroGalaxy.Model.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.Controller;

public class PlayerUpdate : EntityUpdateSystem
{
    private ComponentMapper<Player> _playerMapper;

    public PlayerUpdate() : base(Aspect.All(typeof(Player)))
    {
    }

    public override void Initialize(IComponentMapperService mapperService) =>
        _playerMapper = mapperService.GetMapper<Player>();

    public override void Update(GameTime gameTime)
    {
        foreach (var entityId in ActiveEntities)
        {
            var player = _playerMapper.Get(entityId);

            if (!player.IsInJump && Keyboard.GetState().IsKeyDown(Keys.Space))
                player.Jump();

            player.Update(gameTime.GetElapsedSeconds());
        }
    }
}