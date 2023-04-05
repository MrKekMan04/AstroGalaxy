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
        var elapsedSeconds = gameTime.GetElapsedSeconds();

        foreach (var entityId in ActiveEntities)
        {
            var player = _playerMapper.Get(entityId);

            player.Direct(CalculatePlayerVelocity());

            player.Transform.Position += player.Velocity * (player.Speed * elapsedSeconds);
            
            player.UpdateCooldown(elapsedSeconds);
            
            // TODO: Shooting
        }
    }

    private static Vector2 CalculatePlayerVelocity()
    {
        var movement = new Vector2();
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            movement += new Vector2(-1, 0);
        if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            movement += new Vector2(1, 0);
        if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            movement += new Vector2(0, -1);
        if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            movement += new Vector2(0, 1);

        return movement;
    }
}