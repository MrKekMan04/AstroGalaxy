using AstroGalaxy.Model.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.View;

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
            var player = _entityMapper.Get(entityId);
            var playerPosition = player.Transform.Position;
            var scale = AstroGalaxy.Instance.WindowScale;

            _spriteBatch.Draw(player.Sprite.TextureRegion.Texture,
                new Vector2(playerPosition.X * scale.X, playerPosition.Y * scale.Y),
                player.Frame, Color.White, 0f, Vector2.Zero, scale.X, SpriteEffects.None, 0);
        }

        _spriteBatch.End();
    }
}