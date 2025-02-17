using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition; 
    public float lerpSpeed = 2f; 

    private Vector3 currentTarget; 
    private float currentMovementMultiplier = 1;

    private void Start()
    {
        currentTarget = startPosition;
    }

    private void Update()
    {
        // Move towards the current target position
        transform.position = Vector3.Lerp(transform.position, currentTarget, lerpSpeed * currentMovementMultiplier * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget) < 0.3f)
        {
            currentTarget = (currentTarget == startPosition) ? endPosition : startPosition;
        }
    }

    public void SetMovementMultipleir(float slowMovementMultiplier)
    {
        currentMovementMultiplier = slowMovementMultiplier;
        Debug.Log("Set movement multiplier for enemy to: "+ currentMovementMultiplier);
    }

    public void ResetMovementMultiplier()
    {
        currentMovementMultiplier = 1;
        Debug.Log("Set movement multiplier for enemy to: " + currentMovementMultiplier);
    }
}
