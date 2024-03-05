using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ThrowObject : MonoBehaviour
{
    public float StartingVelocityX = 0.0f;
    public float StartingVelocityY = 1.0f;
    public float RandomVariationX = 0.0f;
    public float RandomVariationY = 0.0f;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(
                       StartingVelocityX + Random.Range(-RandomVariationX, RandomVariationX),
                       StartingVelocityY + Random.Range(-RandomVariationY, RandomVariationY)
                       );
    }
}
