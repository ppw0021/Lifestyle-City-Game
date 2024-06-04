using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
   
    public GameObject tutorialPanel;  // Assign your tutorial panel in the Inspector

    // References to the arrow buttons in the UI
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;
    
    // Lists to hold references to the hollow and full circle indicators in the UI
    public List<GameObject> hollowCircles = new List<GameObject>();
    public List<GameObject> fullCircles = new List<GameObject>();
    
    // List to hold references to the tutorial images
    public List<RawImage> images = new List<RawImage>();
    
    // Index to keep track of the current tutorial image
    private int imageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the tutorial panel is hidden initially
        tutorialPanel.SetActive(false);
        
        // Call UpdateVisuals to set up the UI based on the initial imageIndex
        UpdateVisuals();
        
        // Add listeners to the left and right arrow buttons
        leftArrowButton.GetComponent<Button>().onClick.AddListener(OnLeftArrowButtonClick);
        rightArrowButton.GetComponent<Button>().onClick.AddListener(OnRightArrowButtonClick);
    }

    // Method to update the visual representation of the tutorial progress
    private void UpdateVisuals()
    {
        // Loop through all indicators to update the visual state to reflect the current tutorial step
        for (int i = 0; i < fullCircles.Count; i++)
        {
            // The current circle (based on imageIndex) should display as hollow
            // while all others display as full
            hollowCircles[i].SetActive(i == imageIndex); // Hollow for the current step
            fullCircles[i].SetActive(i != imageIndex);   // Full for all other steps
        }

        // Loop through all images to show only the current image
        for (int i = 0; i < images.Count; i++)
        {
            images[i].gameObject.SetActive(i == imageIndex);
        }

        // Optionally, you can disable the left arrow button if we're at the first image
        // and the right arrow button if we're at the last image
        leftArrowButton.SetActive(imageIndex > 0);
        rightArrowButton.SetActive(imageIndex < images.Count - 1);
    }

    // Event handler for the left arrow button click
    private void OnLeftArrowButtonClick()
    {
        if (imageIndex > 0)
        {
            imageIndex--;
            UpdateVisuals();
        }
    }

    // Event handler for the right arrow button click
    private void OnRightArrowButtonClick()
    {
        if (imageIndex < images.Count - 1)
        {
            imageIndex++;
            UpdateVisuals();
        }
    }
}