using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[Serializable]
public struct ActionCost
{
    [SerializeField] public CurrencyType currencyType;
    [SerializeField] public int Amount;

    public ActionCost(CurrencyType currency, int amount)
    {
        currencyType = currency;
        Amount = amount;
    }

    public override string ToString()
    {
        return $"{currencyType}: {Amount}";
    }
}

public class ActionButton : MonoBehaviour
{
    public TextMeshProUGUI Label;
    public GameplayButton AssignedGameplayButton;
    public Button UIButton;
    public List<ActionCost> ActionButtonCosts;

    private HoverDialogue hoverDialogue;
    private CurrencyManager currencyManager;
    private ActionTracker actionTracker;

    public bool IsClickable
    {
        get
        {
            return UIButton.interactable && UIButton.IsActive() && GetComponentInParent<Phase>().isActive;
        }
    }

    void Start()
    {
        // Initialize components
        UIButton.onClick.AddListener(OnButtonClick);

        hoverDialogue = GetComponent<HoverDialogue>();
        currencyManager = GetComponentInParent<CurrencyManager>();
        actionTracker = GetComponentInParent<ActionTracker>();

        UpdateButtonState();
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

        actionTracker.NotifyAction(AssignedGameplayButton);

        hoverDialogue.RefreshText();

        //BroadcastMessage("OnTurnTaken");
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

        Label.color = new Color(Label.color.r, Label.color.g, Label.color.b, alpha);
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

