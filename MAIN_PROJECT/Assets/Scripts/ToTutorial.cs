using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToTutorial : MonoBehaviour
{
    //public SmoothCameraMovement moveCameraToMain;
    public void OnTutorialButtonClick()
    {
        SceneManager.LoadScene("TutorialSection");
    }
}

/*
if want overlay

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;  // Assign tutorial panel in the Inspector

    // This method is called when the tutorial button is clicked
    public void OnTutorialButtonClick()
    {
        // Toggle the active state of the tutorial panel
        tutorialPanel.SetActive(!tutorialPanel.activeSelf);
    }
}


*/