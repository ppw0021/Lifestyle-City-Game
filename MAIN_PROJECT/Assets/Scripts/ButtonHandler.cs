using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject animalPrefab; // Reference to the animal prefab
    public Transform planeTransform; // Reference to the plane transform for positioning

    public void AddAnimal()
    {
        Debug.Log("Add Animal button clicked");
        // Instantiate the animal prefab at a specified position on the plane
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)); // Adjust the y-value as needed
        Instantiate(animalPrefab, planeTransform.position + spawnPosition, Quaternion.identity);
    }
}
