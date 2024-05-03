using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class expCtrller : MonoBehaviour
{
    // References to TextMeshProUGUI components for displaying experience and level
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI lvlText;

    // Variables to store player level and experience points
    private int level;
    public int currentExp;
    private int targetExp; // Target experience needed to reach the next level

    // Reference to the UI image representing the experience progress bar
    [SerializeField] private Image expProgressBar;

    private void Start()
    {
        // Initialize the interface API
        InterfaceAPI.Initialize(this);
    }

    private void Update()
    {
        // Update the experience display
        UpdateXPDisplay();
    }

    // Method to update the experience display UI
    private void UpdateXPDisplay()
    {
        // Retrieve current experience and level from the interface API
        currentExp = InterfaceAPI.getXp();
        level = InterfaceAPI.getLevel();

        // Calculate the target experience needed to reach the next level
        targetExp = CalculateTargetExp(level); // May need adjustment based on the game's leveling system

        // Update the experience and level display text
        expText.text = currentExp + " / " + targetExp;
        lvlText.text = "Level: " + level.ToString();
        
        // Calculate the fill amount for the experience progress bar
        float currentExpFloat = currentExp;
        float targetExpFloat = targetExp;
        expProgressBar.fillAmount = currentExpFloat / targetExpFloat;
    }

    // Method to calculate the target experience needed to reach the next level
    private int CalculateTargetExp(int level)
    {
        // Adjust the formula as needed based on the game's leveling system
        return 100 + (level * 50);
    }
}
