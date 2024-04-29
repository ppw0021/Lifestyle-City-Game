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
        InterfaceAPI.Initialize(this);
    }

    public void Update()
    {
        UpdateCoinsDisplay();
    }

    public void UpdateCoinsDisplay()
    {
        CoinsValue = InterfaceAPI.getCoins();
        coinsText.text = "$" + CoinsValue.ToString();
    }
}
