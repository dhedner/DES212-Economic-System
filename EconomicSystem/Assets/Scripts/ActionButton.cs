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
}

public class ActionButton : MonoBehaviour
{
    public TextMeshProUGUI Label;
    public TextMeshProUGUI Description;
    public Button UIButton;
    public Action GameplayAction;
    public List<ActionCost> ActionButtonCosts;

    // Get currency manager from parents
    private CurrencyManager currencyManager;

    void Awake()
    {
        // Initialize components
        UIButton.onClick.AddListener(OnButtonClick);

        currencyManager = GetComponentInParent<CurrencyManager>();
    }

    public void Initialize(string label, Action action, List<ActionCost> cost)
    {
        Label.text = label;
        GameplayAction = action;
        ActionButtonCosts = cost;
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
        if (GetComponent<LevelBasedCostChanger>() != null)
        {
            return;
        }
        if (currencyManager.CanAfford(ActionButtonCosts))
        {
            GameplayAction?.Invoke();
            currencyManager.SpendCurrency(ActionButtonCosts);
        }
    }

    public void Show()
    {
        SetButtonVisible(true);
    }

    public void Hide()
    {
        SetButtonVisible(false);
    }

    private void SetButtonOpacity(float alpha)
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
            if (child != null)
            {
                child.gameObject.SetActive(visible);
            }
        }
    }
}

