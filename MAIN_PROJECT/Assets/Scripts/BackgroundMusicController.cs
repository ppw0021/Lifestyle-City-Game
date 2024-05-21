using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicController : MonoBehaviour
{
    public Slider volumeSlider;
    private BackgroundMusic audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<BackgroundMusic>();
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
