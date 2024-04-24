using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectOptions : MonoBehaviour
{
    public RawImage tutorialImage; // Reference to the tutorial image to display

    void Start()
    {
        // Hide all tutorial images initially
        HideAllTutorialImages();
    }

    public void OnButtonClick()
    {
        // Hide all tutorial images
        HideAllTutorialImages();

        // Show the associated tutorial image
        tutorialImage.gameObject.SetActive(true);
    }

    private void HideAllTutorialImages()
    {
        // Find all tutorial images in the scene and hide them
        RawImage[] allTutorialImages = FindObjectsOfType<RawImage>();
        foreach (RawImage image in allTutorialImages)
        {
            image.gameObject.SetActive(false);
        }
    }
}