using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPhase : MonoBehaviour
{
    private Phase winPhase;
    private GameplayController gameplayController;

    void Start()
    {
        winPhase = GetComponentInParent<Phase>();
        gameplayController = GetComponentInParent<GameplayController>();

        winPhase.EnableCondition = IsEnabledPhase;
    }

    private bool IsEnabledPhase(Phase winPhase)
    {
        return gameplayController.phases[4].enabled && winPhase.isActive;
    }
}
