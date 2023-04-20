using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model;

public class Spike : GameObject
{
    public override Rectangle? Frame => null;

    public Spike(Transform2 transform, Sprite sprite) : base(transform, sprite)
    {
    }

    public void Move(Vector2 translate) => Transform.Position -= translate;
}