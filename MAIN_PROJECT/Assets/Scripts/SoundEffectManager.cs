using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioSource[] audioSources; // Assign your AudioSources in the inspector
    public Slider volumeSlider; // Assign your UI Slider in the inspector

    private void Start()
    {
        // Initialize the slider value and add listener
        if (volumeSlider != null)
        {
            volumeSlider.value = 1f; // You can set a default value
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Method to set volume
    public void SetVolume(float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    // Method to play a specific audio clip by index
    public void PlayAudioClip(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            audioSources[index].Play();
        }
        else
        {
            Debug.LogWarning("Audio clip index out of range.");
        }
    }
}

