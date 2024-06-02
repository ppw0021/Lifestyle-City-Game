using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPosition;

    void Start()
    {
        SetNewRandomPosition();
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewRandomPosition();
        }
    }

    void SetNewRandomPosition()
    {
        targetPosition = new Vector3(
            Random.Range(-50f, 50f), // Adjust based on your plane size
            transform.position.y,
            Random.Range(-50f, 50f)
        );
    }
}