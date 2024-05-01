using System; 
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private int xy_offset = 5;
    
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator; 

    [SerializeField]
    private InputManager inputManager; 

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectDatabaseSO database; 
    public int selectedObjectIndex =-1; 

    [SerializeField]
    private GameObject gridVisualization; 

    private GridData floorData, furnitureData; 

    private Renderer previewRenderer; 


    private List<GameObject> placedGameObjects = new();

    private void Start()
    {

        StopPlacement(); 
        floorData = new();
        furnitureData = new();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        
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
    }
    private void StopPlacement()
    {
        selectedObjectIndex = -1; 
        gridVisualization.SetActive(false); 
        cellIndicator.SetActive(false); 
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement; 
    }
    private void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return; 
        }
        Vector3  mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition); 

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red; 

        mouseIndicator.transform.position = mousePosition; 
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);

    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID); 
        if (selectedObjectIndex < 0 )
        {
            Debug.LogError($"No ID found {ID}");
            return; 
        }
        gridVisualization.SetActive(true); 
        cellIndicator.SetActive(true); 
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement; 
    }



    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI()) 
        {
            return; 
        }
        Vector3  mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition); 

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if(placementValidity == false)
        {
            return; 
        }

        
        //Place on server
        int xpos_grid_database = gridPosition.x + xy_offset;
        int ypos_grid_database = gridPosition.z + xy_offset;

        InterfaceAPI.addBuilding(database.objectsData[selectedObjectIndex].ID, xpos_grid_database, ypos_grid_database);

        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab); 
        newObject.transform.position = grid.CellToWorld(gridPosition);

        placedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData: furnitureData;

        selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, placedGameObjects.Count -1 ); 
        Debug.Log("Placed Structure (x,y): (" + gridPosition.x + ", " + gridPosition.z + ") Structure ID: " + selectedObjectIndex);
 

    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData: furnitureData; 

        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size); 
    }

    private void PlaceObject(int XPOS, int YPOS, int STRUCID)
    {
        
        selectedObjectIndex = STRUCID; 
        if (selectedObjectIndex < 0)
        {
            Debug.LogError("Early return because of bad selected object index: " + selectedObjectIndex);
            return; 
        }

        // Calculate grid position based on XPOS and YPOS
        Vector3Int gridPosition = new Vector3Int(XPOS, 0, YPOS); // Assuming Y is the vertical axis
       

        bool placementValidity = CheckPlacementValidity(gridPosition, STRUCID);
        if (placementValidity == false)
        {
            Debug.LogError("Early Return because of Bad placement Validity");
            return; 
        }

        GameObject newObject = Instantiate(database.objectsData[STRUCID].Prefab); 
        newObject.transform.position = grid.CellToWorld(gridPosition);

        placedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[STRUCID].ID == 0 ? floorData : furnitureData;

        selectedData.AddObjectAt(gridPosition, database.objectsData[STRUCID].Size, database.objectsData[STRUCID].ID, placedGameObjects.Count - 1); 

        //Debug.Log($"Grid position is: " + gridPosition); 
        //Debug.Log($"Structure ID is: " + selectedObjectIndex); 
        Debug.Log("Loaded Structure (x,y): (" + gridPosition.x + ", " + gridPosition.z + ") Structure ID: " + selectedObjectIndex);
    }

    
}
