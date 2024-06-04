using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisitSmoothCamera : MonoBehaviour
{
    public Vector3 multiTargetPosition;
    public Vector3 multiTargetRotation;
    public float moveSpeed = 3f; // The speed of camera movement

    public bool isMoving = false;

    void Start()
    {
    }

    public void MoveCamToMultiplayer()
    {
        if (isMoving)
        {
            return;
        }
        StartCoroutine(InterfaceAPI.GetBase(InterfaceAPI.currentUser.user_id));
        StartCoroutine(MoveToMulti());
    }

    private IEnumerator MoveToMulti()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, multiTargetPosition) > 0.5f || Quaternion.Angle(transform.rotation, Quaternion.Euler(multiTargetRotation)) > 0.5f)
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
        SceneManager.LoadScene("MultiplayerMap");
    }

}
