using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public void Execute()
    {
        var gameplayController = GetComponentInParent<GameplayController>();
        gameplayController.Win();
    }
}
