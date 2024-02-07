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
    private ConversionChanger conversionChanger;
    //private CurrencyManager currencyManager;
    void Start()
    {
        panel.SetActive(false);
        actionButton = GetComponent<ActionButton>();
        //if (isUpgrade)
        //{
        //    conversionChanger = GetComponent<ConversionChanger>();
        //}
        //currencyManager = GetComponentInParent<CurrencyManager>();
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
                costsString += $"Gain + {-cost.Amount} {cost.currencyType}\n";
            }
            else
            {
                costsString += $"Cost - {cost.Amount} {cost.currencyType}\n";
            }
        }

        //if (isUpgrade)
        //{
        //    conversionChanger.costs[conversionChanger.currentLevel].UpgradeCost[0] = cost;
        //    costsString += $"Upgrade Cost - {cost.Amount} {cost.CurrencyType}\n";
        //}

        infoText.text = $"{dialogText}\n\n{costsString}";
        //StartCoroutine(RefreshBox());
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
