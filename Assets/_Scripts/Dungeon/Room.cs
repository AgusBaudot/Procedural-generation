using UnityEngine;

public struct Room
{
    public RectInt Bounds; //Struct defined by x, y, width & height.
    public Vector2Int Center => new Vector2Int(Bounds.x + Bounds.width / 2, Bounds.y + Bounds.height / 2);

    public Room(int x, int y , int width, int height)
    {
        Bounds = new RectInt(x, y, width, height);
    }

    public bool isCollidingWith(Room other, int padding = 0)
    {
        return (
            Bounds.xMin - padding <= other.Bounds.xMax &&
            Bounds.xMax + padding >= other.Bounds.xMin &&
            Bounds.yMin - padding <= other.Bounds.yMax &&
            Bounds.yMax + padding >= other.Bounds.yMin
        );
    }

    public bool isCollidingWith(Vector2Int bottomLeft, Vector2Int size, int padding = 0)
    {
        return this.isCollidingWith(new Room(bottomLeft.x,  bottomLeft.y, size.x, size.y), padding);
    }
}