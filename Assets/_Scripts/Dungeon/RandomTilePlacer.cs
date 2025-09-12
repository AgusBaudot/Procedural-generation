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
        //IGenerator generator = new RandomTileGenerator(Random.Range(1, 101));
        IGenerator generator = new PerlinNoiseGenerator(Random.Range(1, 101));
        generator.Generate(Tilemap, MapSize, Tiles);
    }

    private void ClearAllTiles()
    {
        Vector2Int min = -MapSize / 2;
        Vector2Int max = MapSize / 2;
        for (int r = min.y; r < max.y; ++r)
        {
            for (int c = min.x; c < max.x; ++c)
            {
                Tilemap.SetTile(new Vector3Int(c, r), null);
            }
        }
        Setup();
    }
}