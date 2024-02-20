using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SmoothMovement : MonoBehaviour
{
    public Transform targetObject;
    public Vector2 initialPosition;
    public Vector2 destinationPosition;
    public float speed;
    public float slowDownOffset;
    public float slowDownRate;

    private Phase phase;
    private float initialSpeed;

    void Start()
    {
        // Set the initial offscreen position
        transform.position = initialPosition;
        initialSpeed = speed;
        phase = GetComponentInParent<Phase>();
    }

    void Update()
    {
        if (phase.isActive)
        {
            MoveToDestination();
        }
    }

    public void MoveToDestination()
    {
        float distanceToDestination = Vector2.Distance(targetObject.position, destinationPosition);

        if (distanceToDestination <= slowDownOffset)
        {
            // Adjust the speed for slowing down
            speed = Mathf.Lerp(initialSpeed * slowDownRate, initialSpeed, distanceToDestination / slowDownOffset);
        }
        else
        {
            // Reset the speed to its initial value if outside the slow down offset
            speed = initialSpeed;
        }

        // Move the object towards the destination at the current speed
        targetObject.position = Vector2.MoveTowards(targetObject.position, destinationPosition, speed * Time.deltaTime);
    }
}
