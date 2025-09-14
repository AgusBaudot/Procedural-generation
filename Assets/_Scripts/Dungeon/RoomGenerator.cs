using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator
{
    private Vector2Int _widthRange;
    private Vector2Int _heightRange;
    private Vector2Int _mapSize;
    private int _maxAttempts;
    // private IGenerator _generator;
    private List<Room> _rooms = new List<Room>();

    public RoomGenerator (/*IGenerator generator, */Vector2Int size, int maxAttempts, Vector2Int widthRange, Vector2Int heightRange)
    {
        // _generator = generator;
        _mapSize = size;
        _maxAttempts = maxAttempts;
        _widthRange = widthRange;
        _heightRange = heightRange;
    }

    public List<Room> GenerateRooms(int padding = 0)
    {
        Vector2Int rndPosition = Vector2Int.zero;
        Vector2Int rndSize = Vector2Int.zero;
        bool isValid = true;
        for (int i = 0; i < _maxAttempts; i++)
        {
            //Generate random point and size of room.
            rndPosition = new Vector2Int(Random.Range(0, _mapSize.x), Random.Range(0, _mapSize.y));
            rndSize = new Vector2Int(Random.Range(_widthRange.x, _widthRange.y),  Random.Range(_heightRange.x, _heightRange.y));
            isValid = true;

            if (rndPosition.x + rndSize.x >= _mapSize.x || rndPosition.y + rndSize.y >= _mapSize.y)
            {
                continue;
            }
            
            //Check if any of the generated data is not valid.
            foreach (Room room in _rooms)
            {
                //If selected position or its end point are inside another room, exit.
                if (room.isCollidingWith(rndPosition, rndSize, padding))
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                //If room candidate doesn't collide with any other room, generate it.
                _rooms.Add(new Room(rndPosition.x, rndPosition.y, rndSize.x, rndSize.y));
                Debug.Log($"Room generated: {rndPosition}, {rndSize}");
            }
        }

        return _rooms;
    }
}