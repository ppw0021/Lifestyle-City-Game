using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;               // Reference to the PlantObject associated with this item
    public TextMeshProUGUI nameTxt;         // Text for displaying the plant's name
    public TextMeshProUGUI costTxt;         // Text for displaying the plant's cost
    public Image icon;                      // Image for displaying the plant's icon
    public Image btnImage;                  // Image for the button associated with this item
    public TextMeshProUGUI btnTxt;          // Text for the button associated with this item
    FarmManager fm;                         // Reference to the FarmManager script

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FarmManager>();    // Find the FarmManager in the scene
        InitializeUI();                           // Initialize the UI elements of this item
    }

    // Method called when the buy button is clicked
    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.plantName);   // Log message indicating the plant purchase
        fm.SelectPlant(this);                     // Inform FarmManager of the selected plant
    }

    // Method to initialize the UI elements of this item
    void InitializeUI()
    {
        nameTxt.text = plant.plantName;        // Set the plant's name in the UI
        costTxt.text = "$" + plant.cost;       // Set the plant's cost in the UI
        icon.sprite = plant.icon;              // Set the plant's icon in the UI
    }
}