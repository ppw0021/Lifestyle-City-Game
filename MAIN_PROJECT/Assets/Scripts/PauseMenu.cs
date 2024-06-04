using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

/* Remove comment when scenes are ready, 
for adding other buttons that can access other scenes */

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // Reference to the pause menu GameObject

    // Pause the game
    public void Pause()
    {
        pauseMenu.SetActive(true); // Activate the pause menu
        Time.timeScale = 0;         // Set time scale to 0 to pause the game
        Debug.Log("Game paused");   // Log message indicating game paused
    }

    // Resume the game
    public void Resume()
    {
        pauseMenu.SetActive(false); // Deactivate the pause menu
        Time.timeScale = 1;         // Set time scale back to 1 to resume the game
        Debug.Log("Game resumed");  // Log message indicating game resumed
    }

    // Exit the game or go back to the main menu
    public void Exit()
    {
        InterfaceAPI.buildingList.Clear();
        /* This method currently exits the application when pressed
        will be changed to something else (i.e. go back to the main menu etc.) */
        pauseMenu.SetActive(false);       // Deactivate the pause menu
        Time.timeScale = 1;                // Set time scale back to 1
        SceneManager.LoadScene("LoginScene"); // Load the login scene (replace with desired scene name)
        Debug.Log("Game closed");          // Log message indicating game closed
    }
}