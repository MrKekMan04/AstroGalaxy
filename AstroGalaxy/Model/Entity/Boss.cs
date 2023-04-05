using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.Entity;

public class Boss : Enemy
{
    public Boss(Transform2 transform, Sprite sprite, int health) : base(transform, sprite, health, 0)
    {
    }

    public Boss(Transform2 transform, Sprite sprite, int health, int maxHealth) 
        : base(transform, sprite, health, maxHealth, 0)
    {
    }
}