using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
/*remove comment when scenes are ready, 
for adding other buttons that can access other scenes
*/

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; //
        Debug.Log("Game paused");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Game resumed");
    }

    public void Exit()
    {
        /* this method currently exits the application when pressed
        will be changed to something else (i.e. go back to the main menu etc.) */
        pauseMenu.SetActive(false);    
        //Application.Quit();
        SceneManager.LoadScene("LoginScene"); //now loads the login scene
        Debug.Log("Game closed");
    }
}
