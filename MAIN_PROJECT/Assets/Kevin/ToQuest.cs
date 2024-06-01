using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToQuest : MonoBehaviour
{
   // Assign these in the Inspector
    public GameObject questPanel;  // The quest panel to show/hide
    public Button questButton;     // The button that toggles the quest panel

    void Start()
    {
        // Ensure questPanel is assigned
        if (questPanel == null)
        {
            Debug.LogError("Quest panel is not assigned in the Inspector");
            return;
        }

        // Ensure questButton is assigned
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
