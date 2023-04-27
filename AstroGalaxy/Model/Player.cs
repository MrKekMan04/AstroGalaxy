using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model;

public class Player : GameObject
{
    public override Rectangle? Frame =>
        new(_currentFrame.X * Constants.PlayerSpriteFrameSize, _currentFrame.Y * Constants.PlayerSpriteFrameSize,
            Constants.PlayerSpriteFrameSize, Constants.PlayerSpriteFrameSize);

    public override RectangleF Boundaries => new(Transform.Position.X, Transform.Position.Y,
        Constants.PlayerSpriteFrameSize, Constants.PlayerSpriteFrameSize);

    public bool IsInJump { get; private set; }
    private bool _isUpSide = true;
    private float _jumpTimeElapsed;

    private Point _currentFrame = Point.Zero;
    private float _frameTimeElapsed;

    public int Health { get; private set; }
    private float _takingDamageCooldown;

    public Player(Transform2 transform, Sprite sprite) : base(transform, sprite) =>
        Health = Constants.PlayerDefaultHealth;

    public void Update(float deltaTime)
    {
        UpdatePosition(deltaTime);
        UpdateUndeadCooldown(deltaTime);
        UpdateFrame(deltaTime);
    }

    public void Jump() => IsInJump = true;

    public bool CanTakeDamage() => _takingDamageCooldown <= 0;

    public void TakeDamage()
    {
        Health--;
        _takingDamageCooldown = 3;
    }

    public bool IsDead() => Health <= 0;

    private void UpdatePosition(float deltaTime)
    {
        if (!IsInJump) return;

        _jumpTimeElapsed += deltaTime;

        var height = AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight;

        Transform.Position = new Vector2(Transform.Position.X, _isUpSide
            ? (height - Constants.PlayerSpriteFrameSize) * _jumpTimeElapsed / Constants.PlayerJumpTime
            : height - Math.Max(height * _jumpTimeElapsed / Constants.PlayerJumpTime, Constants.PlayerSpriteFrameSize));

        if (!(_jumpTimeElapsed >= Constants.PlayerJumpTime)) return;

        IsInJump = false;
        _jumpTimeElapsed = 0;
        _isUpSide = !_isUpSide;
    }

    private void UpdateUndeadCooldown(float deltaTime)
    {
        if (_takingDamageCooldown > 0) 
            _takingDamageCooldown -= deltaTime;
    }

    private void UpdateFrame(float deltaTime)
    {
        if (!((_frameTimeElapsed += deltaTime) >= Constants.PlayerSpriteFrameUpdateTime)) return;

        _frameTimeElapsed = 0;
        NextFrame();
    }

    private void NextFrame()
    {
        if (++_currentFrame.X < Constants.PlayerSpriteSize.X) return;

        _currentFrame.X = 0;

        if (++_currentFrame.Y >= Constants.PlayerSpriteSize.Y)
            _currentFrame.Y = 0;
    }
}