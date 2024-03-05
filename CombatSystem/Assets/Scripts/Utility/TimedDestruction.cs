using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    public float TimeAlive = 10.0f;

    void Update()
    {
        TimeAlive -= Time.deltaTime;

        if (TimeAlive <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
