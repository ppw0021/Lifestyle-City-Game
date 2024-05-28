using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

// The ForgotPasswordManager class handles the "Forgot Password" functionality for the application.
public class ForgotPasswordManager : MonoBehaviour
{
    // Public UI elements connected from the Unity editor.
    public Button backButton;
    public Button proceedButton;
    public TMP_InputField emailInputField;
    public TMP_InputField answerInputField;
    public TMP_Dropdown securityQuestionDropdown;
    public TMP_Text errorMessage;

    // Variables to store user data retrieved from the file.
    private string storedEmail;
    private string storedPassword;
    private string storedSecurityQuestion;
    private string storedSecurityAnswer;

    // Method called when the script instance is being loaded.
    private void Start()
    {
        // Adding listener to the back button to go back to the login scene.
        backButton.onClick.AddListener(GoBackToLogin);
        // Adding listener to the proceed button to submit the user's input.
        proceedButton.onClick.AddListener(SubmitAnswer);
        // Initially disabling interaction with the security question dropdown.
        securityQuestionDropdown.interactable = false;
        // Clearing any default options in the security question dropdown.
        securityQuestionDropdown.ClearOptions();
        // Initially hiding the error message.
        errorMessage.text = "";
    }

    // Method to switch back to the login scene.
    private void GoBackToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }

    // Method to handle the user's input and verify the email and security answer.
    private void SubmitAnswer()
    {
        // Clear any previous error message.
        errorMessage.text = "";

        // Getting the email and answer input from the user.
        string email = emailInputField.text;
        string answer = answerInputField.text;

        // Checking if the email input is empty and displaying an error message if it is.
        if (string.IsNullOrEmpty(email))
        {
            errorMessage.text = "Please enter your email.";
            return;
        }

        // Checking if the answer input is empty when the security question is interactable, displaying an error message if it is.
        if (string.IsNullOrEmpty(answer) && securityQuestionDropdown.interactable)
        {
            errorMessage.text = "Please enter your answer.";
            return;
        }

        // If the security question dropdown is not interactable, search for the email.
        if (!securityQuestionDropdown.interactable)
        {
            // If the email is found, display the security question.
            if (SearchEmail(email))
            {
                DisplaySecurityQuestion();
            }
            // If the email is not found, display an error message.
            else
            {
                errorMessage.text = "Email not found.";
            }
        }
        // If the security question dropdown is interactable, verify the answer.
        else
        {
            // If the answer is correct, display the stored password.
            if (answer == storedSecurityAnswer)
            {
                errorMessage.text = $"Your password is: {storedPassword}";
            }
            // If the answer is incorrect, display an error message.
            else
            {
                errorMessage.text = "Incorrect answer.";
            }
        }
    }

    // Method to search for the user's email in the stored user data file.
    private bool SearchEmail(string email)
    {
        // Path to the user data file.
        string path = Application.persistentDataPath + "/userdata.txt";
        // Checking if the file exists.
        if (File.Exists(path))
        {
            // Reading all lines from the file.
            string[] lines = File.ReadAllLines(path);
            // Iterating through the lines to find the email.
            for (int i = 0; i < lines.Length; i += 6)
            {
                // Checking if the line contains the email.
                if (lines[i].Contains(email))
                {
                    // Extracting the stored data if the email is found.
                    storedEmail = lines[i].Substring(lines[i].IndexOf(":") + 2);
                    storedPassword = lines[i + 2].Substring(lines[i + 2].IndexOf(":") + 2);
                    storedSecurityQuestion = lines[i + 3].Substring(lines[i + 3].IndexOf(":") + 2);
                    storedSecurityAnswer = lines[i + 4].Substring(lines[i + 4].IndexOf(":") + 2);
                    return true;
                }
            }
        }
        // Returning false if the email is not found.
        return false;
    }

    // Method to display the security question to the user.
    private void DisplaySecurityQuestion()
    {
        // Clearing any existing options in the dropdown.
        securityQuestionDropdown.ClearOptions();
        // Adding the stored security question to the dropdown options.
        securityQuestionDropdown.AddOptions(new List<string> { storedSecurityQuestion });
        // Setting the first option as selected.
        securityQuestionDropdown.value = 0;
        // Making the dropdown interactable.
        securityQuestionDropdown.interactable = true;
    }
}
