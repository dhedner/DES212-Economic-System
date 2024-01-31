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
    private CurrencyManager currencyManager;

    public bool isActive = false;

    public void Start()
    {
        currencyManager = GetComponentInParent<CurrencyManager>();
        if (!isActive)
        {
            Deactivate();
        }
    }

    public void OnCurrencyChanged()
    {
        bool canEnablePhase = currencyManager.CanAfford(new List<ActionCost>
        {
            new ActionCost("Research", LevelCost),
        });

        if (canEnablePhase)
        {
            Activate();
        }
    }

    public void Activate()
    {
        ShowButtons();

        //SendMessage("Show");

        // Show the parent game object
        //foreach (var spriteRenderer in gameObject.GetComponentsInChildren<Renderer>())
        //{
        //    spriteRenderer.enabled = true;
        //}
    }

    public void Deactivate()
    {
        HideButtons();

        //SendMessage("Hide");
        //foreach (var spriteRenderer in gameObject.GetComponentsInChildren<Renderer>())
        //{
        //    spriteRenderer.enabled = false;
        //}
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
