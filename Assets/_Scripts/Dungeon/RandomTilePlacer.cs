using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTilePlacer : MonoBehaviour
{
    [Header("Map settings")]
    [SerializeField] private Vector2Int MapSize;
    [SerializeField] private Tilemap Tilemap;
    [Header("Map tiles")]
    [SerializeField] private List<TileBase> Tiles = new List<TileBase>();
    [Header("Room settings")]
    [SerializeField] private int MaxAttempts = 50;
    [SerializeField] private Vector2Int WidthRange;
    [SerializeField] private Vector2Int HeightRange;
    [SerializeField] private int Padding;
    
    public List<Room> Rooms;

    private RectInt _actualMap;

    private void Awake()
    {
        Setup();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) ClearAllTiles();
    }

    private void Setup()
    {
        _actualMap = new RectInt(
            new Vector2Int (-MapSize.x / 2, -MapSize.y / 2),
            MapSize
            );

        // IGenerator generator = new PerlinNoiseGenerator(Random.Range(1, 101));
        // generator.Generate(Tilemap, MapSize, Tiles);
        
        //Generate rooms:
        RoomGenerator roomGenerator = new RoomGenerator(_actualMap, MaxAttempts, WidthRange, HeightRange, Random.Range(0, 1000));
        Rooms = roomGenerator.GenerateRooms(Padding);
        //Generate room filler to see them.
        IGenerator generator = new RandomTileGenerator(Random.Range(1, 101));
        foreach (Room room in Rooms)
            Colour(room);
    }

    private void ClearAllTiles()
    {
        Vector2Int min = -MapSize / 2;
        for (int r = min.y; r <= MapSize.y; ++r)
        {
            for (int c = min.x; c <= MapSize.x; ++c)
            {
                Tilemap.SetTile(new Vector3Int(c, r), null);
            }
        }
        Setup();
    }

    private void Colour(Room room)
    {
        int rnd = Random.Range(0, Tiles.Count);
        for (int c = room.Bounds.xMin; c <= room.Bounds.xMax; ++c)
        {
            for (int r = room.Bounds.yMin; r <= room.Bounds.yMax; ++r)
            {
                Tilemap.SetTile(new Vector3Int(c, r), Tiles[rnd]);
            }
        }
    }
}