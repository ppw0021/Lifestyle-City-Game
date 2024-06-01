using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToQuest : MonoBehaviour
{
public GameObject questPanel;   // Assign your quest panel in the Inspector
    public Button questButton;      // Assign your button in the Inspector

    void Start()
    {
        // Ensure questPanel and questButton are assigned
        if (questPanel == null)
        {
            Debug.LogError("Quest panel is not assigned in the Inspector");
            return;
        }
        
        if (questButton == null)
        {
            Debug.LogError("Quest button is not assigned in the Inspector");
            return;
        }

        // Ensure the quest panel is hidden initially
        questPanel.SetActive(false);

        // Add a listener to the button to call OnQuestButtonClick when clicked
        questButton.onClick.AddListener(OnQuestButtonClick);
    }

    // This method is called when the quest button is clicked
    void OnQuestButtonClick()
    {
        // Toggle the active state of the quest panel
        questPanel.SetActive(!questPanel.activeSelf);
    }
}
