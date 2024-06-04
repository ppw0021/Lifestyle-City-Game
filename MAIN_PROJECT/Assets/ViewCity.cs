using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ViewCity : MonoBehaviour
{
    public Transform target; // The 3D model's transform
    public RectTransform uiElement; // The UI element's RectTransform
    public Canvas canvas; // The Canvas
    public Camera mainCamera; // The main camera
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI levelText; 
    public int user_id_arg;

    public void init(int user_id_arg)
    {
        this.user_id_arg = user_id_arg;
        foreach (User userCheck in InterfaceAPI.userList)
        {
            if (userCheck.user_id == user_id_arg)
            {
                usernameText.text = userCheck.username;
                levelText.text = userCheck.level.ToString();
            }
        }
    }

    
    public void onVisitClick()
    {
        //Debug.Log("Clicked: " + user_id_arg);
        foreach (User userCheck in InterfaceAPI.userList)
        {
            //Debug.Log("Current User id from for loop: " + userCheck.user_id);
            if (userCheck.user_id == user_id_arg)
            {
                //Debug.Log("Found");
                StartCoroutine(WaitForBaseLoad(userCheck.user_id));
            }
        }
    }

    private IEnumerator WaitForBaseLoad(int baseToLoad)
    {
        yield return StartCoroutine(InterfaceAPI.GetBase(baseToLoad));
        SceneManager.LoadScene("Visit");
    }
    void Update()
    {
        if (target != null && uiElement != null && mainCamera != null && canvas != null)
        {
            // Convert the target's position from world space to screen space
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

            // If the target is in front of the camera
            if (screenPos.z > 0)
            {
                // Convert screen position to Canvas local position
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform, screenPos, canvas.worldCamera, out localPoint);

                // Set the UI element's local position
                uiElement.localPosition = localPoint;
                uiElement.gameObject.SetActive(true);
            }
            else
            {
                // Optionally, hide the UI element if the target is behind the camera
                uiElement.gameObject.SetActive(false);
            }
        }
    }
}
