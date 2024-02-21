using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SmoothMovement : MonoBehaviour
{
    public Transform targetObject;
    //public Vector2 initialPosition;
    public Vector2 destinationPosition;
    public float speed;
    public float slowDownOffset;
    public float slowDownRate;
    public bool easeIn;
    public bool easeOut;

    private float initialSpeed;
    private bool isMoving = false;

    void Start()
    {
        // Set the initial offscreen position
        //initialPosition = transform.position;
        initialSpeed = speed;
    }

    void Update()
    {
        if (!isMoving)
        {
            return;
        }

        MoveToDestination();
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

        if (distanceToDestination <= 0.01f)
        {
            isMoving = false;
        }
    }

    public void StartMovement()
    {
        isMoving = true;
    }
}
