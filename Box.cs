using System;

namespace SATCollision;

public class Box
{
    public Vector2 Pos;
    public int Height;
    public int Width;
    public Color Color;
    public float Rotation; 
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
    public Vector2[] Vertices {
        get {return new Vector2[] {GetVertix(0), GetVertix(1), GetVertix(2), GetVertix(3)};}
    } 

    public Box(Vector2 Position, int width, int height, Color color) 
    {
        Pos = Position;
        Width = width;
        Height = height;
        Color = color;
        Rotation = 0f;
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
        
        switch (Corner) {
            case 0:
                return Rotate(Rotation, TopL, Pos);
            case 1:
                return Rotate(Rotation, TopR, Pos);
            case 2:
                return Rotate(Rotation, BottR, Pos);
            default: 
                return Rotate(Rotation, BottL, Pos);
        }
    }

    public static Vector2 GetRotatedPoint(float angle, float distance)
    {
        float x = MathF.Cos(angle) * distance;
        float y = MathF.Sin(angle) * distance;
    
        return new Vector2(x, y);
    }

    public Vector2 GetEdge(uint Edge) // Used it for demonstration but it doesn't contribute anything to the functionality.
    {
        if (Edge > 3) Edge = 3;

        Vector2 TopL = GetVertix(0);
        Vector2 TopR = GetVertix(1);
        Vector2 BottR = GetVertix(2);
        Vector2 BottL = GetVertix(3);

        switch (Edge) {
            case 0:
                return GetVectorBetween(TopL, TopR);
            case 1:
                return GetVectorBetween(TopR, BottR);
            case 2:
                return GetVectorBetween(BottR, BottL);
            default: 
                return GetVectorBetween(BottL, TopL);
        }
    }

    public static Vector2 Rotate(float angle, Vector2 vec, Vector2 MidPoint)
    {
        float x = ((vec.X - MidPoint.X) * MathF.Cos(angle)) - ((vec.Y - MidPoint.Y) * MathF.Sin(angle)) + MidPoint.X;
        float y = ((vec.X - MidPoint.X) * MathF.Sin(angle)) + ((vec.Y - MidPoint.Y) * MathF.Cos(angle)) + MidPoint.Y;

        return new Vector2(x, y);
    }

    public static Vector2 GetVectorBetween(Vector2 vec1, Vector2 vec2)
    {
        return (vec2 - vec1) / 2 + vec1;
    }

    public static Vector2 ProjectVector(Vector2 vec, Vector2 proj)
    {
        float dot = Vector2.Dot(vec, proj);
        float projMag = proj.Length();
        Vector2 VU = (dot / (projMag * projMag)) * proj;

        return VU;
    }

    public static Vector2 ProjectOnAxis(Vector2[] vecs, Vector2 axis)
    {
        float min = float.PositiveInfinity;
        float max = float.NegativeInfinity;

        foreach (Vector2 vert in vecs) {
            float projection = Vector2.Dot(vert, axis);
            if (projection < min ) min = projection;
            if (projection > max) max = projection;
        }

        return new Vector2(min, max);
    }

    public static bool OverLapping(Vector2 vec1, Vector2 vec2)
    {
        return vec1.X <= vec2.Y && vec1.Y >= vec2.X;
    }

    public static bool Collide(Box box1, Box box2)
    {
        Box[] arr = new Box[] {box1, box2};

        for (int n = 0; n < 2; n++) { // Loop over shapes.
            for (uint i = 0; i < 4; i++) { // Loop over sides.
                uint j = (i + 1) % 4;
                
                // Get perpendicular vector to a side.
                float sideX = -(arr[n].GetVertix(j).Y - arr[n].GetVertix(i).Y);
                float sideY = arr[n].GetVertix(j).X - arr[n].GetVertix(i).X;

                Vector2 sideVec = new(sideX, sideY);

                sideVec.Normalize();
                
                // Project vertices on the axis.
                Vector2 shape1Projection = ProjectOnAxis(box1.Vertices, sideVec);
                Vector2 shape2Projection = ProjectOnAxis(box2.Vertices, sideVec);

                if (! OverLapping(shape1Projection, shape2Projection)) return false; // Check if the two projections overlap.

            }
        }
        
        return true;
    }
}
