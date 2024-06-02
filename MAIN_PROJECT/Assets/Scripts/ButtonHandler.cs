using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject animalPrefab; // Reference to the animal prefab
    public Transform planeTransform; // Reference to the plane transform for positioning

    private GameObject spawnedAnimal; // Reference to the spawned animal

    public void AddAnimal()
    {
        Debug.Log("Add Animal button clicked");
        // Instantiate the animal prefab at a specified position on the plane
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)); // Adjust the y-value as needed
        spawnedAnimal = Instantiate(animalPrefab, planeTransform.position + spawnPosition, Quaternion.identity);
    }

    public void SetWalking(bool isWalking)
    {
        if (spawnedAnimal != null)
        {
            Animator animator = spawnedAnimal.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("isWalking", isWalking);
            }
        }
    }
}