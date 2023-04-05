using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.Entity;

public abstract class Entity
{
    public readonly Transform2 Transform;
    public readonly Sprite Sprite;

    public Vector2 Velocity { get; protected set; }
    public int Speed { get; private set; }
    private readonly int _maxSpeed;


    private readonly int _maxHealth;
    private int _health;

    private float _gunCooldown;
    private float _slowdownCooldown;

    protected Entity(Transform2 transform, Sprite sprite, int health, int speed)
        : this(transform, sprite, health, health, speed)
    {
    }

    protected Entity(Transform2 transform, Sprite sprite, int health, int maxHealth, int speed)
    {
        Transform = transform;
        Sprite = sprite;
        _health = health;
        _maxHealth = maxHealth;
        Speed = speed;
        _maxSpeed = speed;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (_gunCooldown > 0) _gunCooldown -= deltaTime;
        if (_slowdownCooldown > 0) _slowdownCooldown -= deltaTime;

        if (_slowdownCooldown <= 0)
            Speed = _maxSpeed;
    }

    protected void TakeDamage(int damageAmount)
    {
        if (damageAmount <= 0) return;

        _health -= damageAmount;
    }

    protected void Slowdown(double delta, float delay)
    {
        Speed = (int)(_maxSpeed * delta);
        _slowdownCooldown = Math.Max(_slowdownCooldown, delay);
    }

    protected void Heal(int healAmount)
    {
        _health += healAmount;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public bool IsDead() => _health <= 0;
}