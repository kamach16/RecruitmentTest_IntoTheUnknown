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
    private bool mapGenerated;
    private DeploymentManager deploymentManager;

    public bool MapGenerated => mapGenerated;
    public Tile GetTile(int x, int y) => tiles[x, y];

    public void Initialize(DeploymentManager deploymentManager)
    {
        this.deploymentManager = deploymentManager;
    }

    private void GenerateTiles()
    {
        mapData = new int[levelData.Width, levelData.Height];

        for (int x = 0; x < levelData.Width; x++)
        {
            for (int y = 0; y < levelData.Height; y++)
            {
                float percentage = UnityEngine.Random.value * 100;
                if (percentage < levelData.PercentageToGetObstacle) mapData[x, y] = 1; // obstacle
                else if (percentage < levelData.PercentageToGetObstacle + levelData.PercentageToGetCover) mapData[x, y] = 2; // cover
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

    public void GenerateMap()
    {
        mapGenerated = true;

        deploymentManager.DestroyAllUnits();
        DestroyMap();
        GenerateTiles();
    }
}
