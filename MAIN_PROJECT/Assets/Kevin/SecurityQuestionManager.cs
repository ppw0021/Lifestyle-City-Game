using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class SecurityQuestionManager : MonoBehaviour
{
    public Button backButton;
    public Button proceedButton;
    public TMP_Dropdown securityQuestionDropdown;
    public TMP_InputField answerInput;
    public TMP_Text errorMessage;

    private void Start()
    {
        backButton.onClick.AddListener(GoBackToRegister);
        proceedButton.onClick.AddListener(ProceedToLogin);
        InitializeDropdown();
        errorMessage.text = "";
    }

    private void InitializeDropdown()
    {
        List<string> options = new List<string> { "Select a question", "What is your childhood nickname?", "What is your first pet's name?", "What is your childhood dream job?" };
        securityQuestionDropdown.AddOptions(options);
    }

    private void GoBackToRegister()
    {
        SceneManager.LoadScene("RegisterScene");
    }

    private void ProceedToLogin()
    {
        if (ValidateInputs())
        {
            StoreUserData();
            SceneManager.LoadScene("Login (testing)");
        }
        else
        {
            errorMessage.text = "Please fill in all inputs.";
        }
    }

    private bool ValidateInputs()
    {
        return securityQuestionDropdown.value != 0 && !string.IsNullOrEmpty(answerInput.text);
    }

    private void StoreUserData()
    {
        string username = PlayerPrefs.GetString("username");
        string email = PlayerPrefs.GetString("email");
        string password = PlayerPrefs.GetString("password");
        string securityQuestion = securityQuestionDropdown.options[securityQuestionDropdown.value].text;
        string securityAnswer = answerInput.text;

        // Optional: Store data in a text file
        string path = Application.persistentDataPath + "/userdata.txt";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine($"Username: {username}");
            writer.WriteLine($"Email: {email}");
            writer.WriteLine($"Password: {password}");
            writer.WriteLine($"Security Question: {securityQuestion}");
            writer.WriteLine($"Answer: {securityAnswer}");
            writer.WriteLine();
        }

        // Optionally, clear PlayerPrefs
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("email");
        PlayerPrefs.DeleteKey("password");
    }
}