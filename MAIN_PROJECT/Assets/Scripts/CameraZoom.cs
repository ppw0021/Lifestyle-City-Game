using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraZoom : MonoBehaviour
{
    // Zoom variables
    [SerializeField] private float zoomMultiplier = 4f; // Zoom multiplier adjustable in the inspector
    [SerializeField] private float minZoom = 2f; // Minimum zoom level adjustable in the inspector
    [SerializeField] private float maxZoom = 8f; // Maximum zoom level adjustable in the inspector
    [SerializeField] private float smoothTime = 0.25f; // Smooth time for zoom transition adjustable in the inspector

    private float zoom; // Current zoom level
    private float velocity = 0f; // Velocity reference for SmoothDamp

    // Pan variables
    private Vector3 dragOrigin; // To store the initial position of the mouse when panning starts
    private bool isDragging = false; // To check if panning is in progress

    // Reference to the Camera component
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the zoom level with the camera's orthographic size
        zoom = cam.orthographicSize;
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
            // Adjust the zoom level based on the scroll input and zoom multiplier
            zoom -= scroll * zoomMultiplier;
            // Clamp the zoom level to stay within the min and max zoom bounds
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            // Smoothly transition the camera's orthographic size to the target zoom level
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        }
    }

    // Method to handle panning the camera by dragging with the right mouse button
    private void HandlePan()
    {
        // Check if the right mouse button is pressed down
        if (Input.GetMouseButtonDown(1)) // Right mouse button pressed
        {
            // Capture the initial position where panning starts
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
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
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            // Move the camera by the calculated difference
            cam.transform.position += difference;
        }
    }
}
