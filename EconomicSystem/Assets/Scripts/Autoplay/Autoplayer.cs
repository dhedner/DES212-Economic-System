using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Autoplayer : MonoBehaviour
{
    public bool autoplayActive = false;
    public bool fastMode = false;
    public PlayStyle playStyle = PlayStyle.Balanced;

    //public int actionDelay = 1000;
    private int actionDelay = 500;
    private DateTime lastActionTime;
    private GameplayController gameplayController;
    private DecisionMaker decisionMaker;

    public void Start()
    {
        gameplayController = GetComponentInParent<GameplayController>();
        decisionMaker = GetComponent<DecisionMaker>();
    }

    public void Update()
    {
        if (!autoplayActive)
        {
            return;
        }

        if (fastMode)
        {
            actionDelay = 50;
        }
        else
        {
            actionDelay = 500;
        }

        var now = DateTime.Now;
        if (now - lastActionTime > TimeSpan.FromMilliseconds(actionDelay))
        {
            lastActionTime = now;

            decisionMaker.playStyle = playStyle;

            var state = gameplayController.GameplayState;
            var action = decisionMaker.MakeDecision(state);
            gameplayController.TriggerAction(action);
        }
    }

    public void OnGameOver()
    {
        autoplayActive = false;
    }

    public void OnNextPhase()
    {
        if (autoplayActive)
        {
            if (fastMode)
            {
                SetFastMode();
            }
            else
            {
                autoplayActive = false;
            }
        }
    }

    public void SetActive()
    {
        if (!autoplayActive)
        {
            autoplayActive = true;
        }
        else
        {
            autoplayActive = false;
        }
    }

    public void SetFastMode()
    {
        if (!fastMode)
        {
            fastMode = true;
        }
        else
        {
            fastMode = false;
        }
    }

    public void SetPlayStyle(int style)
    {
        playStyle = (PlayStyle)style;
    }
}
