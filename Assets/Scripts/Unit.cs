using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int MoveRange = 3;
    [SerializeField] private int AttackRange = 2;
    [SerializeField] private float moveSpeed = 3f;

    private Tile currentTile;
    private GridManager gridManager;

    private bool isMoving;

    public void Initialize(Tile currentTile, GridManager gridManager)
    {
        this.currentTile = currentTile;
        this.gridManager = gridManager;
    }

    public void MoveTo(Tile tile)
    {
        List<Tile> path = Pathfinder.FindPath(gridManager, currentTile, tile);
        if (!isMoving && path != null && path.Count <= MoveRange)
        {
            StopAllCoroutines();
            StartCoroutine(MoveAlongPath(path));
        }
    }

    private IEnumerator MoveAlongPath(List<Tile> path)
    {
        isMoving = true;

        foreach (Tile tile in path)
        {
            Vector3 targetPos = tile.transform.position;

            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentTile = tile;
        }

        isMoving = false;
    }

    public bool CanAttack(Tile enemyTile, GridManager grid)
    {
        var path = Pathfinder.FindPath(grid, currentTile, enemyTile);
        return path != null && path.Count <= AttackRange;
    }
}