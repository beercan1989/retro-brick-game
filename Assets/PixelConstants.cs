public static class PixelConstants
{
    /// <summary>
    /// Distance taken up by one pixel within the game.
    /// </summary>
    public const float PixelSize = 0.01f;
    
    /// <summary>
    /// Number of pixels taken up by each block.
    /// </summary>
    public const int PixelsPerBrick = 6;

    /// TODO - Work into level difficulty
    /// <summary>
    /// Number of pixels to progress the game by per second.
    /// </summary>
    public const int PixelsPerSecond = 50;
    
    /// <summary>
    /// The grid position for a brick.
    /// </summary>
    public const float PositionByBrick = PixelsPerBrick * PixelSize + PixelSize;

}