using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToTutorial : MonoBehaviour
{
    // //public SmoothCameraMovement moveCameraToMain;
    // public void OnTutorialButtonClick()
    // {
    //     SceneManager.LoadScene("TutorialSection");
    // }

    public GameObject tutorialPanel;  // Assign  tutorial panel in the Inspector public GameObject tutorialPanel;
    public Button tutorialButton;     // Assign  button in the Inspector

    void Start()
    {
        // Ensure tutorialPanel and tutorialButton are assigned
        if (tutorialPanel == null)
        {
            Debug.LogError("Tutorial panel is not assigned in the Inspector");
            return;
        }
        else
        {
            Debug.Log("Tutorial panel is assigned correctly");
        }

        if (tutorialButton == null)
        {
            Debug.LogError("Tutorial button is not assigned in the Inspector");
            return;
        }
        else
        {
            Debug.Log("Tutorial button is assigned correctly");
        }

        // Ensure the tutorial panel is hidden initially
        tutorialPanel.SetActive(false);

        // Add a listener to the button to call OnTutorialButtonClick when clicked
        tutorialButton.onClick.AddListener(OnTutorialButtonClick);
    }

    // This method is called when the tutorial button is clicked
    void OnTutorialButtonClick()
    {
        // Toggle the active state of the tutorial panel
        tutorialPanel.SetActive(!tutorialPanel.activeSelf);
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