using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Phase[] phases;
    private CurrencyManager currencyManager;

    // Start is called before the first frame update
    void Start()
    {
        currencyManager = GetComponentInParent<CurrencyManager>();
        phases[0].Activate();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("GameplayCanvas/GMCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genetic Material: " + currencyManager.GetCurrencyAmount("GeneticMaterial");
        GameObject.Find("GameplayCanvas/DNACount").GetComponent<TMPro.TextMeshProUGUI>().text = "DNA: " + currencyManager.GetCurrencyAmount("DNA");
        GameObject.Find("GameplayCanvas/GenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genomes: " + currencyManager.GetCurrencyAmount("Genome");
        GameObject.Find("GameplayCanvas/CellClusterCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Cell Clusters: " + currencyManager.GetCurrencyAmount("CellClusters");
        GameObject.Find("GameplayCanvas/ResearchLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Research: " + currencyManager.GetCurrencyAmount("Research");

        //GameObject.Find("GameplayCanvas/Phase1/Upgrades/DNASynthesizer/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + currencyManager.GetLevel("DNA");
    }

    public void Win()
    {
        transform.GetChild(0).GetComponent<Canvas>().enabled = true;
    }
}
