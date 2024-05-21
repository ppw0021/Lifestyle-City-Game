using System.Collections;
using UnityEngine;
using TMPro;

public class CurrencyUpdater : MonoBehaviour
{
    public TextMeshProUGUI differenceText;
    public AudioSource coinSpendSFX;
    public AudioSource coinRecieveSFX;
    private int previousCoins;

    private void Start()
    {
        // Check if the required AudioSource components are assigned in the Inspector
        if (coinSpendSFX == null || coinRecieveSFX == null)
        {
            Debug.LogError("AudioSource components are not assigned in the Inspector.");
            return;
        }

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
            Debug.LogError("differenceText is not assigned in the Inspector.");
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

    private void UpdateDifferenceText()
    {
        int currentCoins = InterfaceAPI.getCoins();
        int difference = currentCoins - previousCoins;

        // Ensure differenceText is not null before updating
        if (differenceText != null)
        {
            // Set text color based on the sign of the difference
            if (difference > 0)
            {
                differenceText.color = Color.green; // Positive numbers in green
                if (coinRecieveSFX != null)
                {
                    coinRecieveSFX.Play();
                }
                else
                {
                    Debug.LogError("coinRecieveSFX is not assigned.");
                }
            }
            else if (difference < 0)
            {
                differenceText.color = Color.red; // Negative numbers in red
                if (coinSpendSFX != null)
                {
                    coinSpendSFX.Play();
                }
                else
                {
                    Debug.LogError("coinSpendSFX is not assigned.");
                }
            }
            else
            {
                differenceText.color = Color.white; // Zero can have a default color
            }

            // Update the text to display the difference
            differenceText.text = difference != 0 ? difference.ToString("+#;-#;0") : "";

            // Start coroutine to clear the text after a specified delay
            float displayDuration = 3f; // Adjust this value as needed (in seconds)
            StartCoroutine(ClearTextAfterDelay(displayDuration));
        }
        else
        {
            Debug.LogError("differenceText is not assigned.");
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
