using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Action")]
public class GameplayAction : ScriptableObject
{
    public int cost = 0;
    public int rate = 1;
}
