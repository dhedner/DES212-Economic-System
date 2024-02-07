using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

// Only for human players, not autoplay
public class ActionTracker : MonoBehaviour
{
    public Autoplayer autoplayer;
    public string filename;
    public bool useLearnedData;

    private Dictionary<GameplayButton, Dictionary<GameplayButton, double>> clickCounts;
    private GameplayController controller;

    public void Start()
    {
        clickCounts = new Dictionary<GameplayButton, Dictionary<GameplayButton, double>>();
        controller = GetComponent<GameplayController>();

        LoadFromFile();
    }

    public void OnCurrencyChanged()
    {
        SaveToFile();
    }

    public void NotifyAction(GameplayButton button)
    {
        if (autoplayer.autoplayActive)
        {
            return;
        }
        var state = controller.AvailableActions;
        if (!clickCounts.ContainsKey(state))
        {
            clickCounts[state] = new Dictionary<GameplayButton, double>();
        }

        if (!clickCounts[state].ContainsKey(button))
        {
            clickCounts[state][button] = 0;
        }

        clickCounts[state][button]++;
    }

    public Dictionary<GameplayButton, double> GetActionDistribution(GameplayButton state)
    {
        var distribution = new Dictionary<GameplayButton, double>();
        if (!useLearnedData || !clickCounts.ContainsKey(state))
        {
            // Default uniform distribution
            var options = Enum.GetValues(typeof(GameplayButton));
            double availableActions = 0.0;
            foreach (GameplayButton option in options)
            {
                if (state.HasFlag(option))
                {
                    availableActions++;
                }
            }

            foreach (GameplayButton option in options)
            {
                if (state.HasFlag(option))
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
            var state = (GameplayButton)Enum.Parse(typeof(GameplayButton), parts[0]);
            var action = (GameplayButton)Enum.Parse(typeof(GameplayButton), parts[1]);
            var count = double.Parse(parts[2]);

            if (!clickCounts.ContainsKey(state))
            {
                clickCounts[state] = new Dictionary<GameplayButton, double>();
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
