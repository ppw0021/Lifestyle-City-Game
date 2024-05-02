using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.SceneManagement;

public class SmoothCameraMovement : MonoBehaviour
{
    public Vector3 targetPosition; // The target position to move towards
    public Vector3 targetRotation; // The target rotation to end up with

    public Vector3 shopTargetPosition; // The target position to move towards
    public Vector3 shopTargetRotation; // The target rotation to end up with

    public Vector3 gameTargetPosition;
    public Vector3 gameTargetRotation;
    public float moveSpeed = 3f; // The speed of camera movement

    public GameObject shopWindow;
    public GameObject ShopMinigameTutUIGroup;
    public ShopWindowManager shopWindowManager;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToHome(true));
    }

    // Coroutine to smoothly move the camera to the target position and rotation
    IEnumerator MoveToTarget(Vector3 newTargetPosition, Vector3 newTargetRotation)
    {
        isMoving = true;
        
        while (Vector3.Distance(transform.position, newTargetPosition) > 0.05f || Quaternion.Angle(transform.rotation, Quaternion.Euler(newTargetRotation)) > 0.05f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, newTargetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newTargetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        isMoving = false;
    }

    public void ToggleShop()
    {
        if (isMoving)
        {
            return;
        }
        if (shopWindow.activeSelf)
        {
            shopWindow.SetActive(false);
            StartCoroutine(MoveToHome(true));
        }
        else
        {
            StartCoroutine(MoveToShop());
        }
    }

    public void PlacementCompleted()
    {
        if (isMoving)
        {
            return;
        }
        shopWindowManager.buttonStateInteractable(true, true, true);
        shopWindowManager.showCancelButton(false);
        
    }
    public void MoveCamToBuildMode()
    {
        if (isMoving)
        {
            return;
        }
        shopWindow.SetActive(false);
        shopWindowManager.showCancelButton(true);
        StartCoroutine(MoveToHome(false));
    }

    public void MoveCamBackToShop()
    {
        if (isMoving)
        {
            return;
        }
        StartCoroutine(MoveToShop());
    }

    public void MoveCamToMinigame()
    {
        if (isMoving)
        {
            return;
        }
        shopWindowManager.buttonStateInteractable(false, false, false);
        StartCoroutine(MoveToMinigame());
    }

    private IEnumerator MoveToHome(bool enableTriButtons)
    {
        
        isMoving = true;
        
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f || Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation)) > 0.05f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        if (enableTriButtons)
        {
            shopWindowManager.buttonStateInteractable(true, true, true);
            ShopMinigameTutUIGroup.SetActive(true);
        }
        isMoving = false;
    }

    private IEnumerator MoveToShop()
    {
        isMoving = true;
        shopWindowManager.buttonStateInteractable(true, false, false);
        shopWindowManager.showCancelButton(false);
        while (Vector3.Distance(transform.position, shopTargetPosition) > 0.05f || Quaternion.Angle(transform.rotation, Quaternion.Euler(shopTargetRotation)) > 0.05f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, shopTargetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(shopTargetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        isMoving = false;
        shopWindow.SetActive(true);
    }

    private IEnumerator MoveToMinigame()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, gameTargetPosition) > 0.05f || Quaternion.Angle(transform.rotation, Quaternion.Euler(gameTargetRotation)) > 0.05f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, gameTargetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gameTargetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        isMoving = false;
        SceneManager.LoadScene("MiniGame");
        
    }

    // Move the camera to the specified target position and rotation
    public void MoveCamera(Vector3 newTargetPosition, Vector3 newTargetRotation)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToTarget(newTargetPosition, newTargetRotation));
        }
    }

    // Method to set the target position and rotation
    public void SetTarget(Vector3 newPosition, Vector3 newRotation)
    {
        targetPosition = newPosition;
        targetRotation = newRotation;
        MoveCamera(targetPosition, targetRotation);
    }
}
