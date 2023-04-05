using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.Entity;

public class Enemy : Entity
{
    public Enemy(Transform2 transform, Sprite sprite, int health, int speed) : base(transform, sprite, health, speed)
    {
    }

    public Enemy(Transform2 transform, Sprite sprite, int health, int maxHealth, int speed) 
        : base(transform, sprite, health, maxHealth, speed)
    {
    }
}