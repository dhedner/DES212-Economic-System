using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] Currency geneticMaterial;
    [SerializeField] Currency DNA;
    [SerializeField] Currency genome;

    [SerializeField] Button GenerateButton;
    [SerializeField] Button ResearchButton;
    [SerializeField] Button StabilizeButton;
    [SerializeField] Button RecycleButton;

    public int researchLevel = 0;

    private int _DNACost = 10;
    private int _genomeCost = 10;
    private int _researchCost = 100;

    void Start()
    {
        if (GenerateButton != null)
        {
            GenerateButton.onClick.AddListener(IncreaseGeneticMaterial);
        }

        if (ResearchButton != null)
        {
            ResearchButton.onClick.AddListener(IncreaseResearch);
        }

        if (StabilizeButton != null)
        {
            StabilizeButton.onClick.AddListener(IncreaseDNA);
        }

        if (RecycleButton != null)
        {
            RecycleButton.onClick.AddListener(RecycleDNA);
        }
    }

    void Update()
    {
        GameObject.Find("GameplayCanvas/GMCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genetic Material: " + geneticMaterial.amount.ToString();
        GameObject.Find("GameplayCanvas/DNACount").GetComponent<TMPro.TextMeshProUGUI>().text = "DNA: " + DNA.amount.ToString();
        GameObject.Find("GameplayCanvas/GenomeCount").GetComponent<TMPro.TextMeshProUGUI>().text = "Genomes: " + genome.amount.ToString();
        GameObject.Find("GameplayCanvas/ResearchLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Research: " + researchLevel.ToString();
    }

    void IncreaseGeneticMaterial()
    {
        //geneticMaterial.amount += geneticMaterial.rate;
        Debug.Log("Currency increased: " + geneticMaterial.amount);
    }

    void IncreaseDNA()
    {
        DNA.amount += 1;
        Debug.Log("DNA increased: " + DNA.amount);
    }

    void IncreaseGenome()
    {
        if (geneticMaterial.amount < _genomeCost)
        {
            Debug.Log("Not enough currency to synthesize");
            return;
        }

        geneticMaterial.amount -= _genomeCost;
        genome.amount += 1;
        Debug.Log("Genome increased: " + genome.amount);
    }

    void IncreaseResearch()
    {
        if (geneticMaterial.amount < _researchCost)
        {
            Debug.Log("Not enough genetic material to research");
            return;
        }

        geneticMaterial.amount -= _researchCost;
        researchLevel += 1;
        Debug.Log("Research increased: " + researchLevel);
    }

    void RecycleDNA()
    {
        if (DNA.amount < 1)
        {
            Debug.Log("Not enough DNA to recycle");
            return;
        }

        geneticMaterial.amount += _DNACost;
        DNA.amount -= 1;
        Debug.Log("DNA recycled");
    }
}
