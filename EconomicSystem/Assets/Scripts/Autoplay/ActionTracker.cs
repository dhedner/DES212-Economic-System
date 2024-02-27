using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class GameplayState : IComparable<GameplayState>, IEquatable<GameplayState>
{
    public GameplayButton EnabledButtons
    {
        get
        {
            GameplayButton maskOut = 0;
            //if (Level == 3)
            //{
            //    maskOut |= GameplayButton.Generate;
            //}
            return _enabledButtons & ~maskOut;
        }

        set
        {
            _enabledButtons = value;
        }
    }

    private GameplayButton _enabledButtons;

    public int Level;

    public override string ToString()
    {
        return $"{EnabledButtons}-{Level}";
    }

    public bool IsButtonEnabled(GameplayButton button)
    {
        return EnabledButtons.HasFlag(button);
    }

    // Implement IComparable if the dictionary is a SortedDictionary
    public int CompareTo(GameplayState other)
    {
        if (Level != other.Level)
        {
            return Level.CompareTo(other.Level);
        }

        return EnabledButtons.CompareTo(other.EnabledButtons);
    }

    public bool Equals(GameplayState obj)
    {
        return obj != null && EnabledButtons == obj.EnabledButtons && Level == obj.Level;
    }

    public override int GetHashCode()
    {
        return EnabledButtons.GetHashCode() ^ Level.GetHashCode();
    }

    static public GameplayState Parse(string str)
    {
        var parts = str.Split('-');
        var state = new GameplayState();
        state.EnabledButtons = (GameplayButton)Enum.Parse(typeof(GameplayButton), parts[0]);
        state.Level = int.Parse(parts[1]);
        return state;
    }
}

// Only for human players, not autoplay
public class ActionTracker : MonoBehaviour
{
    public bool save = false;
    public bool record = false;
    public bool useDate;
    public string trackerId;

    private string filename;
    private string date;
    private IDictionary<GameplayState, SortedDictionary<GameplayButton, double>> clickCounts;
    private GameplayController controller;

    public void Start()
    {
        clickCounts = new SortedDictionary<GameplayState, SortedDictionary<GameplayButton, double>>();
        controller = GetComponent<GameplayController>();

        date = DateTime.Now.ToString("yyyyMMdd-\\THHmmss\\Z");
        filename = $"action-tracker-{trackerId}";
        if (useDate)
        {
            filename += $"-{date}";
        }
        filename += ".csv";

        LoadFromFile();
    }

    public void OnCurrencyChanged()
    {
        SaveToFile();
    }

    public void NotifyAction(GameplayButton button)
    {
        if (!record)
        {
            return;
        }

        var state = controller.GameplayState;
        if (!clickCounts.ContainsKey(state))
        {
            clickCounts[state] = new SortedDictionary<GameplayButton, double>();
        }

        if (!clickCounts[state].ContainsKey(button))
        {
            clickCounts[state][button] = 0;
        }

        clickCounts[state][button]++;
    }

    public Dictionary<GameplayButton, double> GetActionDistribution(GameplayState state)
    {
        var distribution = new Dictionary<GameplayButton, double>();
        if (!clickCounts.ContainsKey(state))
        {
            Debug.LogWarning($"No click data for state={state}, using random actions");

            // Default uniform distribution
            var options = Enum.GetValues(typeof(GameplayButton));
            double availableActions = 0.0;
            foreach (GameplayButton option in options)
            {
                if (state.IsButtonEnabled(option))
                {
                    availableActions++;
                }
            }

            foreach (GameplayButton option in options)
            {
                if (state.IsButtonEnabled(option))
                {
                    distribution[option] = 1.0 / availableActions;
                }
                else
                {
                    distribution[option] = 0;
                }
            }

            return distribution;
        }

        double sum = 0;
        foreach (var option in clickCounts[state].Values)
        {
            sum += option;
        }

        foreach (var option in clickCounts[state].Keys)
        {
            distribution[option] = clickCounts[state][option] / sum;
        }

        return distribution;
    }

    public void SaveToFile()
    {
        if (!save)
        {
            return;
        }

        using StreamWriter dataStream = new StreamWriter(filename);

        dataStream.WriteLine("State;Action;Count");

        foreach (var state in clickCounts.Keys)
        {
            foreach (var action in clickCounts[state].Keys)
            {
                dataStream.WriteLine($"{state};{action};{clickCounts[state][action]}");
            }
        }
    }

    public void LoadFromFile()
    {
        if (!File.Exists(filename))
        {
            return;
        }

        using StreamReader dataStream = new StreamReader(filename);
        string line;

        // Always skip the header
        dataStream.ReadLine();

        while ((line = dataStream.ReadLine()) != null)
        {
            var parts = line.Split(';');
            var state = GameplayState.Parse(parts[0]);
            var action = (GameplayButton)Enum.Parse(typeof(GameplayButton), parts[1]);
            var count = double.Parse(parts[2]);

            if (!clickCounts.ContainsKey(state))
            {
                clickCounts[state] = new SortedDictionary<GameplayButton, double>();
            }

            clickCounts[state][action] = count;
        }
    }

    public double GetTotalClickCount()
    {
        double total = 0;
        foreach (var state in clickCounts.Keys)
        {
            foreach (var action in clickCounts[state].Keys)
            {
                total += clickCounts[state][action];
            }
        }

        return total;
    }

    public double GetButtonClickCount(GameplayButton button)
    {
        double total = 0;
        foreach (var state in clickCounts.Keys)
        {
            if (clickCounts[state].ContainsKey(button))
            {
                total += clickCounts[state][button];
            }
        }

        return total;
    }
}
