using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[Serializable]
//public class CostPoolSet
//{
//    public ActionCost[] ActionCosts;
//    public ActionCost[] CostPool;

//    override public string ToString()
//    {
//        string str = "ActionCosts: ";
//        foreach (var cost in ActionCosts)
//        {
//            str += cost + ", ";
//        }
//        str += "CostPool: ";
//        foreach (var cost in CostPool)
//        {
//            str += cost + ", ";
//        }
//        return str;
//    }
//}
[Serializable]
public class CostPoolSet
{
    public ActionCost[] Costs;
}

public class StateBasedCost : CostChanger
{
    public GameObject button;
    public List<CostPoolSet> costs;
    private ActionButton actionButton;

    private CurrencyManager currencyManager;

    public void Start()
    {
        actionButton = button.GetComponent<ActionButton>();
        currencyManager = GetComponentInParent<CurrencyManager>();
        ApplyCost();
    }

    public void OnCurrencyChanged()
    {
        ApplyCost();
    }

    public override void TriggerCostChange()
    {
        ApplyCost();
    }

    void ApplyCost()
    {
        var costToApply = costs[0];

        foreach (var costPoolSet in costs)
        {
            // If the currency of the second element of each cost in the cost pool has the greatest amount between every other
            // second element of each cost, then set the cost of the action button to that cost
            CurrencyType currentType = costPoolSet.Costs[1].currencyType;
            if (currencyManager.GetCurrencyAmount(currentType) > currencyManager.GetCurrencyAmount(costToApply.Costs[1].currencyType))
            {
                costToApply = costPoolSet;
            }
        }

        actionButton.ActionButtonCosts = costToApply.Costs.ToList();
    }
}
