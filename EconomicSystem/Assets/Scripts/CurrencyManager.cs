using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static event Action<Currency> OnCurrencyChanged;

    public Currency geneticMaterialCurrency;
    public Currency DNACurrency;
    public Currency genomeCurrency;
    public Currency researchLevelCurrency;

    public Button GenerateButton;
    public Button ResearchButton;
    public Button StabilizeButton;
    public Button RecycleButton;

    public GameplayAction generateAction;
    public GameplayAction researchAction;
    public GameplayAction stabilizeAction;
    public GameplayAction recycleAction;

    void Start()
    {
        if (GenerateButton != null)
        {
            GenerateButton.onClick.AddListener(IncreaseGeneticMaterial);
        }

        if (ResearchButton != null)
        {
            ResearchButton.onClick.AddListener(IncreaseResearch);
        }

        if (StabilizeButton != null)
        {
            StabilizeButton.onClick.AddListener(IncreaseDNA);
        }

        if (RecycleButton != null)
        {
            RecycleButton.onClick.AddListener(RecycleDNA);
        }
    }

    void Update()
    {
        GameObject.Find("GameplayCanvas/GMCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genetic Material: " + geneticMaterialCurrency.amount.ToString();
        GameObject.Find("GameplayCanvas/DNACount").GetComponent<TMPro.TextMeshProUGUI>().text = "DNA: " + DNACurrency.amount.ToString();
        GameObject.Find("GameplayCanvas/GenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genomes: " + genomeCurrency.amount.ToString();
        GameObject.Find("GameplayCanvas/ResearchLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Research: " + researchLevelCurrency.amount.ToString();
    }

    public void ChangeCurrencyAmount(Currency currency, int amount)
    {
        if (currency != null)
        {
            currency.amount += amount;
            OnCurrencyChanged?.Invoke(currency);
        }
        else
        {
            Debug.LogError("Currency object is null.");
        }
    }

    public void ChangeResearchLevel(int amount)
    {
        geneticMaterialCurrency.amount -= researchAction.cost;
        researchLevelCurrency.amount += amount;
        OnCurrencyChanged?.Invoke(researchLevelCurrency);
    }

    public void IncreaseGeneticMaterial()
    {
        ChangeCurrencyAmount(geneticMaterialCurrency, generateAction.rate);
    }

    public void IncreaseResearch()
    {
        ChangeResearchLevel(1);
    }

    public void IncreaseDNA()
    {
        ChangeCurrencyAmount(DNACurrency, stabilizeAction.rate);
    }

    public void RecycleDNA()
    {
        ChangeCurrencyAmount(DNACurrency, -recycleAction.cost);
        ChangeCurrencyAmount(geneticMaterialCurrency, recycleAction.rate);
    }
}
