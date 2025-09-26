using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RoomGenerator
{
    private Vector2Int _widthRange;
    private Vector2Int _heightRange;
    private RectInt _mapSize;
    private int _maxAttempts;
    private int _seed;
    private Random _rnd;
    // private IGenerator _generator;
    private List<Room> _rooms = new List<Room>();

    public RoomGenerator (/*IGenerator generator, */RectInt size, int maxAttempts, Vector2Int widthRange, Vector2Int heightRange, int seed)
    {
        // _generator = generator;
        _mapSize = size;
        _maxAttempts = maxAttempts;
        _widthRange = widthRange;
        _heightRange = heightRange;
        _seed = seed;
        _rnd = new Random(seed);
    }

    public List<Room> GenerateRooms(int padding = 0)
    {
        Vector2Int rndPosition = Vector2Int.zero;
        Vector2Int rndSize = Vector2Int.zero;
        bool isValid = true;
        for (int i = 0; i < _maxAttempts; i++)
        {
            //Generate random point and size of room.
            rndPosition = new Vector2Int(_rnd.Next(_mapSize.xMin, _mapSize.xMax), _rnd.Next(_mapSize.yMin, _mapSize.yMax));
            rndSize = new Vector2Int(_rnd.Next(_widthRange.x, _widthRange.y),  _rnd.Next(_heightRange.x, _heightRange.y));
            isValid = true;

            if (rndPosition.x + rndSize.x >= _mapSize.xMax || rndPosition.y + rndSize.y >= _mapSize.yMax)
            if (
                    rndPosition.x + rndSize.x >= _mapSize.xMax ||
                    rndPosition.y + rndSize.y >= _mapSize.yMax ||
                    rndPosition.x <= _mapSize.xMin ||
                    rndPosition.y <= _mapSize.yMin
                    )
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