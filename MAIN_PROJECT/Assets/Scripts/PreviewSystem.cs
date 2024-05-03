using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for handling placement previews in the game
public class PreviewSystem : MonoBehaviour
{
    // Offset for the preview object's Y position
    [SerializeField]
    private float previewYOffset = 0.2f;

    // Reference to the cell indicator GameObject
    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewObject;

    // Reference to the preview material prefab
    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    // Method called when the object is initialized
    private void Start()
    {
        // Create an instance of the preview material
        previewMaterialInstance = new Material(previewMaterialPrefab);
        // Deactivate the cell indicator initially
        cellIndicator.SetActive(false);
        // Get the renderer component of the cell indicator
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    // Method to start showing the placement preview
    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        // Instantiate the preview object
        previewObject = Instantiate(prefab);
        // Prepare the preview object
        PreparePreview(previewObject);
        // Prepare the cursor based on the object size
        PrepareCursor(size);
        // Activate the cell indicator
        cellIndicator.SetActive(true);
    }

    // Method to prepare the cursor based on the object size
    private void PrepareCursor(Vector2Int size)
    {
        // Set the scale of the cell indicator
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    // Method to prepare the preview object
    private void PreparePreview(GameObject previewObject)
    {
        // Get all renderers of the preview object
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        // Iterate through each renderer
        foreach (Renderer renderer in renderers)
        {
            // Set the preview material for each renderer
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    // Method to stop showing the preview
    public void StopShowingPreview()
    {
        // Deactivate the cell indicator
        cellIndicator.SetActive(false);
        // Destroy the preview object if it exists
        if (previewObject != null)
            Destroy(previewObject);
    }

    // Method to update the position of the preview and cursor
    public void UpdatePosition(Vector3 position, bool validity)
    {
        // Update the preview position and apply feedback if preview exists
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);
        }

        // Move the cursor and apply feedback
        MoveCursor(position);
        ApplyFeedbackToCursor(validity);
    }

    // Method to apply feedback to the preview object
    private void ApplyFeedbackToPreview(bool validity)
    {
        // Set color for the preview object based on validity
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
        previewMaterialInstance.color = c;
    }

    // Method to apply feedback to the cursor
    private void ApplyFeedbackToCursor(bool validity)
    {
        // Set color for the cursor based on validity
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
    }

    // Method to move the cursor to a specific position
    private void MoveCursor(Vector3 position)
    {
        // Set the position of the cell indicator
        cellIndicator.transform.position = position;
    }

    // Method to move the preview object to a specific position
    private void MovePreview(Vector3 position)
    {
        // Set the position of the preview object with Y offset
        previewObject.transform.position = new Vector3(
            position.x,
            position.y + previewYOffset,
            position.z);
    }

    // Method to start showing the remove preview
    internal void StartShowingRemovePreview()
    {
        // Activate the cell indicator
        cellIndicator.SetActive(true);
        // Prepare the cursor for removal
        PrepareCursor(Vector2Int.one);
        // Apply feedback to cursor for removal
        ApplyFeedbackToCursor(false);
    }
}
