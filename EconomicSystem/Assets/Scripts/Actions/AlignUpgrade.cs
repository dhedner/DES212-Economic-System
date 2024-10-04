using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlignUpgrade : MonoBehaviour
{ 
    public AlignMutagen alignMutagen;
    public ActionButton actionButton;
    public SelectionType selectionType;

    private CurrencyManager currencyManager;

    public void Start()
    {
        actionButton = GetComponent<ActionButton>();
        currencyManager = GetComponentInParent<CurrencyManager>();
        actionButton.EnableCondition = IsEnabledMutagenButton;
    }

    private bool IsEnabledMutagenButton(ActionButton button)
    {
        // If the button can be afforded and it is not active, activate it
        return alignMutagen.selectionType != selectionType && currencyManager.CanAfford(button.ActionButtonCosts);
    }

    public void AssignSelectionType()
    {
        alignMutagen.selectionType = selectionType;
    }
}
