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
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI genomeText;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI monsterStats;
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
            monsterName.text = "DEMONIC BRUTE";
        }
        else if (mostAlignedType == CurrencyType.GreenGenome)
        {
            mostAlignedTypeString = "Green";
            monsterName.text = "VENOMOUS SPAWN";
        }
        else if (mostAlignedType == CurrencyType.PurpleGenome)
        {
            mostAlignedTypeString = "Purple";
            monsterName.text = "PSYCHO PHANTOM";
        }

        PickMonsterStats(mostAlignedType);

        genomeText.text = $"Most Aligned Genome: {mostAlignedTypeString}";
        turnText.text = $"Turns Taken: {actionTracker.GetTotalClickCount()}";
    }

    public void PickMonsterStats(CurrencyType type)
    {
        if (type == CurrencyType.RedGenome)
        {
            monsterStats.text = "Vitality\n 120\n\n" +
                                "Strength\n 30\n\n" +
                                "Fortitude\n 50\n\n" +
                                "Magic\n 10\n\n" +
                                "Cunning\n 15\n\n";
        }
        else if (type == CurrencyType.GreenGenome)
        {
            monsterStats.text = "Vitality\n 80\n\n" +
                                "Strength\n 15\n\n" +
                                "Fortitude\n 20\n\n" +
                                "Magic\n 20\n\n" +
                                "Cunning\n 60\n\n";
        }
        else if (type == CurrencyType.PurpleGenome)
        {
            monsterStats.text = "Vitality\n 65\n\n" +
                                "Strength\n 10\n\n" +
                                "Fortitude\n 30\n\n" +
                                "Magic\n 50\n\n" +
                                "Cunning\n 80\n\n";
        }
    }
}
