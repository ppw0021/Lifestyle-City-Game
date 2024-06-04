using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisitSmoothCamera : MonoBehaviour
{
    public Vector3 multiTargetPosition;
    public Vector3 multiTargetRotation;

    public Vector3 visitPosition;
    public Vector3 visitRotation;
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
        StartCoroutine(WaitForCamera(true));
        StartCoroutine(MoveToMulti());
    }

    public void MoveCamToVisit()
    {
        if (isMoving)
        {
            return;
        }
        StartCoroutine(WaitForCamera(false));
        StartCoroutine(MoveToVisit());
    }

    private IEnumerator WaitForCamera(bool toMulti)
    {

        yield return StartCoroutine(reloadHomeBase());
        if (toMulti)
        {
            SceneManager.LoadScene("MultiplayerMap");
        }
        else
        {
            SceneManager.LoadScene("Visit");
        }
    }

    private IEnumerator reloadHomeBase()
    {
        yield return StartCoroutine(InterfaceAPI.GetBase(InterfaceAPI.currentUser.user_id));
    }

    private IEnumerator MoveToMulti()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, multiTargetPosition) > 20f || Quaternion.Angle(transform.rotation, Quaternion.Euler(multiTargetRotation)) > 20f)
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

        //slotMachine.SetActive(true);
    }
    private IEnumerator MoveToVisit()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, multiTargetPosition) > 20f || Quaternion.Angle(transform.rotation, Quaternion.Euler(multiTargetRotation)) > 20f)
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



        //slotMachine.SetActive(true);
    }
}
