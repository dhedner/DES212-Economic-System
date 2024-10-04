using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Phase : MonoBehaviour
{
    public string Name;
    public List<ActionButton> Buttons;
    public int LevelCost;
    public Predicate<Phase> EnableCondition = (phase) => phase.currencyManager.CanAfford(new List<ActionCost> { new ActionCost(CurrencyType.Research, phase.LevelCost) });

    private GameplayController gameplayController;
    private CurrencyManager currencyManager;

    public bool isActive = false;

    public void Start()
    {
        currencyManager = GetComponentInParent<CurrencyManager>();
        gameplayController = GetComponentInParent<GameplayController>();

        if (!isActive)
        {
            Deactivate();
        }
    }

    public void OnCurrencyChanged()
    {
        //bool canEnablePhase = currencyManager.CanAfford(new List<ActionCost>
        //{
        //    new ActionCost(CurrencyType.Research, LevelCost),
        //});

        bool canEnablePhase = EnableCondition(this) && !gameplayController.HasWon;

        if (!isActive && canEnablePhase)
        {
            Activate();
            gameplayController.PhaseHasChanged();
        }
    }

    public void OnGameOver()
    {
        Deactivate();
    }

    public void Activate()
    {
        ShowButtons();

        foreach (var image in gameObject.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
        }

        foreach (var text in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.enabled = true;
        }

        isActive = true;
    }

    public void Deactivate()
    {
        HideButtons();

        foreach (var image in gameObject.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }

        foreach (var text in gameObject.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.enabled = false;
        }

        isActive = false;
    }

    private void ShowButtons()
    {
        foreach (var button in Buttons)
        {
            button.Show();
        }
    }

    private void HideButtons()
    {
        foreach (var button in Buttons)
        {
            button.Hide();
        }
    }
}
