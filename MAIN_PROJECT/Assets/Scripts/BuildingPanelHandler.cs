using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPanelHandler : MonoBehaviour
{
    public GameObject uiPanel; // Assign the UI Panel in the Inspector
    private bool isPanelVisible = false;

    private void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(isPanelVisible);
        }
    }

    public void TogglePanel()
    {
        if (uiPanel != null)
        {
            isPanelVisible = !isPanelVisible;
            uiPanel.SetActive(isPanelVisible);
        }
    }
}
