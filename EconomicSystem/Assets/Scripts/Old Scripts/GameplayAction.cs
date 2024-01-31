using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Action")]
public class GameplayAction : ScriptableObject
{
    private enum ActionType
    {
        Generate,
        Research,
        Stabilize,
        Recycle,
        BuildGenome,
        Align, 
        Refine,
        Release,
        GeneticSequencer,
        DNASynthesizer,
        HelixSplitter
    }

    public int cost = 0;
    public int rate = 1;
    public int phaseThreshold = 0;

    public void ResetValues()
    {
        cost = 0;
        rate = 1;
        phaseThreshold = 0;
    }
}
