using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private Unit currentUnit;

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
                if (hit.transform.TryGetComponent(out Unit unit))
                {
                    currentUnit = unit;
                }
                else if (currentUnit != null && hit.transform.TryGetComponent(out Tile tile) && tile.Type == TileType.Traversable)
                {
                    currentUnit.MoveTo(tile);
                }
            }
            else
                currentUnit = null;
        }
    }
}
