using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.Entity;

public class Player
{
    public readonly Transform2 Transform;
    public readonly Sprite Sprite;

    public Rectangle Frame =>
        new(_currentFrame.X * Constants.PlayerSpriteFrameSize, _currentFrame.Y * Constants.PlayerSpriteFrameSize,
            Constants.PlayerSpriteFrameSize, Constants.PlayerSpriteFrameSize);

    public bool IsInJump { get; private set; }

    private const float JumpTime = 0.5f;

    private readonly Point _spriteSize = new(4, 4);

    private Point _currentFrame = Point.Zero;

    private int _health;

    private float _frameTimeElapsed;

    private float _jumpTimeElapsed;

    private bool _isUpSide = true;

    public Player(Transform2 transform, Sprite sprite)
    {
        Transform = transform;
        Sprite = sprite;
        _health = Constants.PlayerDefaultHealth;
    }

    public void Update(float deltaTime)
    {
        UpdatePosition(deltaTime);

        UpdateFrame(deltaTime);
    }

    public void Jump() => IsInJump = true;

    public void TakeDamage() => _health--;

    public bool IsDead() => _health <= 0;

    private void UpdatePosition(float deltaTime)
    {
        if (!IsInJump) return;

        _jumpTimeElapsed += deltaTime;

        const int playerHeight = Constants.PlayerSpriteFrameSize;
        var height = AstroGalaxy.Instance.Graphics.PreferredBackBufferHeight;

        if (_isUpSide)
            Transform.Position = new Vector2(Transform.Position.X,
                (height - playerHeight) * _jumpTimeElapsed / JumpTime);
        else
            Transform.Position = new Vector2(Transform.Position.X,
                height - Math.Max(height * _jumpTimeElapsed / JumpTime, playerHeight));

        if (!(_jumpTimeElapsed >= JumpTime)) return;

        IsInJump = false;
        _jumpTimeElapsed = 0;
        _isUpSide = !_isUpSide;
    }

    private void UpdateFrame(float deltaTime)
    {
        _frameTimeElapsed += deltaTime;

        if (!(_frameTimeElapsed >= Constants.PlayerSpriteFrameUpdateTime)) return;

        _frameTimeElapsed = 0;
        NextFrame();
    }

    private void NextFrame()
    {
        ++_currentFrame.X;

        if (_currentFrame.X < _spriteSize.X) return;

        _currentFrame.X = 0;
        ++_currentFrame.Y;

        if (_currentFrame.Y >= _spriteSize.Y)
            _currentFrame.Y = 0;
    }
}