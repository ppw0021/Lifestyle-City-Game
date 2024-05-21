using System.Collections;
using UnityEngine;
using TMPro;

public class CurrencyUpdater : MonoBehaviour
{
    public TextMeshProUGUI differenceText;
    public AudioBehaviour coinSpendSFX;
    public AudioBehaviour coinRecieveSFX;
    private int previousCoins;

    private void Start()
    {
        // Initialize previousCoins with the initial coins value
        previousCoins = InterfaceAPI.getCoins();

        // Check if differenceText is not null before updating
        if (differenceText != null)
        {
            // Update the text initially
            UpdateDifferenceText();
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

        // Set text color based on the sign of the difference
        if (difference > 0)
        {
            differenceText.color = Color.green; // Positive numbers in green
            coinRecieveSFX.GetComponent<AudioSource>().Play();
        }
        else if (difference < 0)
        {
            differenceText.color = Color.red; // Negative numbers in red
            coinSpendSFX.GetComponent<AudioSource>().Play();
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

    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Clear the text after the specified delay
        differenceText.text = "";
    }
}
