using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Class responsible for managing grid data
public class GridData
{
    // Dictionary to store placed objects' data with their positions
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    // Method to add an object to the grid at a specific position
    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        // Calculate positions that the object will occupy on the grid
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize); 
        // Create PlacementData object to store object's data
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        // Iterate through each position and add the object's data to the dictionary
        foreach(var pos in positionToOccupy)
        {
            // Check if the position is already occupied by another object
            if(placedObjects.ContainsKey(pos))
            {
                // Throw an exception if the position is already occupied
                throw new Exception($"Dictionary already contains this cell position {pos}");
            }
            // Add object's data to the dictionary with its position
            placedObjects[pos] = data; 
        }
    }

    // Method to calculate positions that an object will occupy on the grid
    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new(); // Initialize list to store positions
        // Iterate through each cell that the object will occupy
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                // Calculate position based on grid position and object size
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal; // Return list of occupied positions
    }

    // Method to check if an object can be placed at a specific position
    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        // Calculate positions that the object will occupy on the grid
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize); 
        // Iterate through each position and check if it's already occupied
        foreach (var pos in positionToOccupy)
        {
            // If any position is occupied, return false indicating object cannot be placed
            if (placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }
        // If all positions are available, return true indicating object can be placed
        return true; 
    }
}

// Class to store data of objects placed on the grid
public class PlacementData
{
    // List of positions occupied by the object
    public List<Vector3Int> occupiedPositions;
    
     // Unique identifier of the object
    public int ID { get; private set;}
    // Index of the placed object
    public int PlacedObjectIndex {get; private set;}

    // Constructor to initialize PlacementData object
    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex; 
    }
}