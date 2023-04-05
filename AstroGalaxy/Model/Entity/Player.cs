using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.Entity;

public class Player : Entity
{
    public Player(Transform2 transform, Sprite sprite, int health, int speed) : base(transform, sprite, health, speed)
    {
    }

    public Player(Transform2 transform, Sprite sprite, int health, int maxHealth, int speed) 
        : base(transform, sprite, health, maxHealth, speed)
    {
    }

    public void Direct(Vector2 velocity) => Velocity = velocity;
}