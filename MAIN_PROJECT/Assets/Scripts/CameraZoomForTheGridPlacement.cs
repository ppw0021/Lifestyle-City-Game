using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraZoomForTheGridPlacement : MonoBehaviour
{
    // Zoom variables
    [SerializeField] private float zoomMultiplier = 4f; // Zoom multiplier adjustable in the inspector
    [SerializeField] private float minZoom = 15f; // Minimum field of view adjustable in the inspector
    [SerializeField] private float maxZoom = 60f; // Maximum field of view adjustable in the inspector
    [SerializeField] private float smoothTime = 0.25f; // Smooth time for zoom transition adjustable in the inspector

    private float targetFOV; // Target field of view
    private float velocity = 0f; // Velocity reference for SmoothDamp

    // Pan variables
    [SerializeField] private float panSpeed = 0.3f; // Speed of panning adjustable in the inspector
    private Vector3 dragOrigin; // To store the initial position of the mouse when panning starts
    private bool isDragging = false; // To check if panning is in progress

    // Reference to the Camera component
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the target FOV with the camera's current field of view
        targetFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom(); // Handle zooming functionality
        HandlePan(); // Handle panning functionality
    }

    // Method to handle zooming in and out with the mouse scroll wheel
    private void HandleZoom()
    {
        // Check if the mouse is not over a UI element
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Get the scroll input from the mouse
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // Adjust the target field of view based on the scroll input and zoom multiplier
            targetFOV -= scroll * zoomMultiplier;
            // Clamp the target field of view to stay within the min and max zoom bounds
            targetFOV = Mathf.Clamp(targetFOV, minZoom, maxZoom);
            // Smoothly transition the camera's field of view to the target FOV
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref velocity, smoothTime);
        }
    }

    // Method to handle panning the camera by dragging with the right mouse button
    private void HandlePan()
    {
        // Check if the right mouse button is pressed down
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            // Capture the initial position where panning starts
            dragOrigin = Input.mousePosition;
            // Set the flag to indicate that panning has started
            isDragging = true;
        }

        // Check if the right mouse button is released
        if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            // Set the flag to indicate that panning has stopped
            isDragging = false;
        }

        // If panning is in progress
        if (isDragging) // While panning
        {
            // Calculate the difference between the initial and current mouse positions
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 difference = cam.ScreenToViewportPoint(dragOrigin - currentMousePos);

            // Move the camera by the calculated difference
            Vector3 move = new Vector3(difference.x * panSpeed, 0, difference.y * panSpeed);
            cam.transform.Translate(move, Space.World);

            // Update dragOrigin for the next frame
            dragOrigin = currentMousePos;
        }
    }
}
