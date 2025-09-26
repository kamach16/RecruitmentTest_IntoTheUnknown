using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Level_", menuName = "Scriptable Objects/Level Data Object", order = 1)]
public class LevelDataObject : ScriptableObject
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;

    [Header("Tile types")]
    [SerializeField] private List<GameObject> traversableTilePrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> obstacleTilePrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> coverTilePrefabs = new List<GameObject>();

    public int Width => width;
    public int Height => height;

    public List<GameObject> TraversableTilePrefabs => traversableTilePrefabs;
    public List<GameObject> ObstacleTilePrefabs => obstacleTilePrefabs;
    public List<GameObject> CoverTilePrefabs => coverTilePrefabs;
}
