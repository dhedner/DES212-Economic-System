using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DecisionMaker : MonoBehaviour
{
    private ActionTracker tracker;

    public void Start()
    {
        tracker = GetComponentInParent<ActionTracker>();
    }

    public GameplayButton MakeDecision(GameplayState state)
    {
        var random = new System.Random();

       var distribution = tracker.GetActionDistribution(state);

        // Roulette wheel selection
        var r = random.NextDouble();
        double sum = 0.0;
        foreach (var option in distribution)
        {
            sum += option.Value;
            if (r < sum)
            {
                return option.Key;
            }
        }

        throw new Exception("No decision made");
    }
}
