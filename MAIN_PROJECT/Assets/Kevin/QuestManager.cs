using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/*
note from Kevin: 
use for test:
    press 'r' for random mission, which should be set after every login (which i did not do....)
    press 'z', 'x', 'c' to increment mission progress for the three missions

below is the api thing i did so less pain.... but not sure if it's correct or not
also there is a delete method to delete r,z,x,c buttons
this plant plant and spin the slots missions to increment,
 which im kinda having diffculty as not sure how that works
cause didnt really see if the farm manager script code.... so was really confused on how to do it


sorry for the inconvenience
Kevin

to regenerate mission i think you can:

go ->script LoginManager.cs
public class LoginManager : MonoBehaviour
{
    [Header("UI")]
    public Text messageText;
    public TMP_InputField usernameEmail;
    public TMP_InputField passwordInput;
    public QuestSystem questSystem; // Reference to the QuestSystem script

    public void LoginButtonPressed() 
    {
        StartCoroutine(InterfaceAPI.LoginPost(usernameEmail.text, passwordInput.text));
        // Call the method to regenerate missions in the QuestSystem script
        questSystem.GenerateMissions();
        questSystem.UpdateMissionUI(); // Update the UI after regenerating missions
    }
}
and
go -> script QuestSystem.cs

public class QuestSystem : MonoBehaviour
{

      private void Start()
    {
        // Subscribe to the login event raised by the LoginManager script
        LoginManager.OnLogin += OnLoginButtonPressed;
    }

    private void OnLoginButtonPressed()
    {
        // Regenerate missions and update UI when the login button is pressed
        GenerateMissions();
        UpdateMissionUI();
    }
 private void OnDestroy()
    {
        // Unsubscribe from the login event to prevent memory leaks
        LoginManager.OnLogin -= OnLoginButtonPressed;
    }
}
someting like above^

*/

public class QuestSystem : MonoBehaviour
{
    // UI elements for mission details
    public TextMeshProUGUI[] missionDetailTexts;
    // UI elements for mission stats
    public TextMeshProUGUI[] missionStatTexts;
    // Buttons to claim rewards
    public Button[] claimButtons;
    // Buttons to shortcut to missions
    public Button[] shortcutButtons;
    // Progress bars for missions
    public Image[] missionBars;

    // List to store generated missions
    private List<Mission> missions = new List<Mission>();
    // Array of mission types
    private string[] missionTypes = { "Login one time", "Plant corn", "Plant eggplant", "Spin the slots", "Spend $50", "Gain 5 exp" };

    private void Start()
    {
        // Generate missions and update UI on start
        GenerateMissions();
        UpdateMissionUI();

        // Assign listeners to shortcut buttons
        for (int i = 0; i < shortcutButtons.Length; i++)
        {
            int index = i; // Local copy of i to avoid closure issues
            shortcutButtons[i].onClick.AddListener(() => LoadSceneForMission(index));
        }
    }

    private void Update()
    {
        // Regenerate missions and update UI on 'R' key press
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateMissions();
            UpdateMissionUI();
        }

        // Increment mission progress based on key presses
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Button1Pressed();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Button2Pressed();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Button3Pressed();
        }
    }

    // Method to generate missions
    private void GenerateMissions()
    {
        // Clear existing missions
        missions.Clear();
        // Add permanent mission (login)
        missions.Add(new Mission("Login one time", 1, true));

        // Randomly select and add non-permanent missions
        System.Random random = new System.Random();
        HashSet<int> chosenMissions = new HashSet<int>();

        for (int i = 0; i < missionStatTexts.Length - 1; i++)
        {
            int randomMissionIndex;
            do
            {
                randomMissionIndex = random.Next(1, missionTypes.Length);
            } while (chosenMissions.Contains(randomMissionIndex));

            chosenMissions.Add(randomMissionIndex);

            int randomTargetCount = random.Next(1, 4);
            missions.Add(new Mission(missionTypes[randomMissionIndex], randomTargetCount));
        }
    }

    // Method to update mission UI
    private void UpdateMissionUI()
    {
        for (int i = 0; i < missionDetailTexts.Length; i++)
        {
            if (i < missions.Count)
            {
                // Update mission details, stats, and progress bars
                missionDetailTexts[i].text = missions[i].description + " " + missions[i].currentCount + "/" + missions[i].targetCount;
                missionStatTexts[i].text = missions[i].description + " " + missions[i].currentCount + "/" + missions[i].targetCount;
                float fillAmount = (float)missions[i].currentCount / missions[i].targetCount;
                missionBars[i].fillAmount = fillAmount;
            }
            else
            {
                // Clear UI elements if no missions
                missionDetailTexts[i].text = "";
                missionStatTexts[i].text = "";
                missionBars[i].fillAmount = 0f;
            }
        }

        // Show/hide claim and shortcut buttons based on mission completion
        for (int i = 0; i < claimButtons.Length; i++)
        {
            if (i < missions.Count)
            {
                claimButtons[i].gameObject.SetActive(missions[i].IsComplete());

                if (missions[i].description == "Login one time" || missions[i].description == "Gain 5 exp")
                {
                    shortcutButtons[i].gameObject.SetActive(false);
                }
                else
                {
                    shortcutButtons[i].gameObject.SetActive(!missions[i].IsComplete());
                }
            }
            else
            {
                claimButtons[i].gameObject.SetActive(false);
                shortcutButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // Method to claim reward for a mission
    public void ClaimReward(int index)
    {
        if (index >= 0 && index < missions.Count)
        {
            // Reset mission progress and regenerate if permanent
            missions[index].currentCount = 0;
            if (missions[index].isPermanent)
            {
                GenerateMissions();
            }
            UpdateMissionUI();
        }
        else
        {
            Debug.LogError("Invalid mission index: " + index);
        }
    }

    // Methods to increment mission progress for shortcut buttons
    public void Button1Pressed()
    {
        if (missions.Count > 0 && !missions[0].IsComplete())
        {
            missions[0].IncrementCount();
            UpdateMissionUI();
        }
    }

    public void Button2Pressed()
    {
        if (missions.Count > 1 && !missions[1].IsComplete())
        {
            missions[1].IncrementCount();
            UpdateMissionUI();
        }
    }

    public void Button3Pressed()
    {
        if (missions.Count > 2 && !missions[2].IsComplete())
        {
            missions[2].IncrementCount();
            UpdateMissionUI();
        }
    }

    // Method to load scene associated with a mission
    public void LoadSceneForMission(int index)
    {
        Debug.Log("LoadSceneForMission called with index: " + index);

        if (index >= 0 && index < missions.Count)
        {
            string sceneName = "";

            switch (missions[index].description)
            {
                case "Plant corn":
                case "Plant eggplant":
                case "Spend $50":
                    sceneName = "MiniGame";
                    break;
                case "Spin the slots":
                    sceneName = "GridPlacementSystem";
                    break;
                default:
                    Debug.LogWarning("No scene associated with mission: " + missions[index].description);
                    return;
            }

            if (!string.IsNullOrEmpty(sceneName))
            {
                Debug.Log("Loading scene: " + sceneName);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogWarning("Scene name is empty for mission: " + missions[index].description);
            }
        }
        else
        {
            Debug.LogError("Invalid mission index: " + index);
        }
    }

    // Class to represent a mission
    [System.Serializable]
    public class Mission
    {
        public string description;  // Description of the mission
        public int currentCount;    // Current progress of the mission
        public int targetCount;     // Target progress required to complete the mission
        public bool isPermanent;    // Flag indicating if the mission is permanent

        // Constructor to initialize mission properties
        public Mission(string description, int targetCount, bool isPermanent = false)
        {
            this.description = description;
            this.targetCount = targetCount;
            this.isPermanent = isPermanent;
            this.currentCount = 0;
        }
        // Method to increment mission progress
        public void IncrementCount()
        {
            currentCount++; // Increment current progress
        }

        // Method to check if the mission is complete
        public bool IsComplete()
        {
            return currentCount >= targetCount; // Return true if current progress meets or exceeds target
        }

    }
}



/*
update method is the one to delete the r,z,x,c buttons and add the new one to claim rewards


*/


/**/

/* api     Databse part @Dec!!!!!!*/
// // Inside UpdateCoinsText class
// public void GainCoins(int amount)
// {
//     InterfaceAPI.GainCoins(amount); // Call the method from InterfaceAPI to gain coins
// }

// // Inside QuestSystem class
// public void ClaimReward(int index)
// {
//     if (index >= 0 && index < missions.Count)
//     {
//         // Check if the mission is complete
//         if (missions[index].IsComplete())
//         {
//             // Increment count for the mission
//             missions[index].currentCount = 0;

//             // If the mission is permanent, generate new missions
//             if (missions[index].isPermanent)
//             {
//                 GenerateMissions();
//             }

//             // Update the UI
//             UpdateMissionUI();

//             // Gain exp and coins
//             if (missions[index].description == "Login one time" || missions[index].description == "Gain 5 exp")
//             {
//                 // Assuming 5 exp and 10 coins as rewards, adjust the values as per your game design
//                 InterfaceAPI.GainExp(5);
//                 UpdateCoinsText updateCoinsText = FindObjectOfType<UpdateCoinsText>(); // Find the UpdateCoinsText component
//                 if (updateCoinsText != null)
//                 {
//                     updateCoinsText.GainCoins(10); // Gain 10 coins
//                 }
//                 else
//                 {
//                     Debug.LogError("UpdateCoinsText component not found.");
//                 }
//             }
//         }
//         else
//         {
//             Debug.LogError("Mission is not complete yet.");
//         }
//     }
//     else
//     {
//         Debug.LogError("Invalid mission index: " + index);
//     }
// }


