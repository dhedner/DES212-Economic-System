using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Assertions;
using UnityEngine.Events;

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
    SeeStats = 0x4000,
    Restart = 0x8000,
}

public class GameplayController : MonoBehaviour
{
    public int CurrentPhaseIndex
    {
        get; private set;
    } = 1;

    public bool HasWon
    {
        get
        {
            return CurrentPhaseIndex >= phases.Length;
        }
    }

    public Phase[] phases;
    public UnityEvent OnWin;
    public UnityEvent OnRestart;

    private CurrencyManager currencyManager;
    string currencyDisplayPath = "CurrencyDisplay/";

    public GameplayState GameplayState
    {
        get
        {
            return new GameplayState
            {
                EnabledButtons = AvailableActions,
                Level = CurrentPhaseIndex,
            };
        }
    }

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
        OnCurrencyChanged();
    }

    public void OnCurrencyChanged()
    {
        UpdateCurrencyCounts();
        //GameObject.Find("GameplayCanvas/Phase1/Upgrades/DNASynthesizer/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + currencyManager.GetLevel("DNA");
    }

    public void Win()
    {
        foreach (var phase in phases)
        {
            phase.Deactivate();
        }

        //phases[phases.Length - 1].Activate();

        // Enable canvas
        transform.GetChild(6).GetComponent<Image>().enabled = true;
        // Set canvas to sorting order to the top
        //GameObject.Find("WinScreen").GetComponent<Canvas>().enabled = true;
        //transform.GetChild(8).SetAsLastSibling();

        OnWin.Invoke();

        BroadcastMessage("OnGameOver");
    }

    public void RestartGame()
    {
        currencyManager.ResetAllCurrency();
        phases[0].Activate();
        UpdateCurrencyCounts();

        transform.GetChild(6).GetComponent<Image>().enabled = false;

        OnRestart.Invoke();

        BroadcastMessage("OnRestartGame");
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
        CurrentPhaseIndex++;
        BroadcastMessage("OnNextPhase");
    }

    private void UpdateCurrencyCounts()
    {
        GameObject.Find(currencyDisplayPath + "GMCount").GetComponent<TMPro.TextMeshProUGUI>().text = "" + currencyManager.GetCurrencyAmount(CurrencyType.GeneticMaterial);
        GameObject.Find(currencyDisplayPath + "DNACount").GetComponent<TMPro.TextMeshProUGUI>().text = "" + currencyManager.GetCurrencyAmount(CurrencyType.DNA);
        GameObject.Find(currencyDisplayPath + "GenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "" + currencyManager.GetCurrencyAmount(CurrencyType.Genome);
        GameObject.Find(currencyDisplayPath + "RedGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "" + currencyManager.GetCurrencyAmount(CurrencyType.RedGenome);
        GameObject.Find(currencyDisplayPath + "PurpleGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "" + currencyManager.GetCurrencyAmount(CurrencyType.PurpleGenome);
        GameObject.Find(currencyDisplayPath + "GreenGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "" + currencyManager.GetCurrencyAmount(CurrencyType.GreenGenome);
        GameObject.Find(currencyDisplayPath + "CellClusterCount").GetComponent<TMPro.TextMeshProUGUI>().text = "" + currencyManager.GetCurrencyAmount(CurrencyType.CellClusters);
        GameObject.Find(currencyDisplayPath + "ResearchLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Research: " + currencyManager.GetCurrencyAmount(CurrencyType.Research);
    }
}
