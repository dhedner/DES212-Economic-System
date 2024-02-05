using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public Phase[] phases;
    private CurrencyManager currencyManager;
    string path = "CurrencyDisplay/";

    // Start is called before the first frame update
    void Start()
    {
        currencyManager = GetComponentInParent<CurrencyManager>();
        phases[0].Activate();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find(path + "GMCount").GetComponent<TMPro.TextMeshProUGUI>().text = "GM: " + currencyManager.GetCurrencyAmount("GeneticMaterial");
        GameObject.Find(path + "DNACount").GetComponent<TMPro.TextMeshProUGUI>().text = "DNA: " + currencyManager.GetCurrencyAmount("DNA");
        GameObject.Find(path + "GenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genome: " + currencyManager.GetCurrencyAmount("Genome");
        GameObject.Find(path + "RedGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "R: " + currencyManager.GetCurrencyAmount("RedGenome");
        GameObject.Find(path + "PurpleGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "P: " + currencyManager.GetCurrencyAmount("PurpleGenome");
        GameObject.Find(path + "GreenGenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "G: " + currencyManager.GetCurrencyAmount("GreenGenome");
        GameObject.Find(path + "CellClusterCount").GetComponent<TMPro.TextMeshProUGUI>().text = "CC: " + currencyManager.GetCurrencyAmount("CellClusters");
        GameObject.Find(path + "ResearchLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Research: " + currencyManager.GetCurrencyAmount("Research");

        //GameObject.Find("GameplayCanvas/Phase1/Upgrades/DNASynthesizer/UpgradeLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + currencyManager.GetLevel("DNA");
    }

    public void Win()
    {
        transform.GetChild(0).GetComponent<Canvas>().enabled = true;
    }
}
