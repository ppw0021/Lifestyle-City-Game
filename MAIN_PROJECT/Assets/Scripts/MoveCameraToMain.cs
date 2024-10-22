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

    public Vector3 tutorialTargetPosition;
    public Vector3 tutorialTargetRotation;
    public Vector3 wheelTargetPosition;
    public Vector3 wheelTargetRotation;
    public Vector3 multiTargetPosition;
    public Vector3 multiTargetRotation;
    public Vector3 animalPosition;
    public Vector3 animalRotation;
    public float moveSpeed = 3f; // The speed of camera movement

    public GameObject shopWindow;
    public GameObject ShopMinigameTutUIGroup;
    public GameObject slotMachine;
    public ShopWindowManager shopWindowManager;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        if (InterfaceAPI.multiplayerViewStart)
        {
            InterfaceAPI.multiplayerViewStart = false;
            transform.position = multiTargetPosition;
            transform.rotation = Quaternion.Euler(multiTargetRotation);   
        }
        StartCoroutine(MoveToHome(true));

        foreach (int id in InterfaceAPI.useridList)
        {
            StartCoroutine(InterfaceAPI.GetAndAddUserToList(id));
        }
        
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
        //Debug.Log("Users loaded into memory: " + InterfaceAPI.userList.Count);
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

    public void ToggleSlotsWindow()
    {
        if (isMoving)
        {
            return;
        }
        if (slotMachine.activeSelf)
        {
            slotMachine.SetActive(false);
            StartCoroutine(MoveToHome(true));
        }
        else
        {
            MoveCamToWheel();
        }
        // Toggle the active state of the slotsWindow
        
    }

    public void PlacementCompleted()
    {
        if (isMoving)
        {
            return;
        }
        shopWindowManager.buttonStateInteractable(true, true, true, true, true, true);
        shopWindowManager.showCancelButton(false);
        
    }
    public void MoveCamToBuildMode()
    {
        if (isMoving)
        {
            return;
        }
        shopWindow.SetActive(false);
        shopWindowManager.buttonStateInteractable(false, false, false, false, false, false);
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
        shopWindowManager.buttonStateInteractable(false, false, false, false, false, false);
        StartCoroutine(MoveToMinigame());
    }

    public void MoveCamToTutorial()
    {
        if (isMoving)
        {
            return;
        }
        shopWindowManager.buttonStateInteractable(false, false, false, false, false, false);
        StartCoroutine(MoveToTutorial());
    }

    public void MoveCamToWheel()
    {
        if (isMoving)
        {
            return;
        }
        shopWindowManager.buttonStateInteractable(false, false, false, true, false, false);
        StartCoroutine(MoveToWheel());
    }

    public void MoveCamToMultiplayer()
    {
        if (isMoving)
        {
            return;
        }
        shopWindowManager.buttonStateInteractable(false, false, false, false, false, false);
        StartCoroutine(MoveToMulti());
    }

    public void MoveCamToAnimal()
    {
        if (isMoving)
        {
            return;
        }
        shopWindowManager.buttonStateInteractable(false, false, false, false, false, false);
        StartCoroutine(MoveToAnimal());

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
            shopWindowManager.buttonStateInteractable(true, true, true, true, true, true);
            ShopMinigameTutUIGroup.SetActive(true);
        }
        isMoving = false;
    }

    private IEnumerator MoveToShop()
    {
        isMoving = true;
        shopWindowManager.buttonStateInteractable(true, false, false, false, false, false);
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
        while (Vector3.Distance(transform.position, gameTargetPosition) > 0.5f || Quaternion.Angle(transform.rotation, Quaternion.Euler(gameTargetRotation)) > 0.5f)
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

    private IEnumerator MoveToTutorial()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, tutorialTargetPosition) > 0.25f || Quaternion.Angle(transform.rotation, Quaternion.Euler(tutorialTargetRotation)) > 0.25f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, tutorialTargetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(tutorialTargetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        isMoving = false;
        SceneManager.LoadScene("TutorialSection");
    }
    

    private IEnumerator MoveToWheel()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, wheelTargetPosition) > 0.05f || Quaternion.Angle(transform.rotation, Quaternion.Euler(wheelTargetRotation)) > 0.05f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, wheelTargetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(wheelTargetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        Quaternion rotation = Quaternion.Euler(wheelTargetRotation);
        transform.position = wheelTargetPosition;
        transform.rotation = rotation;
        isMoving = false;
        slotMachine.SetActive(true);
    }
    // Move the camera to the specified target position and rotation
    public void MoveCamera(Vector3 newTargetPosition, Vector3 newTargetRotation)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToTarget(newTargetPosition, newTargetRotation));
        }
    }

    private IEnumerator MoveToMulti()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, multiTargetPosition) > 50f || Quaternion.Angle(transform.rotation, Quaternion.Euler(multiTargetRotation)) > 50f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, multiTargetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(multiTargetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        Quaternion rotation = Quaternion.Euler(multiTargetRotation);
        transform.position = multiTargetPosition;
        transform.rotation = rotation;
        isMoving = false;
        //InterfaceAPI.loadUsernameList();
        SceneManager.LoadScene("MultiplayerMap");
        //slotMachine.SetActive(true);
    }

    private IEnumerator MoveToAnimal()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, animalPosition) > 20f || Quaternion.Angle(transform.rotation, Quaternion.Euler(animalRotation)) > 20f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, animalPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(animalRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        Quaternion rotation = Quaternion.Euler(animalRotation);
        transform.position = animalPosition;
        transform.rotation = rotation;
        isMoving = false;
        //InterfaceAPI.loadUsernameList();
        SceneManager.LoadScene("Animal");
        //slotMachine.SetActive(true);
    }

    // Method to set the target position and rotation
    public void SetTarget(Vector3 newPosition, Vector3 newRotation)
    {
        targetPosition = newPosition;
        targetRotation = newRotation;
        MoveCamera(targetPosition, targetRotation);
    }
}
