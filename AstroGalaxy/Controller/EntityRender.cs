using AstroGalaxy.Model.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Controller;

public class EntityRender : EntityDrawSystem
{
    private readonly GraphicsDevice _graphicsDevice;
    private readonly SpriteBatch _spriteBatch;

    private ComponentMapper<Player> _entityMapper;

    public EntityRender(GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(Player)))
    {
        _graphicsDevice = graphicsDevice;
        _spriteBatch = new SpriteBatch(graphicsDevice);
    }

    public override void Initialize(IComponentMapperService mapperService) =>
        _entityMapper = mapperService.GetMapper<Player>();

    public override void Draw(GameTime gameTime)
    {
        _graphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        foreach (var entityId in ActiveEntities)
        {
            var entity = _entityMapper.Get(entityId);
            var playerPosition = entity.Transform.Position;

            var scale = AstroGalaxy.Instance.WindowScale;

            _spriteBatch.Draw(entity.Sprite,
                new Vector2((int)(playerPosition.X * scale.X), (int)(playerPosition.Y * scale.Y)),
                entity.Transform.Rotation, new Vector2(scale.X, scale.Y));
        }

        _spriteBatch.End();
    }
}