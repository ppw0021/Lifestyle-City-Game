using UnityEngine;
using TMPro;
using System.Collections;

public class ResetPassword : MonoBehaviour
{
    public TMP_InputField newUsername;
    public TMP_InputField answer;
    public TMP_InputField newPassword;
    public TMP_InputField verifyPassword;
    public TextMeshProUGUI error;
    // Start is called before the first frame update
    public void onClick()
    {
        Debug.Log(newUsername.text + " " + answer.text + " " + newPassword.text + " " + verifyPassword.text);

        if (newUsername.text == "")
        {
            //Fucked up
            error.text = "Empty username";
        }
        else if (newPassword.text == "")
        {
            error.text = "Empty password";
        }
        else if (newPassword.text != verifyPassword.text)
        {
            error.text = "Passwords do not match";
        }
        else
        {
            error.text = ""; 
            StartCoroutine(Login());
        }
    }

    private IEnumerator Login()
    {
        Debug.Log("Reached LOGIN");
        yield return StartCoroutine(InterfaceAPI.UpdatePassword("password", newPassword.text, newUsername.text));
        yield return StartCoroutine(InterfaceAPI.LoginPost(newUsername.text, newPassword.text));
        error.text = "records do not match"; 
    }
}