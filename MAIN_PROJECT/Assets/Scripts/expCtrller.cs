using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class expCtrller : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private int level;
    public float currentExp;
    [SerializeField] private float targetExp;
    [SerializeField] private Image expProgressBar;

    private void Start()
    {
        // Initialize XP values
        GetXPFromAPI();
    }

    public void GainXP(float amount)
    {
        currentExp += amount;
        CheckLevelUp();
        UpdateXPDisplay();
        SetXPToAPI();
    }

    private void CheckLevelUp()
    {
        if (currentExp >= targetExp)
        {
            currentExp -= targetExp;
            level++;
            targetExp += 50;
        }
    }

    private void UpdateXPDisplay()
    {
        expText.text = currentExp + " / " + targetExp;
        lvlText.text = "Level: " + level.ToString();
        expProgressBar.fillAmount = currentExp / targetExp;
    }

    private void GetXPFromAPI()
    {
        // Get XP values from the API
        int xpFromAPI = InterfaceAPI.getXp();
        level = InterfaceAPI.getLevel();
        currentExp = xpFromAPI;
        targetExp = CalculateTargetExp(level);

        UpdateXPDisplay();
    }

    private void SetXPToAPI()
    {
        // Set XP values to the API
        InterfaceAPI.setXp((int)currentExp);
        InterfaceAPI.setLevel(level);
    }

    private void Update()
    {
        // Check for spacebar input to gain XP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainXP(12); // Gain 12 XP when spacebar is pressed
        }
    }

    // Placeholder method to calculate target XP based on level
    private float CalculateTargetExp(int level)
    {
        return 100 + (level * 50); // Adjust this formula as needed for the game
    }

}


// before code





/*

code without space


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class expCtrller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] public int level;
    public float CurrentExp;
    [SerializeField] private float TargetExp;
    [SerializeField] private Image expProgressBar;

    // Call this method whenever you want to update the experience points
    // e.g., after building a house or completing a mission
    public void UpdateExp(float expGained)
    {
        CurrentExp += expGained;
        ExpController();
    }

    private void ExpController()
    {
        lvlText.text = "Level: " + level.ToString();
        expProgressBar.fillAmount = CurrentExp / TargetExp;

        // Check if the player has enough experience to level up
        if (CurrentExp >= TargetExp)
        {
            CurrentExp -= TargetExp; // Subtract the target exp to carry over excess exp
            level++;
            UpdateTargetExp(); // Update the target experience for the next level
        }

        // Update the UI text to show current and target experience
        expText.text = CurrentExp + " / " + TargetExp;
    }

    // Update the target experience required for the next level
    private void UpdateTargetExp()
    {
        TargetExp += 50; // Increase the target exp for the next level (adjust as needed)
    }

}
*/








