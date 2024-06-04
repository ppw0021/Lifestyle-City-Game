using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClickHandler : MonoBehaviour
{
    // This method is called when the object with a collider is clicked
    private void OnMouseDown()
    {
        // Get the parent object
        GameObject parentObject = transform.parent.gameObject;
        
        // Get the BuildingInformation component from the parent object
        BuildingInformation buildingInfo = parentObject.GetComponent<BuildingInformation>();

        if (buildingInfo != null)
        {
            Debug.Log("Building Information:");
            Debug.Log("X Position: " + buildingInfo.GetXPOS());
            Debug.Log("Y Position: " + buildingInfo.GetYPOS());
            Debug.Log("Structure ID: " + buildingInfo.GetSTRUCID());
            Debug.Log("Instance ID: " + buildingInfo.GetINSTID());
        }
        else
        {
            Debug.LogError("BuildingInformation component not found on parent object!");
        }
    }
}


