# **RecruitmentTest_InTheUnknown**

## 📝 Description

This project is a recruitment task focused on creating a simple pathfinding demo in Unity.
The application includes a pathfinding system, a random map generation system triggered via a UI button during runtime, and two buttons for deploying allies and enemies on the map.
It also features character integration (attacking and moving across the map) and a free-look camera.

For pathfinding, I used the **Breadth-First Search (BFS)** algorithm because the grid consists of tiles with uniform movement cost.
In this case, BFS guarantees the shortest path and is simpler and faster than more general algorithms such as A*.

---

## ⚙️ Features

### 👥 **Units Deployment – UI**
- Add allies and enemies to the map using the script **`Assets/Scripts/Managers/DeploymentManager.cs`**
- Unit deployment is only possible once the map has been generated

### 🗺️ **Map Generation – UI**
- Generate the map using the script **`Assets/Scripts/Managers/GridManager.cs`**
- Map parameters can be customized in the data object (ScriptableObject) located in **`Assets/DataObjects/Data_Level_1.asset`**
- You can adjust the frequency of obstacles and covers using sliders. Additionally, you can select specific tile types to be generated and map size
- When a new map is generated, all units are removed as well

### 📷 **Free-look Camera**
- Camera movement mechanics are implemented in **`Assets/Scripts/Gameplay/GameCamera.cs`**
- Freely move around the map using the **WASD** keys or arrow keys. Hold **Left Shift** to move faster
- Rotate the camera with the **right mouse button (RMB)**

### 🎮 **Gameplay**
- Unit interaction mechanics are handled in **`Assets/Scripts/Managers/GameplayManager.cs`**
- Units use the scripts **`Assets/Scripts/Units/Player.cs`** or **`Assets/Scripts/Units/Enemy.cs`**, both inheriting from **`Assets/Scripts/Units/Unit.cs`**
- Select an ally by **left mouse button (LMB)** it, then click on a tile or an enemy to display the path
- Path selection mechanics are implemented in **`Assets/Scripts/Path/PathFinder.cs`**
- To cancel interaction with a selected unit, simply **left mouse button (LMB)** on an empty area (not a map element, e.g., the skybox)
