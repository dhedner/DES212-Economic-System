using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverDialogue : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI infoText;
    public string dialogText;
    //public bool isUpgrade;

    private ActionButton actionButton;
    private Dictionary<CurrencyType, string> currencyNameStrings = new Dictionary<CurrencyType, string>()
    {
        {CurrencyType.GeneticMaterial, "Genetic Material"},
        {CurrencyType.DNA, "DNA"},
        {CurrencyType.Genome, "Genome"},
        {CurrencyType.RedGenome, "Red Genome"},
        {CurrencyType.GreenGenome, "Green Genome"},
        {CurrencyType.PurpleGenome, "Purple Genome"},
        {CurrencyType.CellClusters, "Cell Clusters"},
        {CurrencyType.Research, "Research"},
        {CurrencyType.DummyCurrency, "Dummy Currency"}
    };

    void Start()
    {
        panel.SetActive(false);
        actionButton = GetComponent<ActionButton>();
    }

    public void RefreshText()
    {
        string costsString = "";
        foreach (var cost in actionButton.ActionButtonCosts)
        {
            if (cost.Amount >= int.MaxValue)
            {
                costsString += $"Max Reached\n";
            }
            else if (cost.Amount < 0)
            {
                costsString += $"Gain + {-cost.Amount} {currencyNameStrings[cost.currencyType]}\n";
            }
            else
            {
                costsString += $"Cost - {cost.Amount} {currencyNameStrings[cost.currencyType]}\n";
            }
        }

        infoText.text = $"{dialogText}\n\n{costsString}";
        RefreshBox();
    }

    public IEnumerator RefreshBox()
    {
        yield return new WaitForSeconds(0.01f);
        // Get the rect transform of the panel
        RectTransform rectTransform = panel.GetComponent<RectTransform>();
        float height = rectTransform.sizeDelta.y;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, height - 50);
    }

    public void OnButtonHover()
    {
        panel.SetActive(true);
        RefreshText();
    }

    public void OnButtonHoverExit()
    {
        panel.SetActive(false);
    }
}
