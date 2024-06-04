using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToForgotPassword : MonoBehaviour
{
    public Button resetButton;
    public Button resetBackButton;
    public Button registerButton;
    public Button registerBackButton;
    
    public GameObject LoginWindow;
    public GameObject ResetWindow;
    public GameObject RegisterWindow;

    void Start()
    {
        resetButton.onClick.AddListener(onResetPassButtonClick);
        resetBackButton.onClick.AddListener(OnBackButtonClick);
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        registerBackButton.onClick.AddListener(OnRegisterBackButtonClick);
        
    }

    void onResetPassButtonClick()
    {
        LoginWindow.SetActive(false);
        ResetWindow.SetActive(true);
    }

    void OnBackButtonClick()
    {
        LoginWindow.SetActive(true);
        ResetWindow.SetActive(false);
    }
    
    void OnRegisterButtonClick()
    {
        LoginWindow.SetActive(false);
        RegisterWindow.SetActive(true);
    }

    void OnRegisterBackButtonClick()
    {
        RegisterWindow.SetActive(false);
        LoginWindow.SetActive(true);
    }

}