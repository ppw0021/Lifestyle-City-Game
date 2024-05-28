using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IslandLoaderScript : MonoBehaviour
{
    public GameObject islandPrefab;
    // Start is called before the first frame update
    private List<int> possibleXPosList = new List<int>();
    private List<int> possibleZPosList = new List<int>();
    void Start()
    {
        for (int i = -10; i <= 10; i++)
        {
            if (i == -1 || i == 0 || i == 1)
            {
                //Nothing
            }
            else
            {
                possibleXPosList.Add(i*50);
            }
        }

        for (int i = -5; i <= 5; i++)
        {
            if (i == -1 || i == 0 || i == 1)
            {
                //Nothing
            }
            else
            {
                possibleZPosList.Add(i*25);
            }
        }

        Shuffle(possibleXPosList);
        Shuffle(possibleZPosList);

        SpawnIslands();
    }

    void SpawnIslands()
    {
        int startingIndex = 0;
        foreach (Base baseInstance in InterfaceAPI.baseList)
        {
            Vector3 spawnPosition = new Vector3(possibleXPosList[startingIndex], 0, possibleZPosList[startingIndex]);
            startingIndex++;
            GameObject spawnedPrefab = Instantiate (islandPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
