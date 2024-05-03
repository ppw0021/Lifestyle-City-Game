using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTutorial : MonoBehaviour
{
    public string tutorialSceneName; // Name of the tutorial scene

    // This method is called when the "tutorial" button is pressed
    public void OnTutorialButtonClick()
    {
        // Load the tutorial scene
        SceneManager.LoadScene(tutorialSceneName);
    }

    // This method is called when the "back" button is pressed
     public void moveToTutorial()
    {
        SceneManager.LoadScene("TutorialSection");
    }
}

