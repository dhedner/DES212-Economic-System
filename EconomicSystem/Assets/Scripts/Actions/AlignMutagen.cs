using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]

public class AlignMutagenCostSet
{
    public ActionCost[] Costs;
}
public class AlignMutagen : CostChanger
{
    public GameObject AlignmentButton;
    public List<AlignMutagenCostSet> costs;

    public void Start()
    {
        ApplyRandomMutation();
    }

    public override void TriggerCostChange()
    {
        ApplyRandomMutation();
    }

    public void ApplyRandomMutation()
    {
        var alignmentButton = AlignmentButton.GetComponent<ActionButton>();
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
}
