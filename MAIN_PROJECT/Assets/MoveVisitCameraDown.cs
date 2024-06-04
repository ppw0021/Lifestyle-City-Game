using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVisitCameraDown : MonoBehaviour
{
    public CloudAnimator cloudControl;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    bool isMoving = false;
    float moveSpeed = 7f;

    public void Start()
    {
        StartCoroutine(waitForCloudAnimation());
    }

    private IEnumerator waitForCloudAnimation()
    {
        cloudControl.closeClouds();
        yield return new WaitForSeconds(1);
        cloudControl.openClouds();
        yield return StartCoroutine(moveCameraToHome());

    }

    private IEnumerator moveCameraToHome()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, targetPosition) > 0.5f || Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation)) > 0.5f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            // Use Quaternion.Lerp to smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), moveSpeed * Time.deltaTime);
            
            yield return null;
        }

        isMoving = false;
    }
}
