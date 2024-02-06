using System;

namespace SATCollision;

public static class Globals // Class for global methods and variables.
{
    public static int Height;
    public static int Width;
    public static Texture2D pixel;

    public static void DrawLine(SpriteBatch spritebatch, Texture2D texture, Vector2 Vec1, Vector2 Vec2, Color color, float scale)
    {
        spritebatch.Draw(texture, Vec1, null, color,
                        MathF.Atan2(Vec2.Y - Vec1.Y, Vec2.X - Vec1.X),
                        new Vector2(0f, (float)texture.Height / 2),
                        new Vector2(Vector2.Distance(Vec1, Vec2), scale),
                        SpriteEffects.None, 0f);
    }
}

