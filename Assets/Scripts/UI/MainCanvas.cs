using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private GameplayPanel gameplayPanel;

    private GameManager gameManager;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameManager = GameManager.Instance;

        gameplayPanel.Initialize(gameManager.DeploymentManager, gameManager.GridManager);
    }
}
