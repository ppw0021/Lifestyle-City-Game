using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;       // Currently selected plant for planting
    public bool isPlanting = false;     // Flag to indicate if the player is in planting mode
    public Color buyColor = Color.green;   // Color for indicating the "Buy" action
    public Color cancelColor = Color.red;  // Color for indicating the "Cancel" action

    // Start is called before the first frame update
    void Start()
    {
        InterfaceAPI.Initialize(this);  // Initialize the interface with this FarmManager
        int x = InterfaceAPI.getXp();   // Get current experience points
        int level = InterfaceAPI.getLevel();    // Get current player level
    }

    // Update is called once per frame
    void Update()
    {
        // No update logic needed for FarmManager
    }

    // Method to select a plant for planting
    public void SelectPlant(PlantItem newPlant)
    {
        if (selectPlant == newPlant)   // If the selected plant is the same as the currently selected one
        {
            Debug.Log("Deselected " + selectPlant.plant.plantName); // Log deselection message
            selectPlant.btnImage.color = buyColor;   // Set button color to buy color
            selectPlant.btnTxt.text = "Buy";        // Set button text to "Buy"
            selectPlant = null;                      // Deselect the plant
            isPlanting = false;                      // Set planting mode to false
        }
        else
        {
            if (selectPlant != null)    // If there was a previously selected plant
            {
                selectPlant.btnImage.color = buyColor;   // Set its button color to buy color
                selectPlant.btnTxt.text = "Buy";        // Set its button text to "Buy"
            }
            selectPlant = newPlant;                     // Set the new selected plant
            selectPlant.btnImage.color = cancelColor;   // Set its button color to cancel color
            selectPlant.btnTxt.text = "Cancel";         // Set its button text to "Cancel"
            Debug.Log("Selected " + selectPlant.plant.plantName); // Log selection message
            isPlanting = true;                          // Set planting mode to true
        }
    }
}
