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
        placeEnemyButton.onClick.AddListener(PlaceEnemyButton_OnClick);
        generateMapButton.onClick.AddListener(GenerateMapButton_OnClick);
    }

    private void PlacePlayerButton_OnClick()
    {
        deploymentManager.SetPlayerToPlace();
    }

    private void PlaceEnemyButton_OnClick()
    {
        deploymentManager.SetEnemyToPlace();
    }

    private void GenerateMapButton_OnClick()
    {
        gridManager.GenerateMap();
    }
}
