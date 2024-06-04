using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClickHandler : MonoBehaviour
{
    private BuildingInformation buildingInfo;
    private Renderer objectRenderer;
    private bool isTransparent = false;

    public Material transparentMaterial; // Assign this in the Inspector
    private Material originalMaterial;

    private void Start()
    {
        // Get the parent object
        GameObject parentObject = transform.parent.gameObject;
        
        // Get the BuildingInformation component from the parent object
        buildingInfo = parentObject.GetComponent<BuildingInformation>();

        // Get the Renderer component from the child object (this object)
        objectRenderer = GetComponent<Renderer>();

        // Store the original material
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    private void OnMouseDown()
    {
        if (buildingInfo != null)
        {
            Debug.Log($"Building Information: X Position: {buildingInfo.GetXPOS()}, Y Position: {buildingInfo.GetYPOS()}, Structure ID: {buildingInfo.GetSTRUCID()}, Instance ID: {buildingInfo.GetINSTID()}");
        }
        else
        {
            Debug.LogError("BuildingInformation component not found on parent object!");
        }

        if (objectRenderer != null)
        {
            ToggleTransparency();
        }
    }

    private void ToggleTransparency()
    {
        if (isTransparent)
        {
            objectRenderer.material = originalMaterial;
        }
        else
        {
            objectRenderer.material = transparentMaterial;
        }

        isTransparent = !isTransparent;
    }
}
