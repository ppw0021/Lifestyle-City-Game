using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToForgotPassword : MonoBehaviour
{
    // UI Components
    public Button registerButton;

    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterButtonClick);
    }

    // Method to handle forgotpassword button click
    void OnRegisterButtonClick()
    {
        // Load the forgotpassword scene
        SceneManager.LoadScene("ForgotPasswordScene");
    }
}
