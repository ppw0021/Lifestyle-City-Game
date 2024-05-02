using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindowManager : MonoBehaviour
{
    public GameObject shopWindow; // Assign the ShopWindow panel to this in the inspector
    [SerializeField]
    public SmoothCameraMovement smoothCameraMovement;

    void Start()
    {
        // Ensure the shop window is not visible initially
        shopWindow.SetActive(false);
    }

    // Toggle the visibility of the shop window
    public void ToggleShopWindow()
    {
        if (smoothCameraMovement.isMoving)
        {
            return;
        }
        if (shopWindow.activeSelf)
        {
            smoothCameraMovement.MoveToHome();
        }
        else
        {
            smoothCameraMovement.MoveToShop();
        }

        shopWindow.SetActive(!shopWindow.activeSelf);
    }
}
