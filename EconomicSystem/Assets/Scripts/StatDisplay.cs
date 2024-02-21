using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite purpleSprite;
    public TextMeshProUGUI genomeText;
    public TextMeshProUGUI turnText;
    public ActionTracker actionTracker;

    private CurrencyManager currencyManager;
    private CurrencyType mostAlignedType;
    private Image sprite;

    public void Start()
    {
        currencyManager = GetComponentInParent<CurrencyManager>();
        sprite = GetComponent<Image>();
    }

    public void OnGameOver()
    {
        RefreshText();
    }

    public void RefreshText()
    {
        //string statsString = "Most Aligned Genome:";
        mostAlignedType = currencyManager.MostAlignedType;

        sprite.sprite = mostAlignedType switch
        {
            CurrencyType.RedGenome => redSprite,
            CurrencyType.GreenGenome => greenSprite,
            CurrencyType.PurpleGenome => purpleSprite,
            _ => null
        };

        string mostAlignedTypeString = "";
        if (mostAlignedType == CurrencyType.RedGenome)
        {
            mostAlignedTypeString = "Red";
        }
        else if (mostAlignedType == CurrencyType.GreenGenome)
        {
            mostAlignedTypeString = "Green";
        }
        else if (mostAlignedType == CurrencyType.PurpleGenome)
        {
            mostAlignedTypeString = "Purple";
        }

        genomeText.text = $"Most Aligned Genome: {mostAlignedTypeString}";
        turnText.text = $"Turns Taken: {actionTracker.GetTotalClickCount()}";
    }
}
