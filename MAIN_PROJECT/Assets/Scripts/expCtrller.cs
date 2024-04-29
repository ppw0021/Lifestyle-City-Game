using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class expCtrller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI lvlText;
    private int level;
    public int currentExp;
    private int targetExp;
    [SerializeField] private Image expProgressBar;

    private void Start()
    {
        InterfaceAPI.Initialize(this);
    }
    private void Update()
    {
        
        UpdateXPDisplay();
    }
    private void UpdateXPDisplay()
    {
        currentExp = InterfaceAPI.getXp();
        level = InterfaceAPI.getLevel();
        targetExp = CalculateTargetExp(level); // may need to adjust this method based on game's leveling system
        expText.text = currentExp + " / " + targetExp;
        lvlText.text = "Level: " + level.ToString();
        
        float currentExpFloat = currentExp;
        float targetExpFloat = targetExp;
        expProgressBar.fillAmount = currentExpFloat / targetExpFloat;
    }

    private int CalculateTargetExp(int level)
    {
        return 100 + (level * 50); // Adjust this formula as needed
    }
}

//Kevin
/*
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
        currentExp = InterfaceAPI.getXp();
        level = InterfaceAPI.getLevel();
        targetExp = CalculateTargetExp(level); // may need to adjust this method based on game's leveling system

        // Debug log to show simulated XP and level values
        Debug.Log("XP from API: " + currentExp);
        Debug.Log("Level from API: " + level);

        UpdateXPDisplay();
    }

    private void SetXPToAPI()
    {
        // Simulating API call to set XP and level values
        // Here we don't need to actually set anything since it's a simulation

        // Debug log to confirm simulated XP and level values set to API
        Debug.Log("XP set to API: " + currentExp);
        Debug.Log("Level set to API: " + level);
    }

    // Placeholder method to calculate target XP based on level
    private float CalculateTargetExp(int level)
    {
        return 100 + (level * 50); // Adjust this formula as needed
    }
}*/

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








