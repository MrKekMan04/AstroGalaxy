using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace AstroGalaxy.Model.UI;

public class Button : GameObject
{
    public override Rectangle? Frame => null;
    public override RectangleF Boundaries { get; }
    
    public string Text { get; }

    public event Action Click;
    
    public Button(Transform2 transform, Sprite sprite, RectangleF boundaries, string text) : base(transform, sprite)
    {
        Boundaries = boundaries;
        Text = text;
    }

    public void OnClick() => Click?.Invoke();
}