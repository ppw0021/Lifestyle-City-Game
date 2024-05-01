using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHome : MonoBehaviour
{
    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene(InterfaceAPI.getMainMenuSceneName());
    }
}
