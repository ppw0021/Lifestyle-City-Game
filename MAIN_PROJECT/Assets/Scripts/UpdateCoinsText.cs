using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateCoinsText : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    private int CoinsValue; // Local variable to store coins value

    private void Start()
    {
        UpdateCoinsDisplay();
    }

    public void UpdateCoinsDisplay()
    {
        // Ensure InterfaceAPI and currentUser are properly initialized
        //if (InterfaceAPI.currentUser != null)
        //{
            CoinsValue = InterfaceAPI.getCoins();
            coinsText.text = CoinsValue.ToString();
        //}
        //else
        //{
        //    Debug.LogError("Current user or InterfaceAPI is not set properly.");
        //}
    }
}
