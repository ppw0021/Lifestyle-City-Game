using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject animalSelectionPanel;
    public Button addAnimalButton;
    public Button backButton;
    public Button bearButton;
    public Button chickenButton;
    public Button dogButton;
    public Button penguinButton;
    public Button lionButton;
    public Button catButton;
    public GameObject bearPrefab;
    public GameObject chickenPrefab;
    public GameObject dogPrefab;
    public GameObject penguinPrefab;
    public GameObject lionPrefab;
    public GameObject catPrefab;
    public Transform planeTransform;

    void Start()
    {
        // Assigning click listeners to buttons
        addAnimalButton.onClick.AddListener(ShowAnimalSelection);
        backButton.onClick.AddListener(ShowMainPanel);

        bearButton.onClick.AddListener(() => AddAnimal(bearPrefab));
        chickenButton.onClick.AddListener(() => AddAnimal(chickenPrefab));
        dogButton.onClick.AddListener(() => AddAnimal(dogPrefab));
        penguinButton.onClick.AddListener(() => AddAnimal(penguinPrefab));
        lionButton.onClick.AddListener(() => AddAnimal(lionPrefab));
        catButton.onClick.AddListener(() => AddAnimal(catPrefab));
    }

    void ShowAnimalSelection()
    {
        mainPanel.SetActive(false);
        animalSelectionPanel.SetActive(true);
    }

    void ShowMainPanel()
    {
        animalSelectionPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    void AddAnimal(GameObject animalPrefab)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f));
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Instantiate(animalPrefab, planeTransform.position + spawnPosition, randomRotation);
    }
}
