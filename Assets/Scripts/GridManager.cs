using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private LevelDataObject levelData;

    public int Height => levelData.Height;
    public int Width => levelData.Width;

    private float tileSize = 1f;
    private int[,] mapData;
    private Tile[,] tiles;

    // ui button event 
    public void GenerateMap()
    {
        DestroyMap();
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        mapData = new int[levelData.Width, levelData.Height];

        for (int x = 0; x < levelData.Width; x++)
        {
            for (int y = 0; y < levelData.Height; y++)
            {
                float r = UnityEngine.Random.value;
                if (r < 0.1f) mapData[x, y] = 1; // obstacle
                else if (r < 0.2f) mapData[x, y] = 2; // cover
                else mapData[x, y] = 0; // traversable
            }
        }

        tiles = new Tile[levelData.Width, levelData.Height];
        for (int x = 0; x < levelData.Width; x++)
        {
            for (int y = 0; y < levelData.Height; y++)
            {
                TileType type = (TileType)mapData[x, y];
                GameObject tilePrefab = GetTargetTile(type);

                var tileObj = Instantiate(tilePrefab, new Vector3(x * tileSize, 0, y * tileSize), Quaternion.identity, transform);
                var tile = tileObj.GetComponent<Tile>();
                tile.Init(x, y);
                tiles[x, y] = tile;
            }
        }
    }

    private GameObject GetTargetTile(TileType type)
    {
        GameObject tile = null;

        switch (type)
        {
            case TileType.Traversable:
                tile = levelData.TraversableTilePrefabs[UnityEngine.Random.Range(0, levelData.TraversableTilePrefabs.Count)];
                break;
            case TileType.Obstacle:
                tile = levelData.ObstacleTilePrefabs[UnityEngine.Random.Range(0, levelData.ObstacleTilePrefabs.Count)];
                break;
            case TileType.Cover:
                tile = levelData.CoverTilePrefabs[UnityEngine.Random.Range(0, levelData.CoverTilePrefabs.Count)];
                break;
            default:
                break;
        }

        return tile;
    }

    private void DestroyMap()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }

    public Tile GetTile(int x, int y) => tiles[x, y];
}
