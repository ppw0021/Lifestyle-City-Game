using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CloudAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject leftClouds;
    public GameObject rightClouds;
    public bool isMoving = false;
    public Vector3 RclosedPosition;
    public Vector3 RopenPosition;
    public Vector3 LclosedPosition;
    public Vector3 LopenPosition;
    public float moveSpeed = 7f;
    public void openClouds()
    {
        StartCoroutine(Open());
    }
    public void closeClouds()
    {
        StartCoroutine(Close());
    }

    public void closeCloudsReturnHome()
    {
        StartCoroutine(CloseHome());
    }

    public void Start()
    {
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        isMoving = true;
        while (Vector3.Distance(leftClouds.transform.localPosition, LopenPosition) > 1f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            leftClouds.transform.localPosition = Vector3.Lerp(leftClouds.transform.localPosition, LopenPosition, moveSpeed * Time.deltaTime);
            rightClouds.transform.localPosition = Vector3.Lerp(rightClouds.transform.localPosition, RopenPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        //leftClouds.transform.localPosition = LopenPosition;
        //rightClouds.transform.localPosition = RopenPosition;
        isMoving = false;
    }

    private IEnumerator Close()
    {
        isMoving = true;
        while (Vector3.Distance(leftClouds.transform.localPosition, LclosedPosition) > 1f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            leftClouds.transform.localPosition = Vector3.Lerp(leftClouds.transform.localPosition, LclosedPosition, moveSpeed * Time.deltaTime);
            rightClouds.transform.localPosition = Vector3.Lerp(rightClouds.transform.localPosition, RclosedPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        //leftClouds.transform.localPosition = LclosedPosition;
        //rightClouds.transform.localPosition = RclosedPosition;
        isMoving = false;
    }

    private IEnumerator CloseHome()
    {
        isMoving = true;
        while (Vector3.Distance(leftClouds.transform.localPosition, LclosedPosition) > 1f)
        {
            // Use Vector3.Lerp to smoothly move the camera towards the target position
            leftClouds.transform.localPosition = Vector3.Lerp(leftClouds.transform.localPosition, LclosedPosition, moveSpeed * Time.deltaTime);
            rightClouds.transform.localPosition = Vector3.Lerp(rightClouds.transform.localPosition, RclosedPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        //leftClouds.transform.localPosition = LclosedPosition;
        //rightClouds.transform.localPosition = RclosedPosition;
        isMoving = false;
        SceneManager.LoadScene("GridPlacementSystem");
    }
}
