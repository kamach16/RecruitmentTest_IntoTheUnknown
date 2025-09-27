using UnityEngine;

public class DeploymentManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private bool canPlaceUnit;
    private GameObject unitToPlace;

    private GridManager gridManager;

    public void Initialize(GridManager gridManager)
    {
        this.gridManager = gridManager;
    }

    private void Update()
    {
        PlaceUnit();
    }

    private void PlaceUnit()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!canPlaceUnit)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.TryGetComponent(out Tile tile) && tile.Type == TileType.Traversable)
                {
                    Unit unitPlaced = Instantiate(unitToPlace, tile.transform.position, Quaternion.identity).GetComponent<Unit>();
                    unitPlaced.Initialize(tile, gridManager);
                    tile.SetUnitOnMe(unitPlaced);

                    canPlaceUnit = false;
                    unitToPlace = null;
                }
            }
        }
    }

    public void SetPlayerToPlace()
    {
        unitToPlace = playerPrefab;

        canPlaceUnit = true;
    }

    public void SetEnemyToPlace()
    {
        unitToPlace = enemyPrefab;

        canPlaceUnit = true;
    }
}
