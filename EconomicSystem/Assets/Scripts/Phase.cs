using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Phase : MonoBehaviour
{
    public string Name;
    public List<ActionButton> Buttons;
    public int LevelCost;

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
        bool canEnablePhase = currencyManager.CanAfford(new List<ActionCost>
        {
            new ActionCost(CurrencyType.Research, LevelCost),
        });

        if (!isActive && canEnablePhase)
        {
            Activate();
            gameplayController.PhaseHasChanged();
        }
    }

    public void Activate()
    {
        ShowButtons();

        foreach (var image in gameObject.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
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
