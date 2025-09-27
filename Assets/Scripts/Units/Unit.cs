using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private int moveRange = 3;
    [SerializeField] private int attackRange = 2;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Collider myCollider;

    private Tile currentTile;
    private GridManager gridManager;

    private bool isMoving;

    public bool IsMoving => isMoving;
    public int MoveRange => moveRange;
    public int AttackRange => attackRange;

    public void Initialize(Tile currentTile, GridManager gridManager)
    {
        this.currentTile = currentTile;
        this.gridManager = gridManager;

        Invoke("DelayedInteraction", 0.1f); // it prevents from overraycasting (when we place player, at the same time we selecting him)
    }

    public void MoveTo(Tile tile)
    {
        List<Tile> path = GetPath(tile);
        if (!isMoving && path != null && path.Count <= moveRange)
        {
            StopAllCoroutines();
            StartCoroutine(MoveAlongPath(path));
        }
    }

    public void MoveToByTilesCount(Tile tile, int count, Action callback)
    {
        List<Tile> path = GetDirectPathByTileCount(tile, count);
        
        if (!isMoving && path != null && path.Count <= moveRange + attackRange)
        {
            StopAllCoroutines();
            StartCoroutine(MoveAlongPath(path, callback));
        }
    }

    private IEnumerator MoveAlongPath(List<Tile> path, Action callback = null)
    {
        isMoving = true;
        currentTile.SetUnitOnMe(null);

        foreach (Tile tile in path)
        {
            tile.SetUnitOnMe(null);

            Vector3 targetPos = tile.transform.position;

            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            
            currentTile = tile;
        }

        currentTile.SetUnitOnMe(this);
        isMoving = false;
        callback?.Invoke();
    }

    public bool CanAttack(Tile enemyTile)
    {
        var path = Pathfinder.FindPath(gridManager, currentTile, enemyTile, true);
        return path != null && path.Count <= attackRange;
    }

    public List<Tile> GetPath(Tile endTile, bool attackPath = false)
    {
        return Pathfinder.FindPath(gridManager, currentTile, endTile, attackPath);
    }

    public List<Tile> GetDirectPathByTileCount(Tile endTile, int count, bool attackPath = false)
    {
        return Pathfinder.FindPath(gridManager, currentTile, endTile, attackPath).Take(count).ToList();
    }

    private void DelayedInteraction()
    {
        myCollider.enabled = true;
    }
}