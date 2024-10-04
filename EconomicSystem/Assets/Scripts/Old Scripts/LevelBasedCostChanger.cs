using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]

public class ActionMultiplier
{
    public string CurrencyType;
    public int Multiplier;
}

[Serializable]
public class ActionCostSet
{
    public ActionCost[] Costs;
    public ActionMultiplier[] multipliers;
}

public class LevelBasedCostChanger : CostChanger
{
    private int currentLevel = 0;
    public List<ActionCostSet> costs;

    void Start()
    {
        var actionbutton = GetComponent<ActionButton>();
        actionbutton.ActionButtonCosts = costs[currentLevel].Costs.ToList();
    }

    public override void TriggerCostChange()
    {
        var currencyManager = GetComponentInParent<CurrencyManager>();

        if (currentLevel >= costs.Count)
        {
            return;
        }

        var actionCostSet = costs[currentLevel];
        var cost = actionCostSet.Costs.ToList();
        if (currencyManager.CanAfford(cost))
        {
            currencyManager.SpendCurrency(cost);
            
            var actionbutton = GetComponent<ActionButton>();
            currentLevel++;
            if (currentLevel < costs.Count)
            {
                actionbutton.ActionButtonCosts = costs[currentLevel].Costs.ToList();
            }
        }
    }
}
