using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private Unit currentUnit;

    public bool HasInteractionWithUnit => currentUnit != null;

    public static GameplayManager Instance { get; private set; }

    private List<Tile> currentPath = new List<Tile>();

    private GridManager gridManager;

    private Unit targetUnit;

    private void Awake()
    {
        Instance = this;

        gridManager = FindFirstObjectByType<GridManager>();
    }

    private void Update()
    {
        InteractWithUnit();
    }

    private void InteractWithUnit()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.TryGetComponent(out Player player)) // player selecting
                {
                    // unselected unit while unit is selected
                    if (currentUnit != null)
                    {
                        currentUnit = null;
                        ResetPath();
                        return;
                    }

                    currentUnit = player;
                }
                else if (currentUnit != null && hit.transform.TryGetComponent(out Tile tile) && tile.Type == TileType.Traversable && !tile.HasUnit) // move
                {
                    currentUnit.MoveTo(tile);
                }
                else if (currentUnit != null) // attack
                {
                    bool hasEnemy = hit.transform.TryGetComponent(out Enemy enemy);
                    bool hasTile = hit.transform.TryGetComponent(out Tile tile1) &&
                                   tile1.Type == TileType.Traversable &&
                                   (tile1.HasUnit && tile1.UnitOnMe is Enemy);

                    Enemy enemyToDestroy = null;
                    Tile enemyTile = enemy != null ? gridManager.GetTile((int)enemy.transform.position.x, (int)enemy.transform.position.z) : tile1;

                    if (hasEnemy)
                        enemyToDestroy = enemy;
                    else if (hasTile)
                        enemyToDestroy = tile1.UnitOnMe as Enemy;

                    if (!currentUnit.CanAttack(enemyTile))
                        return;

                    enemyTile.SetUnitOnMe(null);
                    Destroy(enemyToDestroy.gameObject);
                }
            }
            else
            {
                currentUnit = null;
            }

            ResetPath();
        }

        if (currentUnit != null && !currentUnit.IsMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (currentUnit != null && hit.transform.TryGetComponent(out Tile tile) && tile.Type == TileType.Traversable && !tile.HasUnit) // move path
                {
                    // reset path
                    if(currentUnit.GetPath(tile) != currentPath)
                    {
                        ResetPath();
                    }

                    currentPath = currentUnit.GetPath(tile);
                    if (currentPath != null)
                    {
                        for (int i = 0; i < currentPath.Count; i++)
                        {
                            if (i <= currentUnit.MoveRange - 1)
                                currentPath[i].ShowMoveReachablePathMarker();
                            else
                                currentPath[i].ShowMoveUnreachablePathMarker();
                        }
                    }
                }
                else if (currentUnit != null) // attack path
                {
                    bool hasEnemy = hit.transform.TryGetComponent(out Enemy enemy);
                    bool hasTile = hit.transform.TryGetComponent(out Tile tile1) &&
                                   tile1.Type == TileType.Traversable &&
                                   (tile1.HasUnit && tile1.UnitOnMe is Enemy);

                    if (hasEnemy || hasTile)
                    {
                        Tile targetTile = enemy != null ? gridManager.GetTile((int)enemy.transform.position.x, (int)enemy.transform.position.z) : tile1;

                        // reset path
                        if (currentUnit.GetPath(targetTile, true) != currentPath)
                        {
                            ResetPath();
                        }

                        currentPath = currentUnit.GetPath(targetTile, true);
                        if (currentPath != null)
                        {
                            for (int i = 0; i < currentPath.Count; i++)
                            {
                                if (i <= currentUnit.AttackRange - 1)
                                    currentPath[i].ShowAttackReachablePathMarker();
                                else
                                    currentPath[i].ShowAttackUnreachablePathMarker();
                            }
                        }
                    }
                    else
                        ResetPath();
                }
                else
                    ResetPath();
            }
        }
    }

    private void ResetPath()
    {
        if (currentPath == null)
            return;

        foreach (var tileInPath in currentPath)
        {
            tileInPath.HideMarker();
        }
    }
}
