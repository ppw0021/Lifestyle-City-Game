using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToShop : MonoBehaviour
{
    public SmoothCameraMovement smoothCameraMovement;
    public void OnShopButtonClick()
    {
        SceneManager.LoadScene("Shop");
    }
}
