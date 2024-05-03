using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Class responsible for handling input events and interactions
public class InputManager : MonoBehaviour
{
    // Reference to the scene camera
    [SerializeField]
    private Camera sceneCamera;

    // Last position of the cursor
    private Vector3 lastPosition;

    // Layer mask for object placement
    [SerializeField]
    private LayerMask placementLayermask;

    // Event invoked when the mouse button is clicked
    public event Action OnClicked;

    // Event invoked when the escape key is pressed
    public event Action OnExit;

    // Method called every frame
    private void Update()
    {
        // Check for left mouse button click and invoke OnClicked event
        if(Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        // Check for escape key press and invoke OnExit event
        if(Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    // Method to check if the pointer is over UI
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    // Method to get the selected map position based on mouse input
    public Vector3 GetSelectedMapPosition()
    {
        // Get mouse position
        Vector3 mousePos = Input.mousePosition;
        // Set mouse position z-coordinate to near clip plane of the scene camera
        mousePos.z = sceneCamera.nearClipPlane;
        // Cast a ray from the screen point to the scene
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        // Perform raycast and check if it hits an object on the placement layer
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            // Update lastPosition with the hit point
            lastPosition = hit.point;
        }
        // Return the last position
        return lastPosition;
    }
}
