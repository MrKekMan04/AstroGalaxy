using System;
using System.Linq;
using AstroGalaxy.Model;
using AstroGalaxy.Model.StateMachine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Controller;

public class SpikeUpdate : EntityUpdateSystem
{
    private readonly Vector2 _translation = new(0.1f, 0);
    private ComponentMapper<Spike> _spikeMapper;
    private float _deltaXToSpawn;
    private float _timeElapsed;

    private readonly MainGame _game;

    public SpikeUpdate(MainGame game) : base(Aspect.All(typeof(Spike))) => _game = game;

    public override void Initialize(IComponentMapperService mapperService) =>
        _spikeMapper = mapperService.GetMapper<Spike>();

    public override void Update(GameTime gameTime)
    {
        var distanceElapsed = _translation * Math.Max(Math.Min(_timeElapsed += gameTime.GetElapsedSeconds(), 80), 40);

        foreach (var entityId in ActiveEntities.Where(entityId => _spikeMapper.Has(entityId)))
        {
            var spike = _spikeMapper.Get(entityId);

            spike.Move(distanceElapsed);

            if (spike.Transform.Position.X <= -Constants.SpikeSpriteSize)
                DestroyEntity(entityId);
        }

        if ((_deltaXToSpawn -= distanceElapsed.X) <= 0)
            Spawn();
    }

    private void Spawn()
    {
        var random = new Random();

        _deltaXToSpawn = Constants.SpikeSpriteSize + Constants.PlayerSpriteFrameSize * _translation.X *
            Math.Max(Math.Min(_timeElapsed + Constants.PlayerJumpTime, 80), 40) * (Constants.PlayerJumpTime + 0.2f);

        var graphics = AstroGalaxy.Instance.Graphics;
        var isSpawnUp = random.Next(2) == 1;

        var entity = _game.World.CreateEntity();

        entity.Attach(new Spike(
            new Transform2(graphics.PreferredBackBufferWidth + (isSpawnUp ? Constants.SpikeSpriteSize : 0),
                isSpawnUp
                    ? Constants.SpikeSpriteSize / 2
                    : graphics.PreferredBackBufferHeight - Constants.SpikeSpriteSize / 2,
                isSpawnUp ? (float)Math.PI : 0),
            new Sprite(AstroGalaxy.Instance.Content.Load<Texture2D>(Constants.SpikeTexturePath))));
    }
}