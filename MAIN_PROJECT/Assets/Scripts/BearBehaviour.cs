using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBehavior : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float idleDuration = 2f;
    public float walkDuration = 3f;

    private Animator animator;
    private bool isWalking = false;
    private float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = idleDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
            timer = isWalking ? walkDuration : idleDuration;
        }

        if (isWalking)
        {
            // Move the bear forward
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

            // Optionally, add random turning
            float randomTurn = Random.Range(-1f, 1f);
            transform.Rotate(0, randomTurn, 0);
        }
    }
}
