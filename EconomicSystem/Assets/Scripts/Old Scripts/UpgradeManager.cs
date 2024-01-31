using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Currency geneticMaterialCurrency;
    public Currency DNACurrency;
    public Currency genomeCurrency;

    public Button GeneticSequencerButton;
    public Button DNASynthesizerButton;
    public Button HelixSplitterButton;

    // Upgrades and the actions they affect
    public GameplayAction generateAction;
    public GameplayAction geneticSequencerAction;

    public GameplayAction stabilizeAction;
    public GameplayAction DNASynthesizerAction;

    public GameplayAction recycleAction;
    public GameplayAction HelixSplitterAction;

    private int[] _geneticSequencerCost = { 10, 30, 70 };
    private int _geneticSequencerLevel = 0;

    private int[] _DNASynthesizerCost = { 10, 30, 70, 150, 280 };
    private int _DNASynthesizerLevel = 0;

    private int[] _HelixSplitterCost = { 10, 30, 70 };
    private int _HelixSplitterLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GeneticSequencerButton != null)
        {
            GeneticSequencerButton.onClick.AddListener(GeneticSequencer);
        }

        if (DNASynthesizerButton != null)
        {
            DNASynthesizerButton.onClick.AddListener(DNASynthesizer);
        }

        if (HelixSplitterButton != null)
        {
            HelixSplitterButton.onClick.AddListener(HelixSplitter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("GameplayCanvas/Upgrades/GeneticSequencerUpgrade/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "LV: " + _geneticSequencerLevel.ToString() + "/3";
        GameObject.Find("GameplayCanvas/Upgrades/DNASynthesizerUpgrade/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "LV: " + _DNASynthesizerLevel.ToString() + "/5";
        GameObject.Find("GameplayCanvas/Upgrades/HelixSplitterUpgrade/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "LV: " + _HelixSplitterLevel.ToString() + "/3";
    }

    //void ApplyUpgrade(Currency currency, GameplayAction upgradeAction, GameplayAction affectedAction, int[] cost, int level)
    //{
    //    if (currency.amount < cost[level])
    //    {
    //        Debug.Log("Not enough Genetic Material to upgrade generation");
    //        return;
    //    }

    //    currency.amount -= cost[level];
    //    level += 1;

    //    upgradeAction.cost = cost[level];
    //    upgradeAction.rate += 1;
    //    affectedAction.rate = upgradeAction.rate;

    //    Debug.Log("Upgrade Purchased (LV: " + level + ")");
    //}

    void GeneticSequencer()
    {
        //ApplyUpgrade(geneticMaterialCurrency, geneticSequencerAction, generateAction, _geneticSequencerCost, _geneticSequencerLevel);
        if (geneticMaterialCurrency.amount < _geneticSequencerCost[_geneticSequencerLevel])
        {
            Debug.Log("Not enough Genetic Material to upgrade generation");
            return;
        }

        geneticMaterialCurrency.amount -= _geneticSequencerCost[_geneticSequencerLevel];
        _geneticSequencerLevel += 1;

        geneticSequencerAction.cost = _geneticSequencerCost[_geneticSequencerLevel];
        geneticSequencerAction.rate += 1;
        generateAction.rate = geneticSequencerAction.rate;

        Debug.Log("Upgrade Purchased (LV: " + _geneticSequencerLevel + ")");
    }

    void DNASynthesizer()
    {
        //ApplyUpgrade(DNACurrency, DNASynthesizerAction, stabilizeAction, _DNASynthesizerCost, _DNASynthesizerLevel);
        if (DNACurrency.amount < _DNASynthesizerCost[_DNASynthesizerLevel])
        {
            Debug.Log("Not enough DNA to upgrade stabilization");
            return;
        }

        DNACurrency.amount -= _DNASynthesizerCost[_DNASynthesizerLevel];
        _DNASynthesizerLevel += 1;

        DNASynthesizerAction.cost = _DNASynthesizerCost[_DNASynthesizerLevel];
        DNASynthesizerAction.rate += 1;
        stabilizeAction.rate = DNASynthesizerAction.rate;

        Debug.Log("Upgrade Purchased (LV: " + _DNASynthesizerLevel + ")");
    }

    void HelixSplitter()
    {
        //ApplyUpgrade(DNACurrency, HelixSplitterAction, stabilizeAction, _HelixSplitterCost, _HelixSplitterLevel);
        if (DNACurrency.amount < _HelixSplitterCost[_HelixSplitterLevel])
        {
            Debug.Log("Not enough Genomes to upgrade splitting");
            return;
        }

        DNACurrency.amount -= _HelixSplitterCost[_HelixSplitterLevel];
        _HelixSplitterLevel += 1;

        HelixSplitterAction.cost = _HelixSplitterCost[_HelixSplitterLevel];
        HelixSplitterAction.rate += 5;
        recycleAction.rate = HelixSplitterAction.rate;

        Debug.Log("Upgrade Purchased (LV: " + _HelixSplitterLevel + ")");
    }
}
