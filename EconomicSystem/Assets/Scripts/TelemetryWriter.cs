using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class TelemetryWriter : MonoBehaviour
{
    public static StreamWriter DataStream;

    public static void WriteTelemetry(string message, string value)
    {
        DataStream = new StreamWriter("TelemetryData.csv", true);
        DataStream.WriteLine(message);
        DataStream.WriteLine(value);
        DataStream.Close();
    }
}
