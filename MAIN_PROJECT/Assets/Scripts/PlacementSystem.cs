using System; 
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Class responsible for managing object placement in the game
public class PlacementSystem : MonoBehaviour
{
    // Offset for adjusting grid positions
    [SerializeField]
    private int xy_offset = 5;

    // Reference to the mouse indicator GameObject
    [SerializeField]
    private GameObject mouseIndicator;

    // Reference to the InputManager
    [SerializeField]
    private InputManager inputManager;

    // Reference to the grid
    [SerializeField]
    private Grid grid;

    // Reference to the object database
    [SerializeField]
    private ObjectDatabaseSO database;

    // Index of the selected object
    public int selectedObjectIndex = -1;

    // Reference to the grid visualization GameObject
    [SerializeField]
    private GameObject gridVisualization;

    private GridData floorData, furnitureData;

    // Reference to the preview system
    [SerializeField]
    private PreviewSystem preview;

    public SmoothCameraMovement smoothCameraMovement;
    private List<GameObject> placedGameObjects = new();

    private Vector3Int lastDetectedPosition = Vector3Int.zero; 
    
    // Method called when the object is initialized
    private void Start()
    {
          // Stop any ongoing placement
        StopPlacement(); 
        // Initialize grid data for floor and furniture
        floorData = new();
        furnitureData = new();
      
        
        // for (int i = 0; i < InterfaceAPI.buildingList.Count; i++)
        // {
        //     // int XPOS = InterfaceAPI.buildingList[i].getXPos();
        //     // int YPOS = InterfaceAPI.buildingList[i].getYPos();
        //     // int STRUCID = InterfaceAPI.buildingList[i].getStructureId();
        //     // InterfaceAPI.buildingList[i].printDetails();

        //     PlaceObject(XPOS, YPOS, STRUCID);
        // }

        //int XPOS = 0; 
        //int YPOS = 0;
        //int STRUCID = 0; 
        //PlaceObject(XPOS, YPOS, STRUCID);

        // Initialize InterfaceAPI and place objects from building list
        InterfaceAPI.Initialize(this);
        for (int i = 0; i < InterfaceAPI.buildingList.Count; i++)
        {
            int XPOS = InterfaceAPI.buildingList[i].getXPos() - xy_offset;
            int YPOS = InterfaceAPI.buildingList[i].getYPos() - xy_offset;
            int STRUCID = InterfaceAPI.buildingList[i].getStructureId();
            Debug.Log("Attempting to Place: " + i);
            PlaceObject(XPOS, YPOS, STRUCID);
            //InterfaceAPI.buildingList[i].printDetails();
        }
        InterfaceAPI.baseList.Clear();
        foreach (int useridfor in InterfaceAPI.useridList)
        {
            Debug.Log("Attempting to load " + useridfor);
            StartCoroutine(InterfaceAPI.GetAllBases(useridfor));
        }
        
    }
    // Method to stop object placement
    public void StopPlacement()
    {
        // Reset selected object index and hide grid visualization
        selectedObjectIndex = -1; 
        gridVisualization.SetActive(false); 
        // Stop showing preview and remove event listeners
        preview.StopShowingPreview(); 
        //inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement; 
        lastDetectedPosition = Vector3Int.zero; 
    }
     // Method called every frame
    private void Update()
    {
        // If no object is selected, return
        if (selectedObjectIndex < 0)
        {
            return; 
        }
         // Get mouse position in world space and convert to grid position
        Vector3  mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition); 

         // If the grid position has changed, update preview position
        if(lastDetectedPosition != gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        
            mouseIndicator.transform.position = mousePosition; 
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            lastDetectedPosition = gridPosition; 
        }
    }

     // Method to start object placement
    public void StartPlacement(int ID)
    {
        // Stop any ongoing placement
        StopPlacement();
         // Find the index of the selected object in the database
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID); 
         // If object index is not found, log an error and return
        if (selectedObjectIndex < 0 )
        {
            Debug.LogError($"No ID found {ID}");
            return; 
        }
        // Show grid visualization and start showing placement preview
        gridVisualization.SetActive(true); 
        preview.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab, database.objectsData[selectedObjectIndex].Size);
         // Add event listeners for placement and stopping placement
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement; 
    }

    private void PlaceStructure()
    {
        
        // If pointer is over UI, return
        if(inputManager.IsPointerOverUI()) 
        {
            return; 
        }

        
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        // Get mouse position and convert to grid position
        Vector3Int gridPosition = grid.WorldToCell(mousePosition); 

        // Check placement validity
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if(placementValidity == false)
        {
            return; 
        }

        //Deduct coins
        int coinsToSet = InterfaceAPI.getCoins();
        coinsToSet -= database.objectsData[selectedObjectIndex].Cost;
        InterfaceAPI.setCoins(coinsToSet);

        //Add XP
        InterfaceAPI.addXp(database.objectsData[selectedObjectIndex].XPGain);

        //Place on server
        int xpos_grid_database = gridPosition.x + xy_offset;
        int ypos_grid_database = gridPosition.z + xy_offset;

        InterfaceAPI.addBuilding(database.objectsData[selectedObjectIndex].ID, xpos_grid_database, ypos_grid_database);

        // Instantiate and position the object
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab); 
        newObject.transform.position = grid.CellToWorld(gridPosition);

        // Add the object to the list of placed objects
        placedGameObjects.Add(newObject);
        // Determine the appropriate grid data based on the object type
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData: furnitureData;

        // Add object data to the grid data
        selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, placedGameObjects.Count -1 ); 
        //Debug.Log("Placed Structure (x,y): (" + gridPosition.x + ", " + gridPosition.z + ") Structure ID: " + selectedObjectIndex);

        // Update preview position and appearance
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false); 

        // Deduct object cost from coins and check if there are enough coins remaining
        int coinCheck = InterfaceAPI.getCoins();
        coinCheck -= database.objectsData[selectedObjectIndex].Cost;

        if (coinCheck < 0)
        {
            // Stop placement if there are not enough coins and signal completion
            StopPlacement();
            smoothCameraMovement.PlacementCompleted();
        }

        // If repeat placement is allowed, return
        if (database.objectsData[selectedObjectIndex].AllowRepeatPlacement)
        {
            return;
        }

        //Allow one building at a time
        StopPlacement();
        smoothCameraMovement.PlacementCompleted();


    }

     // Method to check the validity of object placement
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // Determine the appropriate grid data based on the object type
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData: furnitureData; 
        // Check if the object can be placed at the given grid position
        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size); 
    }
    // Method to place an object from the building list
    private void PlaceObject(int XPOS, int YPOS, int STRUCID)
    {
        selectedObjectIndex = STRUCID; 
        // Check if the selected object index is valid
        if (selectedObjectIndex < 0)
        {
            // Log an error and return if the index is invalid
            Debug.LogError("Early return because of bad selected object index: " + selectedObjectIndex);
            return; 
        }

        // Calculate grid position based on XPOS and YPOS
        Vector3Int gridPosition = new Vector3Int(XPOS, 0, YPOS); // Assuming Y is the vertical axis
        // Check placement validity at the calculated grid position
        bool placementValidity = CheckPlacementValidity(gridPosition, STRUCID);
        if (placementValidity == false)
        {
            // Log an error and return if placement is not valid
            Debug.LogError("Early Return because of Bad placement Validity");
            return; 
        }
        // Instantiate the object from the database prefab
        GameObject newObject = Instantiate(database.objectsData[STRUCID].Prefab); 
        // Position the object at the calculated grid position
        newObject.transform.position = grid.CellToWorld(gridPosition);

        // Add the object to the list of placed objects
        placedGameObjects.Add(newObject);
        // Determine the appropriate grid data based on the object type
        GridData selectedData = database.objectsData[STRUCID].ID == 0 ? floorData : furnitureData;
        // Add object data to the grid data
        selectedData.AddObjectAt(gridPosition, database.objectsData[STRUCID].Size, database.objectsData[STRUCID].ID, placedGameObjects.Count - 1); 

        //Debug.Log($"Grid position is: " + gridPosition); 
        //Debug.Log($"Structure ID is: " + selectedObjectIndex); 

        // Log the loaded structure details
        //Debug.Log("Loaded Structure (x,y): (" + gridPosition.x + ", " + gridPosition.z + ") Structure ID: " + selectedObjectIndex);
    }

    
}
