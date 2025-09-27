using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private DeploymentManager deploymentManager;
    [SerializeField] private GameplayManager gameplayManager;

    public static GameManager Instance { get; private set; }

    public GridManager GridManager => gridManager;
    public DeploymentManager DeploymentManager => deploymentManager;
    public GameplayManager GameplayManager => gameplayManager;

    private void Awake()
    {
        Instance = this;

        Initialize();
    }

    private void Initialize()
    {
        deploymentManager.Initialize(gridManager);
        gameplayManager.Initialize(gridManager);
    }
}
