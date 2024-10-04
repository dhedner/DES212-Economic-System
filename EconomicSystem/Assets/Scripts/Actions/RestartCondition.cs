using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCondition : MonoBehaviour
{
    public void Execute()
    {
        var gameplayController = GetComponentInParent<GameplayController>();
        gameplayController.RestartGame();
    }
}
