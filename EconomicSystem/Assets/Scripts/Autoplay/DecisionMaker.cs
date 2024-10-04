using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayStyle
{
    Random,
    Novice,
    Balanced,
    Smart
}

public class DecisionMaker : MonoBehaviour
{
    public ActionTracker tracker;
    public PlayStyle playStyle
    {
        get { return _playStyle; }
        set
        {
            if (_playStyle != value)
            {
                tracker.trackerId = GetTrackerIdForPlayStyle(value);
                tracker.Reload();
            }

            _playStyle = value;
        }
    }

    private PlayStyle _playStyle = PlayStyle.Balanced;

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

    private string GetTrackerIdForPlayStyle(PlayStyle style)
    {
        switch (style)
        {
            case PlayStyle.Random:
                return "training-random";
            case PlayStyle.Novice:
                return "training-novice";
            case PlayStyle.Balanced:
                return "training-balanced";
            case PlayStyle.Smart:
                return "training-smart";
            default:
                throw new Exception($"Unknown play style {style}");
        }
    }
}
