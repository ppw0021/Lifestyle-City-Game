using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToRegisterScene : MonoBehaviour
{
 // UI Components
    public Button registerButton;

    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterButtonClick);
    }

    // Method to handle register button click
    void OnRegisterButtonClick()
    {
        // Load the register scene
        SceneManager.LoadScene("RegisterScene");
    }
}
