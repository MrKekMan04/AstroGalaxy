using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model;

public abstract class GameObject
{
    public readonly Transform2 Transform;
    public readonly Sprite Sprite;
    public abstract Rectangle? Frame { get; }

    protected GameObject(Transform2 transform, Sprite sprite)
    {
        Transform = transform;
        Sprite = sprite;
    }
}