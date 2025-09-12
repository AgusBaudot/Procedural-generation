using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoiseGenerator : IGenerator
{
    private readonly System.Random _rand;
    private readonly float _noiseScale;
    private readonly int _octaves;
    private readonly float _persistence;
    private readonly float _lacunarity;
    private readonly float _offsetX;
    private readonly float _offsetY;

    // ctor: seed + noise params
    public PerlinNoiseGenerator(int seed, float noiseScale = 0.08f, int octaves = 1, float persistence = 0.5f, float lacunarity = 2f)
    {
        _rand = new System.Random(seed);
        _noiseScale = Mathf.Max(1e-6f, noiseScale); // avoid divide by zero
        _octaves = Mathf.Max(1, octaves);
        _persistence = Mathf.Clamp01(persistence);
        _lacunarity = Mathf.Max(0.01f, lacunarity);

        // generate large offsets from the seed so different seeds sample different regions of Perlin space
        _offsetX = (float)(_rand.NextDouble() * 10000.0);
        _offsetY = (float)(_rand.NextDouble() * 10000.0);
    }

    public void Generate(Tilemap tilemap, Vector2Int size, List<TileBase> tiles)
    {
        if (tilemap == null || tiles == null || tiles.Count == 0) return;

        Vector2Int min = -size / 2;
        Vector2Int max = size / 2;

        for (int y = min.y; y < max.y; ++y)
        {
            for (int x = min.x; x < max.x; ++x)
            {
                float sample = GetFractalPerlin((x + _offsetX) * _noiseScale, (y + _offsetY) * _noiseScale);
                // map sample [0,1] to index [0, tiles.Count-1] safely:
                int index = Mathf.Clamp(Mathf.FloorToInt(sample * tiles.Count), 0, tiles.Count - 1);

                tilemap.SetTile(new Vector3Int(x, y, 0), tiles[index]);
            }
        }
    }

    // fBm: combine octaves of Perlin noise
    private float GetFractalPerlin(float sampleX, float sampleY)
    {
        float amplitude = 1f;
        float frequency = 1f;
        float value = 0f;
        float maxAmp = 0f;

        for (int i = 0; i < _octaves; ++i)
        {
            float n = Mathf.PerlinNoise(sampleX * frequency, sampleY * frequency);
            value += n * amplitude;
            maxAmp += amplitude;

            amplitude *= _persistence;
            frequency *= _lacunarity;
        }

        // normalize to [0,1]
        return Mathf.Clamp01(value / maxAmp);
    }
}