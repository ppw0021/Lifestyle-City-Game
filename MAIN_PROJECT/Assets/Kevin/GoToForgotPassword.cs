using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToForgotPassword : MonoBehaviour
{
    // UI Components
    public Button registerButton;
    public Button backButton;
    public GameObject LoginWindow;
    public GameObject ResetWindow;

    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    // Method to handle forgotpassword button click
    void OnRegisterButtonClick()
    {
        // Load the forgotpassword scene
        //SceneManager.LoadScene("ForgotPasswordScene");
        LoginWindow.SetActive(false);
        ResetWindow.SetActive(true);
    }

    void OnBackButtonClick()
    {
        LoginWindow.SetActive(true);
        ResetWindow.SetActive(false);
    }
}
