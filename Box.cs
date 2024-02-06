using System;

namespace SATCollision;

public class Box
{
    public Vector2 Pos;
    public float xMin {
        get {return Pos.X - (Width / 2);}
    }
    public float xMax {
        get {return Pos.X + (Width / 2);}
    }
    public float yMin {
        get {return Pos.Y - (Height / 2);}
    }
    public float yMax {
        get {return Pos.Y + (Height / 2);}
    }
    public int Height;
    public int Width;
    public Color Color;
    public float Rotation = 0f; 

    public Box(Vector2 Position, int width, int height, Color color) 
    {
        Pos = Position;
        Width = width;
        Height = height;
        Color = color;
    }

    public static bool Intersected(Box box1, Box box2)
    {
        return true;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, Pos, new Rectangle {Width = Width, Height = Height}, Color,
        Rotation, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
    }

    public Vector2 GetVertix(uint Corner) 
    {
        if (Corner > 3) Corner = 3;

        switch (Corner) {
            case 0:
                return new Vector2(xMin, yMin);
            case 1:
                return new Vector2(xMax, yMin);
            case 2:
                return new Vector2(xMax, yMax);
            default: 
                return new Vector2(xMin, yMax);
        }
    }

    public void Update()
    {
        Pos = Rotate(Rotation, Pos);

    }

    public Vector2 GetEdge(uint Edge)
    {
        if (Edge > 3) Edge = 3;

        Vector2 TopL = GetVertix(0);
        Vector2 TopR = GetVertix(1);
        Vector2 BottR = GetVertix(2);
        Vector2 BottL = GetVertix(3);

        switch (Edge) {
            case 0:
                return (TopR - TopL) / 2 + TopL;
            case 1:
                return (BottR - TopR) / 2 + TopR;
            case 2:
                return (BottL - BottR) / 2 + BottR;
            default: 
                return (TopL - BottL) / 2 + BottL;
        }
    }

    public Vector2 Rotate(float angle, Vector2 vec)
    {
        Vector2 RotationMatrix = new(MathF.Cos(angle) - MathF.Sin(angle), MathF.Sin(angle) + MathF.Cos(angle));

        return vec * RotationMatrix;
    }

}
