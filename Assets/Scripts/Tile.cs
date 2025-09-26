using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private TileType type;
    [SerializeField] private GameObject mark;

    public int X => x;
    public int Y => y;
    public TileType Type => type;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    private void OnMouseEnter()
    {
        if (type == TileType.Traversable && mark != null)
            mark.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (type == TileType.Traversable && mark != null)
            mark.SetActive(false);
    }
}
