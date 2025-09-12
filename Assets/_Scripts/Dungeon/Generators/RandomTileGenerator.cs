using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class RandomTileGenerator : IGenerator
{
    private System.Random _random;
    public RandomTileGenerator(int seed)
    {
        _random = new System.Random(seed);
    }

    public void Generate(Tilemap tilemap, Vector2Int size, List<TileBase> tiles)
    {
        Vector2Int min = -size / 2;
        Vector2Int max = size / 2;

        for (int r = min.y; r < max.y; ++r)
        {
            for (int c = min.x; c < max.x; ++c)
            {
                int rnd = _random.Next(tiles.Count);
                tilemap.SetTile(new Vector3Int(c, r), tiles[rnd]);
            }
        }
    }
}
