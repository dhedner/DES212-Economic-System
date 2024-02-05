using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ConversionCostSet
{
    public ActionCost[] ActionCosts;
    public ActionCost[] UpgradeCost;

    override public string ToString()
    {
        string str = "ActionCosts: ";
        foreach (var cost in ActionCosts)
        {
            str += cost + ", ";
        }
        str += "UpgradeCost: ";
        foreach (var cost in UpgradeCost)
        {
            str += cost + ", ";
        }
        return str;
    }
}

public class ConversionChanger : CostChanger
{
    public GameObject ButtonWithCosts;
    private int currentLevel = 0;
    public List<ConversionCostSet> costs;
    private ActionButton actionButton;
    private ActionButton upgradeButton;

    public void Start()
    {
        upgradeButton = gameObject.GetComponent<ActionButton>();
        actionButton = ButtonWithCosts.GetComponent<ActionButton>();

        ApplyCosts();
    }

    public override void TriggerCostChange()
    {
        var upgradeButton = gameObject.GetComponent<ActionButton>();
        if (currentLevel >= costs.Count - 1)
        {
            upgradeButton.ActionButtonCosts = new List<ActionCost>
            {
                new ActionCost("GeneticMaterial", int.MaxValue)
            };
            upgradeButton.SetButtonOpacity(0.3f);

            return;
        }

        currentLevel++;
        if (currentLevel < costs.Count)
        {
            ApplyCosts();
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    private void ApplyCosts()
    {
        var actionCostSet = costs[currentLevel];
        // If the button with costs is the same as the button that triggered the cost change
        if (actionButton == upgradeButton)
        {
            Debug.Log($"Combining button costs to {actionCostSet}");

            var combined = actionCostSet.ActionCosts.Concat(actionCostSet.UpgradeCost).ToArray();
            actionButton.ActionButtonCosts = combined.ToList();
        }
        // If the button with costs is not the same as the button that triggered the cost change
        else
        {
            Debug.Log($"Switching button costs to {actionCostSet}");

            actionButton.ActionButtonCosts = actionCostSet.ActionCosts.ToList();
            upgradeButton.ActionButtonCosts = actionCostSet.UpgradeCost.ToList();
        }
    }
}
