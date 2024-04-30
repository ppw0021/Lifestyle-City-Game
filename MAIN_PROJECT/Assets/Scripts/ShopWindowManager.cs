using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindowManager : MonoBehaviour
{
    public GameObject shopWindow; // Assign the ShopWindow panel to this in the inspector

    void Start()
    {
        // Ensure the shop window is not visible initially
        shopWindow.SetActive(false);
    }

    // Toggle the visibility of the shop window
    public void ToggleShopWindow()
    {
        shopWindow.SetActive(!shopWindow.activeSelf);
    }
}
