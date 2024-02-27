using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CurrencyType
{
    GeneticMaterial,
    DNA,
    Genome,
    RedGenome,
    GreenGenome,
    PurpleGenome,
    CellClusters,
    Research,
    DummyCurrency
}

[Serializable]
public class CurrencyStatus
{
    public int amount;
    public double multiplier;
}

public class CurrencyManager : MonoBehaviour
{
    private int _redGenomesUsed;
    private int _greenGenomesUsed;
    private int _purpleGenomesUsed;

    public Dictionary<CurrencyType, CurrencyStatus> currencies;

    public CurrencyManager()
    {
        currencies = new Dictionary<CurrencyType, CurrencyStatus>{
            { CurrencyType.GeneticMaterial, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.DNA, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.Genome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.RedGenome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.PurpleGenome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.GreenGenome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.CellClusters, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.Research, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
            { CurrencyType.DummyCurrency, new CurrencyStatus { amount = 0, multiplier = 1.0 } }
        };

        // Use for end of game testing
        //currencies = new Dictionary<CurrencyType, CurrencyStatus>{
        //    { CurrencyType.GeneticMaterial, new CurrencyStatus { amount = 500, multiplier = 1.0 } },
        //    { CurrencyType.DNA, new CurrencyStatus { amount = 300, multiplier = 1.0 } },
        //    { CurrencyType.Genome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
        //    { CurrencyType.RedGenome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
        //    { CurrencyType.PurpleGenome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
        //    { CurrencyType.GreenGenome, new CurrencyStatus { amount = 0, multiplier = 1.0 } },
        //    { CurrencyType.CellClusters, new CurrencyStatus { amount = 10, multiplier = 1.0 } },
        //    { CurrencyType.Research, new CurrencyStatus { amount = 5, multiplier = 1.0 } },
        //    { CurrencyType.DummyCurrency, new CurrencyStatus { amount = 0, multiplier = 1.0 } }
        //};
    }

    public CurrencyType MostAlignedType
    {
        get
        {
            if (currencies[CurrencyType.RedGenome].amount > currencies[CurrencyType.GreenGenome].amount && currencies[CurrencyType.RedGenome].amount > currencies[CurrencyType.PurpleGenome].amount)
            {
                return CurrencyType.RedGenome;
            }
            else if (currencies[CurrencyType.GreenGenome].amount > currencies[CurrencyType.RedGenome].amount && currencies[CurrencyType.GreenGenome].amount > currencies[CurrencyType.PurpleGenome].amount)
            {
                return CurrencyType.GreenGenome;
            }
            else
            {
                return CurrencyType.PurpleGenome;
            }
        }
    }

    public void AddCurrency(CurrencyType type, int amount)
    {
        if (!currencies.ContainsKey(type))
        {
            currencies[type] = new CurrencyStatus{ 
                amount = 0,
                multiplier = 1.0,
            };
        }
        currencies[type].amount += (int)Math.Round(amount * currencies[type].multiplier);

        BroadcastMessage("OnCurrencyChanged");
    }

    public bool CanAfford(List<ActionCost> costs)
    {
        if (costs == null)
        {
            return true;
        }

        foreach (var cost in costs)
        {
            if (!currencies.ContainsKey(cost.currencyType) || currencies[cost.currencyType].amount < cost.Amount)
            {
                return false;
            }
        }
        return true;
    }

    public void SpendCurrency(List<ActionCost> costs)
    {
        if (CanAfford(costs))
        {
            foreach (var cost in costs)
            {
                Debug.Log("Costs: " + cost);
                currencies[cost.currencyType].amount -= cost.Amount;

                if (cost.currencyType == CurrencyType.RedGenome && cost.Amount > 0)
                {
                    _redGenomesUsed += cost.Amount;
                }
                else if (cost.currencyType == CurrencyType.GreenGenome && cost.Amount > 0)
                {
                    _greenGenomesUsed += cost.Amount;
                }
                else if (cost.currencyType == CurrencyType.PurpleGenome && cost.Amount > 0)
                {
                    _purpleGenomesUsed += cost.Amount;
                }
            }
            
            BroadcastMessage("OnCurrencyChanged");
        }
    }

    public void AddMultiplier(CurrencyType type, double multiplier)
    {
        currencies[type].multiplier += multiplier;

        BroadcastMessage("OnCurrencyChanged");
    }

    public void RemoveMultiplier(CurrencyType type, double multiplier)
    {
        currencies[type].multiplier -= multiplier;

        BroadcastMessage("OnCurrencyChanged");
    }

    public void SetMultiplier(CurrencyType type, double multiplier)
    {
        currencies[type].multiplier = multiplier;

        BroadcastMessage("OnCurrencyChanged");
    }
    public int GetCurrencyAmount(CurrencyType type)
    {
        return currencies[type].amount;
    }

    public void ResetAllCurrency()
    {
        foreach (var currency in currencies)
        {
            currency.Value.amount = 0;
        }
    }
}

//public class CurrencyManager : MonoBehaviour
//{
//    public static event Action<Currency> OnCurrencyChanged;

//    public Currency geneticMaterialCurrency;
//    public Currency DNACurrency;
//    public Currency genomeCurrency;
//    public Currency researchLevelCurrency;

//    public Button GenerateButton;
//    public Button ResearchButton;
//    public Button StabilizeButton;
//    public Button RecycleButton;

//    public GameplayAction generateAction;
//    public GameplayAction researchAction;
//    public GameplayAction stabilizeAction;
//    public GameplayAction recycleAction;

//    private int[] _researchCost = { 100, 150, 200, 250, 300 };

//    void Start()
//    {
//        if (GenerateButton != null)
//        {
//            GenerateButton.onClick.AddListener(IncreaseGeneticMaterial);
//        }

//        if (ResearchButton != null)
//        {
//            ResearchButton.onClick.AddListener(IncreaseResearch);
//        }

//        if (StabilizeButton != null)
//        {
//            StabilizeButton.onClick.AddListener(IncreaseDNA);
//        }

//        if (RecycleButton != null)
//        {
//            RecycleButton.onClick.AddListener(RecycleDNA);
//        }
//    }

//    void Update()
//    {
//        GameObject.Find("GameplayCanvas/GMCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genetic Material: " + geneticMaterialCurrency.amount.ToString();
//        GameObject.Find("GameplayCanvas/DNACount").GetComponent<TMPro.TextMeshProUGUI>().text = "DNA: " + DNACurrency.amount.ToString();
//        GameObject.Find("GameplayCanvas/GenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genomes: " + genomeCurrency.amount.ToString();
//        GameObject.Find("GameplayCanvas/ResearchLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Research: " + researchLevelCurrency.amount.ToString();
//    }

//    public void ChangeCurrencyAmount(Currency currency, int amount)
//    {
//        if (currency != null)
//        {
//            currency.amount += amount;
//            OnCurrencyChanged?.Invoke(currency);
//        }
//        else
//        {
//            Debug.LogError("Currency object is null.");
//        }
//    }

//    public void ChangeResearchLevel(int amount)
//    {
//        geneticMaterialCurrency.amount -= researchAction.cost;
//        researchLevelCurrency.amount += amount;
//        OnCurrencyChanged?.Invoke(researchLevelCurrency);
//    }

//    public void IncreaseGeneticMaterial()
//    {
//        ChangeCurrencyAmount(geneticMaterialCurrency, generateAction.rate);
//    }

//    public void IncreaseResearch()
//    {
//        ChangeResearchLevel(1);
//    }

//    public void IncreaseDNA()
//    {
//        ChangeCurrencyAmount(DNACurrency, stabilizeAction.rate);
//    }

//    public void RecycleDNA()
//    {
//        ChangeCurrencyAmount(DNACurrency, -recycleAction.cost);
//        ChangeCurrencyAmount(geneticMaterialCurrency, recycleAction.rate);
//    }
//}
