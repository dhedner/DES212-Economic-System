using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Progress;

public class TelemetryWriter : MonoBehaviour
{
    public bool enableTelemetry = false;

    private CurrencyManager currencyManager;
    private ActionTracker tracker;

    private double totalTurns;
    private double trackerTurns;
    private double[] phaseTurns = new double[4];
    private CurrencyType[] currencies;
    private GameplayButton[] buttons;
    private int currentPhase = 1;

    public void Start()
    {
        currencyManager = GetComponent<CurrencyManager>();
        tracker = GetComponent<ActionTracker>();

        currencies = new CurrencyType[Enum.GetNames(typeof(CurrencyType)).Length];
        buttons = new GameplayButton[Enum.GetNames(typeof(GameplayButton)).Length];

        trackerTurns = tracker.GetTotalClickCount();

        WriteCurrencyData();
    }

    // Message being broadcast from the ActionButton OnClick()
    public void OnCurrencyChanged()
    {
        totalTurns = tracker.GetTotalClickCount() - trackerTurns;

        if (totalTurns % 10 == 0)
        {
            WriteCurrencyData();
        }
    }

    // Message being broadcast from the Phase when next phase is activated
    public void OnNextPhase()
    {
        phaseTurns[currentPhase] = totalTurns;
        currentPhase++;
    }

    // Message being broadcast from the GameplayController when game is over
    public void OnGameOver()
    {
        WriteButtonData();
        WritePhaseData();
    }

    private void WriteButtonData()
    {
        if (!enableTelemetry)
        {
            return;
        }

        Debug.Log("Writing button data");

        if (!File.Exists("ButtonData.csv"))
        {
            File.Delete("ButtonData.csv");
            WriteToCSV("ButtonData.csv", string.Join(",", Enum.GetNames(typeof(GameplayButton))));
        }

        var buttonCounts = from GameplayButton type in Enum.GetValues(typeof(GameplayButton))
                           select tracker.GetButtonClickCount(type);

        WriteToCSV("ButtonData.csv", string.Join(",", buttonCounts));
    }

    private void WritePhaseData()
    {
        if (!enableTelemetry)
        {
            return;
        }
        
        Debug.Log("Writing phase data");

        if (!File.Exists("TurnsAtPhaseStart.csv"))
        {
            File.Delete("TurnsAtPhaseStart.csv");
            WriteToCSV("TurnsAtPhaseStart.csv", "Phase1,Phase2,Phase3,Phase4");
        }

        WriteToCSV("TurnsAtPhaseStart.csv", string.Join(",", phaseTurns));
    }

    private void WriteCurrencyData()
    {
        if (!enableTelemetry)
        {
            return;
        }

        Debug.Log("Writing currency data");

        if (!File.Exists("CurrencyPerTurn.csv"))
        {
            File.Delete("CurrencyPerTurn.csv");
            WriteToCSV("CurrencyPerTurn.csv", string.Join(",", Enum.GetNames(typeof(CurrencyType))));
        }

        var currencyAmounts = from CurrencyType x in Enum.GetValues(typeof(CurrencyType))
                              select currencyManager.GetCurrencyAmount(x);

        WriteToCSV("CurrencyPerTurn.csv", string.Join(",", currencyAmounts));
    }

    private void WriteToCSV(string filename, string data)
    {
        using StreamWriter dataStream = new StreamWriter(filename, true);

        if (dataStream != null)
        {
            dataStream.WriteLine(data);
            dataStream.Flush(); // Flush() to write without closing the stream
        }
    }

    private void OnDestroy()
    {
        WriteButtonData();
        WritePhaseData();
    }
}
