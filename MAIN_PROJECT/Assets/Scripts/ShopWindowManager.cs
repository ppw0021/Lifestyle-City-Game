using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindowManager : MonoBehaviour
{
    public GameObject shopWindow; // Assign the ShopWindow panel to this in the inspector
    public GameObject ShopMinigameTutUIGroup;
    public GameObject shopButton;
    public GameObject tutorialButton;
    public GameObject miniGameButton;
    public GameObject wheelspinButton;
    public GameObject multiplayerButton;
    public GameObject animalGameButton;
    [SerializeField]
    public SmoothCameraMovement smoothCameraMovement;
    public GameObject cancelButton;

    void Start()
    {
        // Ensure the shop window is not visible initially
        shopWindow.SetActive(false);
        ShopMinigameTutUIGroup.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void buttonStateInteractable(bool shop, bool tutorial, bool minigame, bool wheelspin, bool multiplayer, bool animalGame)
    {
        shopButton.SetActive(shop);
        tutorialButton.SetActive(tutorial);
        miniGameButton.SetActive(minigame);
        wheelspinButton.SetActive(wheelspin);
        multiplayerButton.SetActive(multiplayer);
        animalGameButton.SetActive(animalGame);
    }

    public void showCancelButton(bool show)
    {
        if (!show)
        {
            cancelButton.SetActive(false);
        }
        else{
            cancelButton.SetActive(true);
        }
    }
/*
    private IEnumerator WaitForMovementComplete()
    {
        while (smoothCameraMovement.isMoving)
        {
            yield return null;
        }

        shopWindow.SetActive(!shopWindow.activeSelf);
        isMoving = false;
    }*/
}
