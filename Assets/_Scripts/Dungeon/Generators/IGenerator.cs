using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IGenerator
{
    void Generate(Tilemap tilemap, Vector2Int size, List<TileBase> tiles);
}