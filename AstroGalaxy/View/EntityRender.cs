using AstroGalaxy.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View;

public class EntityRender : EntityDrawSystem
{
    private readonly SpriteBatch _spriteBatch;

    private ComponentMapper<Player> _playerMapper;
    private ComponentMapper<Spike> _spikeMapper;

    public EntityRender(GraphicsDevice graphicsDevice) : base(Aspect.One(typeof(Player), typeof(Spike))) => 
        _spriteBatch = new SpriteBatch(graphicsDevice);

    public override void Initialize(IComponentMapperService mapperService)
    {
        _playerMapper = mapperService.GetMapper<Player>();
        _spikeMapper = mapperService.GetMapper<Spike>();
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        var scale = AstroGalaxy.Instance.WindowScale;

        foreach (var entityId in ActiveEntities)
        {
            var entity = GetGameObject(entityId);

            if (entity == null) continue;

            _spriteBatch.Draw(entity.Sprite.TextureRegion.Texture,
                new Vector2(entity.Transform.Position.X * scale.X, entity.Transform.Position.Y * scale.Y),
                entity.Frame,
                Color.White,
                entity.Transform.Rotation,
                entity.Sprite.Origin,
                scale,
                SpriteEffects.None,
                0);
        }

        _spriteBatch.End();
    }

    private GameObject GetGameObject(int entityId)
    {
        if (_playerMapper.Has(entityId))
            return _playerMapper.Get(entityId);

        return _spikeMapper.Has(entityId) ? _spikeMapper.Get(entityId) : null;
    }
}