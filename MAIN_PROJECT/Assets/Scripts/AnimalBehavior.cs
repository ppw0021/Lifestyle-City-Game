using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    public float minWalkDuration = 2f;
    public float maxWalkDuration = 5f;
    public float minIdleDuration = 1f;
    public float maxIdleDuration = 3f;
    public float walkSpeed = 2f;

    private Animator animator;
    private bool isWalking = false;
    private float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetRandomTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
            SetRandomTimer();
        }

        if (isWalking)
        {
            // Move the animal forward
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

            // Optionally, add random turning
            float randomTurn = Random.Range(-1f, 1f);
            transform.Rotate(0, randomTurn, 0);
        }
    }

    void SetRandomTimer()
    {
        if (isWalking)
        {
            timer = Random.Range(minWalkDuration, maxWalkDuration);
        }
        else
        {
            timer = Random.Range(minIdleDuration, maxIdleDuration);
        }
    }
}
