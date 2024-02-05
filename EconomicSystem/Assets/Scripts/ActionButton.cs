using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[Serializable]
public struct ActionCost
{
    [SerializeField] public string CurrencyType;
    [SerializeField] public int Amount;

    public ActionCost(string currencyType, int amount)
    {
        CurrencyType = currencyType;
        Amount = amount;
    }

    public override string ToString()
    {
        return $"{CurrencyType}: {Amount}";
    }
}

public class ActionButton : MonoBehaviour
{
    public TextMeshProUGUI Label;
    public TextMeshProUGUI Description;
    public Button UIButton;
    public List<ActionCost> ActionButtonCosts;
    private HoverDialogue hoverDialogue;

    // Get currency manager from parents
    private CurrencyManager currencyManager;

    void Awake()
    {
        // Initialize components
        UIButton.onClick.AddListener(OnButtonClick);

        currencyManager = GetComponentInParent<CurrencyManager>();
        hoverDialogue = GetComponent<HoverDialogue>();
    }

    void Update()
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        bool canAfford = currencyManager.CanAfford(ActionButtonCosts);
        UIButton.interactable = canAfford;
        SetButtonOpacity(canAfford ? 1.0f : 0.3f);
    }

    private void OnButtonClick()
    {
        if (!currencyManager.CanAfford(ActionButtonCosts))
        {
            return;
        }
        
        currencyManager.SpendCurrency(ActionButtonCosts);

        if (TryGetComponent(out CostChanger costChanger))
        {
            costChanger.TriggerCostChange();
        }

        hoverDialogue.RefreshText();
    }

    public void Show()
    {
        SetButtonVisible(true);
    }

    public void Hide()
    {
        SetButtonVisible(false);
    }

    public void SetButtonOpacity(float alpha)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    private void SetButtonVisible(bool visible)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.enabled = visible;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child != null && child.tag != "InactiveByDefault")
            {
                child.gameObject.SetActive(visible);
            }
        }
    }
}

