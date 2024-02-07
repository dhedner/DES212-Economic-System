using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Assertions;

[Flags]
public enum GameplayButton
{
    Generate = 0x0001,
    Research = 0x0002,
    GeneticSequencer = 0x0004,
    Stabilize = 0x0008,
    Recycle = 0x0010,
    DNASynthesizer = 0x0020,
    HelixSplitter = 0x0040,
    Synthesize = 0x0080,
    AlignMutagen = 0x0100,
    Deconstruct = 0x0200,
    DominantAlignment = 0x0400,
    RecessiveAlignment = 0x0800,
    Construct = 0x1000,
    Release = 0x2000,
}

public class GameplayController : MonoBehaviour
{
    public Phase[] phases;
    private Phase currentPhase;
    private CurrencyManager currencyManager;
    string path = "CurrencyDisplay/";

    public GameplayButton AvailableActions
    {
        get
        {
            var actionButtons = GetComponentsInChildren<ActionButton>();
            GameplayButton result = 0;
            foreach (var button in actionButtons)
            {
                if (button.IsClickable)
                {
                    result |= button.AssignedGameplayButton;
                }
            }

            return result;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currencyManager = GetComponentInParent<CurrencyManager>();
        phases[0].Activate();
        currentPhase = phases[0];
        UpdateCurrencyCounts();
    }

    public void OnCurrencyChanged()
    {
        UpdateCurrencyCounts();
        //GameObject.Find("GameplayCanvas/Phase1/Upgrades/DNASynthesizer/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + currencyManager.GetLevel("DNA");
    }

    public void Win()
    {
        transform.GetChild(1).GetComponent<Canvas>().enabled = true;
        foreach (var phase in phases)
        {
            phase.Deactivate();
        }

        BroadcastMessage("OnGameOver");
    }

    public void TriggerAction(GameplayButton action)
    {
        var actionButtons = GetComponentsInChildren<ActionButton>();
        foreach (var button in actionButtons)
        {
            if (button.AssignedGameplayButton == action)
            {
                button.UIButton.onClick.Invoke();
                Debug.Log($"Autoplay triggered {action}");
                return;
            }
        }
    }

    public void PhaseHasChanged()
    {
        BroadcastMessage("OnNextPhase");
    }

    private void UpdateCurrencyCounts()
    {
        GameObject.Find(path + "GMCount").GetComponent<TMPro.TextMeshProUGUI>().text = "GM: " + currencyManager.GetCurrencyAmount(CurrencyType.GeneticMaterial);
        GameObject.Find(path + "DNACount").GetComponent<TMPro.TextMeshProUGUI>().text = "DNA: " + currencyManager.GetCurrencyAmount(CurrencyType.DNA);
        GameObject.Find(path + "GenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genome: " + currencyManager.GetCurrencyAmount(CurrencyType.Genome);
        GameObject.Find(path + "RedGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "R: " + currencyManager.GetCurrencyAmount(CurrencyType.RedGenome);
        GameObject.Find(path + "PurpleGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "P: " + currencyManager.GetCurrencyAmount(CurrencyType.GreenGenome);
        GameObject.Find(path + "GreenGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "G: " + currencyManager.GetCurrencyAmount(CurrencyType.PurpleGenome);
        GameObject.Find(path + "CellClusterCount").GetComponent<TMPro.TextMeshProUGUI>().text = "CC: " + currencyManager.GetCurrencyAmount(CurrencyType.CellClusters);
        GameObject.Find(path + "ResearchLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Research: " + currencyManager.GetCurrencyAmount(CurrencyType.Research);
    }
}
