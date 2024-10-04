using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SeeStats : MonoBehaviour
{
    public Image statsPanel;
    public UnityEvent OnSeeStats;

    public void Start()
    {
        statsPanel.enabled = false;
    }

    public void Execute()
    {
        Debug.Log("SeeStats.Execute");
        OnSeeStats.Invoke();
        statsPanel.enabled = true;
    }
}
