using UnityEngine;

public class DeploymentManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private bool canPlaceUnit;
    private GameObject unitToPlace;

    private void Update()
    {
        PlaceUnit();
    }

    public void PlaceUnit()
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

                    canPlaceUnit = false;
                    unitToPlace = null;
                }
            }
        }
    }

    // ui button event 
    public void SetUnitToPlace(GameObject unitPrefab)
    {
        unitToPlace = unitPrefab;

        canPlaceUnit = true;
    }
}
