using Microsoft.Xna.Framework;

namespace AstroGalaxy;

public static class Constants
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

    public const string ArialFontPath = "Fonts/Arial";
    public const int ArialFontHeight = 20;
    
    public const string ButtonTexturePath = "UI Textures/Button";
    
    public const string LoseScreenMenuButtonText = "В меню";
    public const string LoseScreenRepeatButtonText = "Заново";
    public const string LoseScreenTitle = "ПОТРАЧЕНО!";
    public const string LoseScreenScoreReceivedText = "Очков набрано: {0:f1}";

    public const string SplashScreenPlayButtonText = "Играть";
    public const string SplashScreenExitButtonText = "Выход";
    public const string SplashScreenTitle = "Astro Galaxy";

    public const string MainGameScoreText = "Очки: {0:f1}";
}