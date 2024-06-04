using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraZoomForTheGridPlacement : MonoBehaviour
{
    // Zoom variables
    [SerializeField] private float zoomSpeed = 10f; // Speed of zooming adjustable in the inspector
    [SerializeField] private float minZoomDistance = 5f; // Minimum zoom distance adjustable in the inspector
    [SerializeField] private float maxZoomDistance = 30f; // Maximum zoom distance adjustable in the inspector

    // Pan variables
    [SerializeField] private float panSpeed = 0.3f; // Speed of panning adjustable in the inspector
    private Vector3 dragOrigin; // To store the initial position of the mouse when panning starts
    private bool isDragging = false; // To check if panning is in progress

    // Boundary points
    [SerializeField] private Vector3 boundaryPoint1;
    [SerializeField] private Vector3 boundaryPoint2;
    [SerializeField] private Vector3 boundaryPoint3;
    [SerializeField] private Vector3 boundaryPoint4;

    // Reference to the Camera component
    [SerializeField] private Camera cam;

    // Default camera position and rotation
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the camera component is assigned
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }

        // Store the default position and rotation of the camera
        defaultPosition = cam.transform.position;
        defaultRotation = cam.transform.rotation;
    }

    // Called when the object becomes enabled and active
    void OnEnable()
    {
        // Reset the camera to its default position and rotation
        if (cam != null)
        {
            cam.transform.position = defaultPosition;
            cam.transform.rotation = defaultRotation;
        }
    }

    // Called when a new scene is loaded
    void OnDisable()
    {
        // Reset the camera to its default position and rotation
        if (cam != null)
        {
            cam.transform.position = defaultPosition;
            cam.transform.rotation = defaultRotation;
        }
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
            // Calculate the new camera position based on the scroll input and zoom speed
            Vector3 direction = cam.transform.forward * scroll * zoomSpeed;
            Vector3 newPosition = cam.transform.position + direction;

            // Calculate the distance from the new position to the origin
            float distance = Vector3.Distance(newPosition, Vector3.zero);
            // Clamp the distance to stay within the min and max zoom bounds
            if (distance >= minZoomDistance && distance <= maxZoomDistance)
            {
                // Apply the new position to the camera
                cam.transform.position = newPosition;
            }
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
