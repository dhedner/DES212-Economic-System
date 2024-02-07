using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SelectionType
{
    PickLargest,
    PickSmallest,
    PickRandom
}

[Serializable]
public class AlignMutagenCostSet
{
    public ActionCost[] Costs;
}
public class AlignMutagen : CostChanger
{
    public GameObject AlignmentButton;
    public List<AlignMutagenCostSet> costs;
    public SelectionType selectionType = SelectionType.PickRandom;

    private CurrencyManager currencyManager;

    public void Start()
    {
        currencyManager = GetComponentInParent<CurrencyManager>();
        ApplyRandomMutation();
    }

    public override void TriggerCostChange()
    {
        ApplyRandomMutation();
    }

    public void ApplyRandomMutation()
    {
        var alignmentButton = AlignmentButton.GetComponent<ActionButton>();

        if (selectionType == SelectionType.PickRandom)
        {
            var random = new System.Random();
            var randomNumber = random.Next(0, costs.Count);

            if (randomNumber == 1)
            {
                alignmentButton.ActionButtonCosts = costs[0].Costs.ToList();
                Debug.Log("Red Mutagen Added");
            }
            else if (randomNumber == 2)
            {
                alignmentButton.ActionButtonCosts = costs[1].Costs.ToList();
                Debug.Log("Purple Mutagen Added");
            }
            else
            {
                alignmentButton.ActionButtonCosts = costs[2].Costs.ToList();
                Debug.Log("Green Mutagen Added");
            }
        }
        else
        {
            string selectedType = ApplyWeights();

            switch (selectedType)
            {
                case "Red Genome":
                    alignmentButton.ActionButtonCosts = costs[0].Costs.ToList();
                    break;
                case "Purple Genome":
                    alignmentButton.ActionButtonCosts = costs[1].Costs.ToList();
                    break;
                case "Green Genome":
                    alignmentButton.ActionButtonCosts = costs[2].Costs.ToList();
                    break;
                default:
                    break;
            }
        }

    }

    public string ApplyWeights()
    {
        var dict = new Dictionary<string, double>
        {
            { "Red Genome", 0.15 },
            { "Purple Genome", 0.15 },
            { "Green Genome", 0.15 }
        };

        var items = new List<Tuple<int, string>>
        {
            new Tuple<int, string>(currencyManager.GetCurrencyAmount(CurrencyType.RedGenome), "Red Genome"),
            new Tuple<int, string>(currencyManager.GetCurrencyAmount(CurrencyType.PurpleGenome), "Purple Genome"),
            new Tuple<int, string>(currencyManager.GetCurrencyAmount(CurrencyType.GreenGenome), "Green Genome"),
        };
        items.Sort();

        if (selectionType == SelectionType.PickLargest)
        {
            dict[items[2].Item2] = 0.7;
        }
        else
        {
            dict[items[0].Item2] = 0.7;
        }

        var picked = RouletteSelection(dict);
        return picked;
    }

    private string RouletteSelection(Dictionary<string, double> options)
    {
        var random = new System.Random();

        var r = random.NextDouble();
        double sum = 0.0;
        foreach (var option in options)
        {
            sum += option.Value;
            if (r < sum)
            {
                return option.Key;
            }
        }

        return options.Keys.First();
    }
}
