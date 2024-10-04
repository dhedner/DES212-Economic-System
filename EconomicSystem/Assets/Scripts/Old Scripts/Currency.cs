using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "Currency")]
public class Currency : ScriptableObject
{
    public int amount = 0;

    public void ResetValues()
    {
        amount = 0;
    }
}
