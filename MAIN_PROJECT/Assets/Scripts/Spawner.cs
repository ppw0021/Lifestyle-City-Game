using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int spawnCost; // Cost of spawning the prefab

    // This function gets called when the button is clicked
    public void SpawnPrefab()
    {
        // Check if the user has enough coins
        int userCoins = InterfaceAPI.getCoins();
        if (userCoins >= spawnCost)
        {
            // Deduct coins from the user
            InterfaceAPI.setCoins(userCoins - spawnCost);

            // Instantiate the prefab
            GameObject spawnedObject = Instantiate(prefabToSpawn);

            // Set the parent of the instantiated prefab
            Canvas canvas = FindObjectOfType<Canvas>();
            spawnedObject.transform.SetParent(canvas.transform, false);

            // Ensure it follows the cursor immediately upon spawning
            spawnedObject.GetComponent<FollowCursor>().enabled = true;
        }
        else
        {
            Debug.Log("Insufficient coins to spawn the prefab!");
        }
    }
}
