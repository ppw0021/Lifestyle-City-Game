using UnityEngine;
using System.Collections;

public class SmoothCameraMovement : MonoBehaviour
{
    public Vector3 targetPosition; // The target position to move towards
    public Vector3 targetRotation; // The target rotation to end up with

    public Vector3 shopTargetPosition; // The target position to move towards
    public Vector3 shopTargetRotation; // The target rotation to end up with
    public float moveSpeed = 3f; // The speed of camera movement

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        MoveToHome();
    }

    public void MoveToHome()
    {
        if (targetPosition != null)
        {
            MoveCamera(targetPosition, targetRotation);
        }
        else
        {
            Debug.LogWarning("No target specified for camera movement.");
        }
    }

    public void MoveToShop()
    {
        if (shopTargetPosition != null)
        {
            MoveCamera(shopTargetPosition, shopTargetRotation);
        }
        else
        {
            Debug.LogWarning("No target specified for camera movement.");
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
        
        // Ensure the camera is exactly at the target position and rotation
        //transform.position = newTargetPosition;
        //transform.rotation = Quaternion.Euler(newTargetRotation);
        isMoving = false;
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
