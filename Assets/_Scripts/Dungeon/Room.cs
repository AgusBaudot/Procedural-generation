using UnityEngine;

public struct Room
{
    public RectInt Bounds; //Struct defined by x, y, width & height.
    public Vector2Int Center => new Vector2Int(Bounds.x + Bounds.width / 2, Bounds.y + Bounds.height / 2);

    public Room(int x, int y , int width, int height)
    {
        Bounds = new RectInt(x, y, width, height);
    }
}