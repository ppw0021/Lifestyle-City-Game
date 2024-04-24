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
