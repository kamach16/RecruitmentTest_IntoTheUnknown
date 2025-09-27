using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour
{
    [SerializeField] private Button placePlayerButton;
    [SerializeField] private Button placeEnemyButton;
    [SerializeField] private Button generateMapButton;

    private DeploymentManager deploymentManager;
    private GridManager gridManager;

    public void Initialize(DeploymentManager deploymentManager, GridManager gridManager)
    {
        this.deploymentManager = deploymentManager;
        this.gridManager = gridManager;

        placePlayerButton.onClick.AddListener(PlacePlayerButton_OnClick);
        placeEnemyButton.onClick.AddListener(PlaceEnemyButton);
        generateMapButton.onClick.AddListener(GenerateMapButton);
    }

    private void PlacePlayerButton_OnClick()
    {
        deploymentManager.SetPlayerToPlace();
    }

    private void PlaceEnemyButton()
    {
        deploymentManager.SetEnemyToPlace();
    }

    private void GenerateMapButton()
    {
        gridManager.GenerateMap();
    }
}
