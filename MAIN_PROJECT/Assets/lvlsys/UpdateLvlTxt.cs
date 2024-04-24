// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;


// public class UpdateLvlTxt : MonoBehaviour
// {
  
//  public TextMeshProUGUI lvlText;

//     private void Start()
//     {
//         UpdateLevelDisplay();
//     }

//     public void UpdateLevelDisplay()
//     {
//         // Ensure InterfaceAPI and currentUser are properly initialized
//         if (InterfaceAPI.currentUser != null)
//         {
//             int level = InterfaceAPI.currentUser.level;
//             lvlText.text = "Level: " + level.ToString();
//         }
//         else
//         {
//             Debug.LogError("Current user or InterfaceAPI is not set properly.");
//         }
//     }
// }
