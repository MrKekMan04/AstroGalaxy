using AstroGalaxy.Model.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AstroGalaxy.Controller;

public class ButtonUpdate : EntityUpdateSystem
{
    private ComponentMapper<Button> _buttonMapper;

    public ButtonUpdate() : base(Aspect.One(typeof(Button)))
    {
    }

    public override void Initialize(IComponentMapperService mapperService) => 
        _buttonMapper = mapperService.GetMapper<Button>();

    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            var scale = AstroGalaxy.Instance.WindowScale;
            
            foreach (var buttonId in ActiveEntities)
            {
                var button = _buttonMapper.Get(buttonId);

                if (button.Boundaries.Contains(new Point2(mouseState.X / scale.X, mouseState.Y / scale.Y))) 
                    button.OnClick();
            }
        }
    }
}