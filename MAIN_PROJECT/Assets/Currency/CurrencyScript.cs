using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyScript : MonoBehaviour
{

    public int coins;
    public int farmsOwned = 0;
    public float coinGenerationInterval = 1;
    public TextMeshProUGUI coinDisplay;
    public TextMeshProUGUI farmsOwnedDisplay;

    // Start is called before the first frame update
    void Start()
    {
        coins = InterfaceAPI.currentUser.coins;  // Set coins first based on the current user
        Debug.Log(coins);
        StartCoroutine(GenerateCoins());
        UpdateUI();  // Ensure UI updates immediately after setting the coins
    }


    private IEnumerator GenerateCoins()
    {
        while (farmsOwned > 0)
        {
            yield return new WaitForSeconds(coinGenerationInterval);
            coins += 10 * farmsOwned;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (coinDisplay != null)
            coinDisplay.text = "Coins: " + coins;

        if (farmsOwnedDisplay != null)
            farmsOwnedDisplay.text = "Owned: " + farmsOwned;
    }

    public void BuyFarm()
    {
        if (coins >= 100)
        {
            coins -= 100;
            farmsOwned += 1;
            if (farmsOwned == 1)  // If it's the first farm, start the coroutine
            {
                StartCoroutine(GenerateCoins());
            }
            UpdateUI();
        }
    }

}
