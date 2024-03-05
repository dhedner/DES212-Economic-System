using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using System.IO;

public class GameplayController : MonoBehaviour
{
    public static bool AutoMode = false;
    public static bool FastMode = false;
    public static float DT;

    public int Fights = 6;
    public int FightCount = 0;
    public static bool SimOver = false;
    public static string CurrentAI = "Random";

    public int Rounds = 6;
    private int RoundCount = 0;
    public static bool RoundOver = false;
    public static bool RoundStart = false;
    public static float RoundDelay = 3.0f;
    public static float RoundTimer = 3.0f;

    // Arena settings
    public static float EdgeDistance = 8.0f;
    public static float StartingX = 5.0f;

    // Telemetry data for an individual fight
    public static int Victories = 0;
    public static int Defeats = 0;
    public static float DamageDone = 0;
    public static float TotalFightTime = 0;
    public static StreamWriter DataStream;

    public static Player Player;

    public static GameObject Canvas;

    public static GameObject InfoTextPrefab;
    public static GameObject StaticInfoTextPrefab;
    public static GameObject EnemyType1Prefab;
    public static GameObject EnemyType2Prefab;
    public static GameObject EnemyType3Prefab;

    // Start is called before the first frame update
    void Start()
    {
        DataStream = new StreamWriter("FightData.csv", true);

        DataStream.WriteLine("AI,Victories,Defeats,DamageDone,TotalFightTime");

        Canvas = GameObject.Find("Canvas");

        Player = GameObject.Find("Player").GetComponent<Player>();

        // Load all prefabs
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Application.Quit();
    //    }

    //    if (FightCount >= Fights)
    //    {
    //        if (!SimOver)
    //        {
    //            SimOver = true;
    //            DataStream.Close();
    //            SpawnInfoText(("SIMULATION OVER"), true);
    //        }

    //        return;
    //    }

    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        AutoMode = !AutoMode;
    //    }

    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        FastMode = !FastMode;
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        FightCount = 0;
    //        RoundCount = 0;
    //        RoundTimer = RoundDelay;
    //        RoundStart = false;
    //        RoundOver = false;
    //        SimOver = false;
    //        CurrentAI = "Random";
    //        Victories = 0;
    //        Defeats = 0;
    //        DamageDone = 0;
    //        TotalFightTime = 0;
    //    }

    //    if (FastMode)
    //    {
    //        DT = 0.1f;
    //    }
    //    else if (Time.deltaTime < 0.1f)
    //    {
    //        DT = Time.deltaTime;
    //    }
    //    else
    //    {
    //        DT = 0.1f;
    //    }

    //    if (RoundCount == 0)
    //    {
    //        NewRound();
    //    }

    //    RoundOver = IsRoundOver();
    //    if (!RoundOver)
    //    {
    //        TotalFightTime += DT;
    //    }
    //    else if (RoundTimer > 0.0f)
    //    {
    //        RoundTimer -= DT;
    //    }
    //    else
    //    {
    //        NewRound();
    //    }
    //}

    //bool IsRoundOver()
    //{
    //    // Player is dead
    //    if (Player.HitPoints <= 0.0f)
    //    {
    //        if (!RoundOver)
    //        {
    //            SpawnInfoText("DEFEAT");
    //            Defeats++;
    //        }
    //        return true;
    //    }

    //    if (Player.Target == null)
    //    {
    //        if (RoundStart)
    //        {
    //            return false;
    //        }

    //        if (!RoundOver)
    //        {
    //            SpawnInfoText("VICTORY");
    //            Victories++;
    //        }
    //        return true;
    //    }

    //    RoundStart = false;
    //    return false;
    //}

    //void NewRound()
    //{
    //    RoundCount++;
    //    ClearEnemies();

    //    if (RoundCount > Rounds)
    //    {
    //        NewFight();
    //        return;
    //    }

    //    if (RoundCount % 6 == 1)
    //    {
    //        Instantiate(EnemyType1Prefab, new Vector3(StartingX, 0.0f, 0.0f), Quaternion.Euler(0, 0, 90), null);
    //    }
    //    else if (RoundCount % 6 == 2)
    //    {
    //        Instantiate(EnemyType2Prefab, new Vector3(StartingX, 0.0f, 0.0f), Quaternion.Euler(0, 0, 90), null);
    //    }
    //    else if (RoundCount % 6 == 3)
    //    {
    //        Instantiate(EnemyType3Prefab, new Vector3(StartingX, 0.0f, 0.0f), Quaternion.Euler(0, 0, 90), null);
    //    }
    //    else if (RoundCount % 6 == 4)
    //    {
    //        Instantiate(EnemyType1Prefab, new Vector3(StartingX + 1, -1.5f, 0.0f), Quaternion.Euler(0, 0, 90), null);
    //        Instantiate(EnemyType1Prefab, new Vector3(StartingX, 0.0f, 2.0f), Quaternion.Euler(0, 0, 90), null);
    //        Instantiate(EnemyType1Prefab, new Vector3(StartingX + 1, 1.5f, 2.0f), Quaternion.Euler(0, 0, 90), null);
    //    }
    //    else if (RoundCount % 6 == 5)
    //    {
    //        Instantiate(EnemyType2Prefab, new Vector3(StartingX + 1, -1.5f, 0.0f), Quaternion.Euler(0, 0, 90), null);
    //        Instantiate(EnemyType2Prefab, new Vector3(StartingX, 0.0f, 2.0f), Quaternion.Euler(0, 0, 90), null);
    //        Instantiate(EnemyType2Prefab, new Vector3(StartingX + 1, 1.5f, 2.0f), Quaternion.Euler(0, 0, 90), null);
    //    }
    //    else if (RoundCount % 6 == 0)
    //    {
    //        Instantiate(EnemyType3Prefab, new Vector3(StartingX + 1, -1.5f, 0.0f), Quaternion.Euler(0, 0, 90), null);
    //        Instantiate(EnemyType3Prefab, new Vector3(StartingX, 0.0f, 2.0f), Quaternion.Euler(0, 0, 90), null);
    //        Instantiate(EnemyType3Prefab, new Vector3(StartingX + 1, 1.5f, 2.0f), Quaternion.Euler(0, 0, 90), null);
    //    }

    //    Player.Initialize();

    //    SpawnInfoText("ROUND " + RoundCount);

    //    RoundTimer = RoundDelay;
    //    RoundStart = true;
    //}

    //void NextFight()
    //{
    //    FightCount++;
    //    RoundCount = 0;
    //    DataStream.WriteLine(CurrentAI + "," + Victories + "," + Defeats + "," + DamageDone + "," + TotalFightTime);

    //    Victories = 0;
    //    Defeats = 0;
    //    DamageDone = 0;
    //    TotalFightTime = 0;
    //}

    //void ClearEnemies()
    //{
    //    var enemies = FindObjectsOfType<Enemy>();

    //    if (enemies.Length == 0)
    //    {
    //        return;
    //    }

    //    foreach (var enemy in enemies)
    //    {
    //        Destroy(enemy.gameObject);
    //    }
    //}

    //void SpawnInfoText(string text, bool isStatic = false)
    //{
    //    SpawnInfoText(new Vector3(0.0f, 0.0f, 0.0f), text, isStatic);
    //}

    //void SpawnInfoText(Vector3 position, string text, bool isStatic = false)
    //{
    //    var prefab = isStatic ? StaticInfoTextPrefab : InfoTextPrefab;
    //    var infoText = Instantiate(prefab, position, Quaternion.identity, Canvas.transform);
    //    infoText.GetComponent<InfoText>().SetText(text);
    //}
}
