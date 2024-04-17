using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectOptions : MonoBehaviour, IPointerClickHandler
{
      public RawImage[] tutorialImages; // Array to hold RawImage components for each tutorial image
    public int[] imageIndices; // Array to hold the index of the associated tutorial image for each button

    public void OnPointerClick(PointerEventData eventData)
    {
        // Get the index of the clicked button
        int buttonIndex = GetButtonIndex();

        // Check if the button index is valid and corresponds to an image index
        if (buttonIndex >= 0 && buttonIndex < imageIndices.Length)
        {
            int imageIndex = imageIndices[buttonIndex];

            // Show the associated tutorial image
            if (imageIndex >= 0 && imageIndex < tutorialImages.Length)
            {
                foreach (RawImage image in tutorialImages)
                {
                    image.gameObject.SetActive(false);
                }
                tutorialImages[imageIndex].gameObject.SetActive(true);
            }
        }
    }

    private int GetButtonIndex()
    {
        // Find the index of this button in the parent's child list
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == transform)
            {
                return i;
            }
        }
        return -1;
    }
}