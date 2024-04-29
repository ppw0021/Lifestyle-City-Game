using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateCoinsText : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    private int CoinsValue; // Local variable to store coins value
    public bool triedTo = false;
    private void Start()
    {
        UpdateCoinsDisplay();
        InterfaceAPI.Initialize(this);
    }

    public void Update()
    {
        try
        {
            UpdateCoinsDisplay();
        }
        catch(Exception)
        {
            if (!triedTo)
            {
                triedTo = true;
                Debug.Log("No User Value");
            }
        }
    }

    public void UpdateCoinsDisplay()
    {
        CoinsValue = InterfaceAPI.getCoins();
        coinsText.text = "$" + CoinsValue.ToString();
    }
}
