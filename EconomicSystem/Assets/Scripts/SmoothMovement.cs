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
    private float t = 0;

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

    float EaseInAndOut(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    public void MoveToDestination()
    {
        targetObject.position = Vector2.Lerp(targetObject.position, destinationPosition, EaseInAndOut(t));
        t += speed * Time.deltaTime;

        float distanceToDestination = Vector2.Distance(targetObject.position, destinationPosition);

        //if (distanceToDestination <= slowDownOffset)
        //{
        //    // Adjust the speed for slowing down
        //    speed = Mathf.Lerp(initialSpeed * slowDownRate, initialSpeed, distanceToDestination / slowDownOffset);
        //}
        //else
        //{
        //    // Reset the speed to its initial value if outside the slow down offset
        //    speed = initialSpeed;
        //}

        //// Move the object towards the destination at the current speed
        //targetObject.position = Vector2.MoveTowards(targetObject.position, destinationPosition, speed * Time.deltaTime);

        if (distanceToDestination <= 1.0f)
        {
            isMoving = false;
        }
    }

    public void StartMovement()
    {
        isMoving = true;
    }
}
