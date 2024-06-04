using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToBase : MonoBehaviour
{
    //private bool isMoving = false;
    public Vector3 targetPosition; // The target position to move towards
    public Vector3 targetRotation; // The target rotation to end up with
    public float moveSpeed = 3f; // The speed of camera movement
    public void moveToBase()
    {
        StartCoroutine(MoveToBasePriv());
    }

    private IEnumerator MoveToBasePriv()
    {
        //isMoving = true;
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f || Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation)) > 0.05f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
            InterfaceAPI.multiplayerViewStart = true;
            SceneManager.LoadScene("GridPlacementSystem");
        }
        //isMoving = false;
    }
}
