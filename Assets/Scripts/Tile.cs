using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private TileType type;
    [SerializeField] private GameObject defaultMarker;
    [SerializeField] private GameObject moveReachablePathMarker;
    [SerializeField] private GameObject moveUnreachablePathMarker;
    [SerializeField] private GameObject attackReachablePathMarker;
    [SerializeField] private GameObject attackUnreachablePathMarker;

    public int X => x;
    public int Y => y;
    public TileType Type => type;

    public Unit UnitOnMe { get; private set; }
    public bool HasUnit => UnitOnMe != null;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetUnitOnMe(Unit unitOnMe)
    {
        UnitOnMe = unitOnMe;
    }

    public void HideMarker()
    {
        moveUnreachablePathMarker.SetActive(false);
        moveReachablePathMarker.SetActive(false);
        attackReachablePathMarker.SetActive(false);
        attackUnreachablePathMarker.SetActive(false);
        defaultMarker.SetActive(false);
    }

    public void ShowMoveReachablePathMarker()
    {
        moveReachablePathMarker.SetActive(true);
        moveUnreachablePathMarker.SetActive(false);
    }

    public void ShowMoveUnreachablePathMarker()
    {
        moveUnreachablePathMarker.SetActive(true);
        moveReachablePathMarker.SetActive(false);
    }

    public void ShowAttackReachablePathMarker()
    {
        attackReachablePathMarker.SetActive(true);
        attackUnreachablePathMarker.SetActive(false);
    }

    public void ShowAttackUnreachablePathMarker()
    {
        attackUnreachablePathMarker.SetActive(true);
        attackReachablePathMarker.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (!GameplayManager.Instance.HasInteractionWithUnit && type == TileType.Traversable && defaultMarker != null)
            defaultMarker.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!GameplayManager.Instance.HasInteractionWithUnit && type == TileType.Traversable && defaultMarker != null)
            defaultMarker.SetActive(false);
    }
}
