using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using UnityEngine.SceneManagement;  
remove comment when scenes are ready, 
for adding other buttons that can access other scenes
*/

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    public void Exit()
    {
        /* this method currently exits the application when pressed
        will be changed to something else (i.e. go back to the main menu etc.) */
        Application.Quit();
    }
}
