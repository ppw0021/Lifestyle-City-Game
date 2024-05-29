using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    bool isPlanted = false;                 // Flag to track if a plant is currently planted in the plot
    public SpriteRenderer plant;           // Reference to the SpriteRenderer component for the plant
    int plantStage = 0;                     // Current stage of plant growth
    float timer;                            // Timer for tracking plant growth intervals
    PlantObject selectedPlant;              // Reference to the currently selected plant type
    FarmManager fm;                         // Reference to the FarmManager script for managing planting
    public SoundEffectsManager soundEffectsManager;

    // Start is called before the first frame update
    void Start()
    {
        fm = transform.parent.GetComponent<FarmManager>();  // Get FarmManager component from parent object
        // Ensure the initial sorting order is set to 1
        plant.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted)
        {
            timer -= Time.deltaTime;    // Decrease the timer by deltaTime each frame
            // If timer reaches 0 and there are more growth stages remaining
            if (timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                timer = selectedPlant.growthTime;   // Reset timer
                plantStage++;                       // Move to the next growth stage
                UpdatePlant();                      // Update the plant's appearance
            }
        }
    }

    // Called when the mouse button is pressed down on the plot
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            // If the plant has reached its final stage, harvest it
            if (plantStage == selectedPlant.plantStages.Length - 1)
            {
                Harvest();              
            }
        }
        else if (fm.isPlanting) // If FarmManager is in planting mode
        {
            Plant(fm.selectPlant.plant);    // Plant the selected plant type
        }
    }

    // Harvest the fully grown plant
    void Harvest()
    {
        Debug.Log("Harvested");                 // Log message indicating harvesting
        isPlanted = false;                      // Reset planted flag
        InterfaceAPI.addXp(25);                 // Add experience points
        int coins = InterfaceAPI.getCoins();    // Get current coin count
        coins += selectedPlant.reward;          // Add plant reward to coins
        InterfaceAPI.Initialize(this);          // Initialize the interface
        InterfaceAPI.setCoins(coins);           // Update coin count
        plant.gameObject.SetActive(false);     // Deactivate the plant object
    }

    // Plant a new plant in the plot
    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;               // Set selected plant type
        // Check if player has enough coins to plant
        if (selectedPlant.cost > InterfaceAPI.getCoins())
        {
            Debug.Log("No Money");              // Log message indicating insufficient funds
            return;                             // Exit method
        }
        Debug.Log("Planted");                   // Log message indicating successful planting
        isPlanted = true;                       // Set planted flag
        int coins = InterfaceAPI.getCoins();    // Get current coin count
        coins -= selectedPlant.cost;            // Deduct planting cost from coins
        InterfaceAPI.Initialize(this);          // Initialize the interface
        InterfaceAPI.setCoins(coins);           // Update coin count
        plantStage = 0;                         // Reset plant stage
        UpdatePlant();                          // Update the plant's appearance
        timer = selectedPlant.growthTime;       // Set timer for next growth stage
        plant.gameObject.SetActive(true);      // Activate the plant object
    }

    // Update the appearance of the plant based on its growth stage
    void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[plantStage]; // Update plant sprite
    }
}
