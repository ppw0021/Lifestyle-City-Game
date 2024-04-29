using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoBackButton : MonoBehaviour
{
    public string mainMenuSceneName; // Name of the main menu scene

    // This method is called when the "Go Back" button is pressed
    public void OnGoBackButtonClick()
    {
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
