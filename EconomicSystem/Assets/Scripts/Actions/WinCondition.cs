using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour
{
    public void Execute()
    {
        var gameplayController = GetComponentInParent<GameplayController>();
        gameplayController.Win();
    }
}
