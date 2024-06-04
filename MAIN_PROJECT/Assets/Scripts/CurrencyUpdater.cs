using System.Collections;
using UnityEngine;
using TMPro;

public class CurrencyUpdater : MonoBehaviour
{
    public SoundEffectsManager soundEffectsManager;
    public TextMeshProUGUI differenceText;
    private int previousCoins;

    private const string StringFormat = "+#;-#;0";
    private const float DisplayDuration = 3f;

    private void Start()
    {
        // Initialize previousCoins with the initial coins value
        previousCoins = InterfaceAPI.getCoins();

        // Check if differenceText is assigned in the Inspector
        if (differenceText != null)
        {
            // Update the text initially
            UpdateDifferenceText();
        }
        else
        {
            //Debug.LogError("differenceText is not assigned in the Inspector.");
        }
    }

    private void Update()
    {
        // Check for changes and update text if necessary
        int currentCoins = InterfaceAPI.getCoins();
        if (currentCoins != previousCoins)
        {
            UpdateDifferenceText();
            previousCoins = currentCoins;
        }
    }

    // Method to update the difference text
    private void UpdateDifferenceText()
    {
        int currentCoins = InterfaceAPI.getCoins();
        int difference = currentCoins - previousCoins;

        // Ensure differenceText is not null before updating
        if (differenceText != null)
        {
            // Set text color based on the sign of the difference
            SetDifferenceTextColor(difference);

            // Play sound based on the coin difference
            PlaySoundEffect(difference);

            // Update the text to display the difference
            differenceText.text = difference != 0 ? difference.ToString(StringFormat) : "";

            // Start coroutine to clear the text after a specified delay
            StartCoroutine(ClearTextAfterDelay(DisplayDuration));
        }
        else
        {
            Debug.LogError("differenceText is not assigned.");
        }
    }

    // Method to set the color of the difference text
    private void SetDifferenceTextColor(int difference)
    {
        if (difference > 0)
        {
            differenceText.color = Color.green; // Positive numbers in green
        }
        else if (difference < 0)
        {
            differenceText.color = Color.red; // Negative numbers in red
        }
        else
        {
            differenceText.color = Color.white; // Zero can have a default color
        }
    }

    // Method to play sound effects based on the coin difference
    private void PlaySoundEffect(int difference)
    {
        if (soundEffectsManager != null)
        {
            if (difference > 0)
            {
                soundEffectsManager.PlayAudioClip(0); // Play sound for positive difference
            }
            else if (difference < 0)
            {
                soundEffectsManager.PlayAudioClip(1); // Play sound for negative difference
            }
        }
        else
        {
            Debug.LogError("soundEffectsManager is not assigned.");
        }
    }

    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Ensure differenceText is not null before clearing the text
        if (differenceText != null)
        {
            // Clear the text after the specified delay
            differenceText.text = "";
        }
        else
        {
            Debug.LogError("differenceText is not assigned.");
        }
    }
}
