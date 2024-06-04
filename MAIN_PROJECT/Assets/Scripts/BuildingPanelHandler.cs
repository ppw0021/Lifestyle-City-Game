using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class BuildingPanelHandler : MonoBehaviour
{
    public GameObject uiPanel;
    public TextMeshProUGUI buildingInfoText; 
    public Button closeButton; 

    private void OnEnable()
    {
        BuildingEventManager.Instance.OnBuildingClicked += ShowPanel;
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HidePanel);
        }
    }

    private void OnDisable()
    {
        BuildingEventManager.Instance.OnBuildingClicked -= ShowPanel;
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(HidePanel);
        }
    }

    private void ShowPanel(BuildingInformation buildingInfo)
    {
        if (uiPanel != null && buildingInfoText != null)
        {
            buildingInfoText.text = $"X Position: {buildingInfo.GetXPOS()}\n" +
                                    $"Y Position: {buildingInfo.GetYPOS()}\n" +
                                    $"Structure ID: {buildingInfo.GetSTRUCID()}\n" +
                                    $"Instance ID: {buildingInfo.GetINSTID()}";
            uiPanel.SetActive(true);
        }
    }

    public void HidePanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }
}