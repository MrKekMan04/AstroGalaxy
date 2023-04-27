using Microsoft.Xna.Framework;

namespace AstroGalaxy;

public abstract class Constants
{
    public const string PlayerTexturePath = "Entities Textures/asteroid_4x4";
    public const int PlayerDefaultHealth = 3;
    public const int PlayerSpriteFrameSize = 125;
    public const float PlayerSpriteFrameUpdateTime = 0.2f;
    public const float PlayerJumpTime = 0.5f;
    public static readonly Point PlayerSpriteSize = new(4, 4);

    public const string SpikeTexturePath = "Entities Textures/Spike";
    public const int SpikeSpriteSize = 125;
    
    public const string HeartTexturePath = "Entities Textures/heart";
    public const int HeartSpriteSize = 80;
}