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

        Vector2 TopL = new Vector2(xMin, yMin);
        Vector2 TopR = new Vector2(xMax, yMin);
        Vector2 BottR = new Vector2(xMax, yMax);
        Vector2 BottL = new Vector2(xMin, yMax);
        float distance1 = Vector2.Distance(Pos, TopL);
        float distance2 = Vector2.Distance(Pos, TopR);
        float distance3 = Vector2.Distance(Pos, BottR);
        float distance4 = Vector2.Distance(Pos, BottL);

        // switch (Corner) {
        //     case 0:
        //         return GetRotatedPoint(Rotation, distance1) + Pos;
        //     case 1:
        //         return GetRotatedPoint(Rotation + 90, distance2) + Pos;
        //     case 2:
        //         return GetRotatedPoint(Rotation + 180, distance3) + Pos;
        //     default: 
        //         return GetRotatedPoint(Rotation + 270, distance4) + Pos;
        // }

        switch (Corner) {
            case 0:
                return Rotate(Rotation, TopL);
            case 1:
                return Rotate(Rotation, TopR);
            case 2:
                return Rotate(Rotation, BottR);
            default: 
                return Rotate(Rotation, BottL);
        }
    }

    public void Update()
    {
        Pos = Rotate(Rotation, Pos);

    }

    public Vector2 GetRotatedPoint(float angle, float distance)
    {
        float x = MathF.Cos(angle) * distance;
        float y = MathF.Sin(angle) * distance;
    
        return new Vector2(x, y);
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
        float x = ((vec.X - Pos.X) * MathF.Cos(angle)) - ((vec.Y - Pos.Y) * MathF.Sin(angle)) + Pos.X;
        float y = ((vec.X - Pos.X) * MathF.Sin(angle)) + ((vec.Y - Pos.Y) * MathF.Cos(angle)) + Pos.Y;

        return new Vector2(x, y);
    }

}
