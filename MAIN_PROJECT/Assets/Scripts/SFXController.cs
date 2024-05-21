using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    public Slider volumeSlider;
    private SFX audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<SFX>();
        if (audioManager != null && volumeSlider != null)
        {
            volumeSlider.value = 0.1f;  // Set initial slider value to 10%
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void SetVolume(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetVolume(volume);
        }
    }
}
