using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class QuestSystem : MonoBehaviour
{
    // UI elements for displaying mission details
    public TextMeshProUGUI[] missionDetailTexts; // Text elements showing mission details
    public TextMeshProUGUI[] missionStatTexts; // Text elements showing mission stats
    public Button[] claimButtons; // Buttons for claiming rewards
    public Button[] shortcutButtons; // Shortcut buttons for completing missions
    public Image[] missionBars; // Progress bars for missions

    // List of missions and available mission types
    private List<Mission> missions = new List<Mission>(); // List to store generated missions
    private string[] missionTypes = { "Login one time", "Plant corn", "Plant eggplant", "Spin the slots", "Spend $50", "Gain 5 exp" }; // Types of missions available

    private void Start()
    {
        // Generate initial set of missions and update UI
        GenerateMissions();
        UpdateMissionUI();
    }

    private void Update()
    {
        // Regenerate missions randomly when the 'R' key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateMissions();
            UpdateMissionUI();
        }

        // Check for key presses to progress missions
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Button1Pressed(); // Call method for the first mission (Z key)
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Button2Pressed(); // Call method for the second mission (X key)
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Button3Pressed(); // Call method for the third mission (C key)
        }
    }

    // Generate a new set of missions
    private void GenerateMissions()
    {
        // Clear existing missions
        missions.Clear();

        // Add permanent mission
        missions.Add(new Mission("Login one time", 1, true));

        // Add random missions
        System.Random random = new System.Random(); // Random number generator
        HashSet<int> chosenMissions = new HashSet<int>(); // Set to track chosen missions

        for (int i = 0; i < missionStatTexts.Length - 1; i++)
        {
            int randomMissionIndex;
            do
            {
                randomMissionIndex = random.Next(1, missionTypes.Length);
            } while (chosenMissions.Contains(randomMissionIndex));

            chosenMissions.Add(randomMissionIndex);

            int randomTargetCount = random.Next(1, 4); // Random target count between 1 and 3
            missions.Add(new Mission(missionTypes[randomMissionIndex], randomTargetCount));
        }
    }

    // Update UI elements based on mission progress
    private void UpdateMissionUI()
    {
        // Update mission detail texts, stat texts, and mission bars
        for (int i = 0; i < missionDetailTexts.Length; i++)
        {
            if (i < missions.Count)
            {
                // Update mission details
                missionDetailTexts[i].text = missions[i].description + " " + missions[i].currentCount + "/" + missions[i].targetCount;
                missionStatTexts[i].text = missions[i].description + " " + missions[i].currentCount + "/" + missions[i].targetCount;
                float fillAmount = (float)missions[i].currentCount / missions[i].targetCount;
                missionBars[i].fillAmount = fillAmount;
            }
            else
            {
                // Clear UI elements if no more missions
                missionDetailTexts[i].text = "";
                missionStatTexts[i].text = "";
                missionBars[i].fillAmount = 0f;
            }
        }

        // Update claim buttons and shortcut buttons visibility
        for (int i = 0; i < claimButtons.Length; i++)
        {
            if (i < missions.Count)
            {
                claimButtons[i].gameObject.SetActive(missions[i].IsComplete()); // Show claim button if mission is complete
                shortcutButtons[i].gameObject.SetActive(!missions[i].IsComplete()); // Show shortcut button if mission is incomplete
            }
            else
            {
                // Hide buttons if no more missions
                claimButtons[i].gameObject.SetActive(false);
                shortcutButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // Method to handle claiming rewards
    public void ClaimReward(int index)
    {
        if (index >= 0 && index < missions.Count)
        {
            // Reset mission progress and update UI
            missions[index].currentCount = 0;
            if (missions[index].isPermanent)
            {
                GenerateMissions();
            }
            UpdateMissionUI();
        }
        else
        {
            // Log error if index is out of bounds
            Debug.LogError("Invalid mission index: " + index);
        }
    }

    // Method to handle button 1 (login mission)
    public void Button1Pressed()
    {
        if (missions.Count > 0 && !missions[0].IsComplete())
        {
            missions[0].IncrementCount(); // Increment login mission progress
            UpdateMissionUI();
        }
    }

    // Method to handle button 2 (second mission)
    public void Button2Pressed()
    {
        if (missions.Count > 1 && !missions[1].IsComplete())
        {
            missions[1].IncrementCount(); // Increment second mission progress
            UpdateMissionUI();
        }
    }

    // Method to handle button 3 (third mission)
    public void Button3Pressed()
    {
        if (missions.Count > 2 && !missions[2].IsComplete())
        {
            missions[2].IncrementCount(); // Increment third mission progress
            UpdateMissionUI();
        }
    }
}

// Class representing a mission
[System.Serializable]
public class Mission
{
    public string description; // Description of the mission
    public int currentCount; // Current progress of the mission
    public int targetCount; // Target progress needed to complete the mission
    public bool isPermanent; // Indicates if the mission is permanent

    // Constructor for the mission
    public Mission(string description, int targetCount, bool isPermanent = false)
    {
        this.description = description;
        this.targetCount = targetCount;
        this.isPermanent = isPermanent;
        this.currentCount = 0;
    }

    // Method to increment the mission progress
    public void IncrementCount()
    {
        currentCount++;
    }

    // Method to check if the mission is complete
    public bool IsComplete()
    {
        return currentCount >= targetCount;
    }
}
