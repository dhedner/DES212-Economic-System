using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class TelemetryWriter : MonoBehaviour
{
    public bool enableTelemetry = false;
    public ActionTracker tracker;

    private CurrencyManager currencyManager;
    private GameplayController gameplayController;

    private double totalTurns;
    private double[] phaseTurns = new double[4];
    private CurrencyType[] currencies;
    private GameplayButton[] buttons;
    private string date;

    public void Start()
    {
        if (!enableTelemetry)
        {
            return;
        }
        currencyManager = GetComponent<CurrencyManager>();
        gameplayController = GetComponent<GameplayController>();

        date = DateTime.Now.ToString("yyyyMMdd-\\THHmmss\\Z");

        currencies = new CurrencyType[Enum.GetNames(typeof(CurrencyType)).Length];
        buttons = new GameplayButton[Enum.GetNames(typeof(GameplayButton)).Length];

        WriteCurrencyData();
    }

    // Message being broadcast from the ActionButton OnClick()
    public void OnCurrencyChanged()
    {
        if (!enableTelemetry)
        {
            return;
        }
        totalTurns = tracker.GetTotalClickCount();

        if (totalTurns % 10 == 0)
        {
            WriteCurrencyData();
        }
    }

    // Message being broadcast from the Phase when next phase is activated
    public void OnNextPhase()
    {
        if (!enableTelemetry)
        {
            return;
        }
        if (gameplayController.CurrentPhaseIndex >= phaseTurns.Length + 1)
        {
            return;
        }
        phaseTurns[gameplayController.CurrentPhaseIndex - 2] = totalTurns;
    }

    // Message being broadcast from the GameplayController when game is over
    public void OnGameOver()
    {
        phaseTurns[phaseTurns.Length - 1] = totalTurns;
    }

    public void OnRestartGame()
    {
        // When the game restarts, reset all local data
        if (!enableTelemetry)
        {
            return;
        }
        totalTurns = 0;
        phaseTurns = new double[4];

        WriteCurrencyData();
    }

    private void WriteButtonData()
    {
        if (!enableTelemetry)
        {
            return;
        }

        Debug.Log("Writing button data");

        if (!File.Exists($"ButtonData{date}.csv"))
        {
            WriteToCSV($"ButtonData{date}.csv", string.Join(",", Enum.GetNames(typeof(GameplayButton))));
        }

        var buttonCounts = from GameplayButton type in Enum.GetValues(typeof(GameplayButton))
                           select tracker.GetButtonClickCount(type);

        WriteToCSV($"ButtonData{date}.csv", string.Join(",", buttonCounts));
    }

    private void WritePhaseData()
    {
        if (!enableTelemetry)
        {
            return;
        }

        Debug.Log("Writing phase data");

        if (!File.Exists($"TurnsAtPhaseStart{date}.csv"))
        {
            WriteToCSV($"TurnsAtPhaseStart{date}.csv", "Phase1,Phase2,Phase3,Phase4");
        }

        WriteToCSV($"TurnsAtPhaseStart{date}.csv", string.Join(",", phaseTurns));
    }

    private void WriteCurrencyData()
    {
        if (!enableTelemetry)
        {
            return;
        }

        Debug.Log("Writing currency data");

        if (!File.Exists($"CurrencyPerTurn{date}.csv"))
        {
            WriteToCSV($"CurrencyPerTurn{date}.csv", string.Join(",", Enum.GetNames(typeof(CurrencyType))));
        }

        var currencyAmounts = from CurrencyType x in Enum.GetValues(typeof(CurrencyType))
                              select currencyManager.GetCurrencyAmount(x);

        WriteToCSV($"CurrencyPerTurn{date}.csv", string.Join(",", currencyAmounts));
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

    //private void PutCSVInDirectory(string filename, string path)
    //{
    //    //string path = Application.dataPath + "/TelemetryData";
    //    if (!Directory.Exists(path))
    //    {
    //        Directory.CreateDirectory(path);
    //    }

    //    File.Move(filename, path + "/" + filename);
    //}

    private void OnDestroy()
    {
        // Write the data on termination
        //OnGameOver();
        WriteButtonData();
        WritePhaseData();
    }
}
