using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomGenerator
{
    [SerializeField] private Vector2Int WidthRange;
    [SerializeField] private Vector2Int HeightRange;
    private IGenerator generator;

    public RoomGenerator (IGenerator generator)
    {
        this.generator = generator;
    }

    public void GenerateRooms(int maxAttempts)
    {
        for (int i = 0; i < maxAttempts; i++)
        {

        }
    }
}