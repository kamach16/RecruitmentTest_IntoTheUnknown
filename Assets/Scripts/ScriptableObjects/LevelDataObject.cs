using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Level_", menuName = "Scriptable Objects/Level Data Object")]
public class LevelDataObject : ScriptableObject
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;

    [Header("Tile types")]
    [SerializeField] private List<GameObject> traversableTilePrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> obstacleTilePrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> coverTilePrefabs = new List<GameObject>();
    [SerializeField] [Range(0, 100)] private float percentageToGetObstacle;
    [SerializeField] [Range(0, 100)] private float percentageToGetCover;

    public int Width => width;
    public int Height => height;

    public List<GameObject> TraversableTilePrefabs => traversableTilePrefabs;
    public List<GameObject> ObstacleTilePrefabs => obstacleTilePrefabs;
    public List<GameObject> CoverTilePrefabs => coverTilePrefabs;
    public float PercentageToGetObstacle => percentageToGetObstacle;
    public float PercentageToGetCover => percentageToGetCover;
}
