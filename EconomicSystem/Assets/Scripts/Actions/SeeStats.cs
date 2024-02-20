using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeeStats : MonoBehaviour
{
    public Image statsPanel;

    public void Start()
    {
        statsPanel.enabled = false;
    }

    public void Execute()
    {
        Debug.Log("SeeStats.Execute");
        statsPanel.enabled = true;
    }
}
