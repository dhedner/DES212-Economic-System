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

    public List<ActionCost> ActionButtonCosts
    {
        get
        {
            return _actionButtonCosts;
        }
        set
        {
            _actionButtonCosts = value;
            UpdateButtonState();
        }
    }
    
    [SerializeField]
    private List<ActionCost> _actionButtonCosts;
    
    public Predicate<ActionButton> EnableCondition
    {
        get
        { 
            return _enableCondition;
        }
        set 
        {
            _enableCondition = value;
            UpdateButtonState();
        }
    }
    private Predicate<ActionButton> _enableCondition;

    private HoverDialogue hoverDialogue;
    private ActionTracker actionTracker;
    private CurrencyManager currencyManager
    {
        get 
        {
            if (_currencyManager == null)
            {
                _currencyManager = GetComponentInParent<CurrencyManager>();
            }

            return _currencyManager;
        }
        set
        {
            _currencyManager = value;
        }
    }
    private CurrencyManager _currencyManager;

    public bool IsClickable
    {
        get
        {
            return UIButton.interactable && UIButton.IsActive() && GetComponentInParent<Phase>().isActive;
        }
    }

    ActionButton()
    {
        _enableCondition = DefaultEnableCondition;

        if (_actionButtonCosts == null)
        {
            _actionButtonCosts = new List<ActionCost>();
        }
    }

    void Start()
    {
        // Initialize components
        UIButton.onClick.AddListener(OnButtonClick);

        hoverDialogue = GetComponent<HoverDialogue>();
        actionTracker = GetComponentInParent<ActionTracker>();

        UpdateButtonState();
    }

    public void OnCurrencyChanged()
    {
        UpdateButtonState();
    }

    public void OnGameOver()
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        bool isEnabled = EnableCondition(this);

        UIButton.interactable = isEnabled;
        SetButtonOpacity(isEnabled ? 1.0f : 0.3f);
    }

    private bool DefaultEnableCondition(ActionButton button)
    {
        return button.currencyManager.CanAfford(button.ActionButtonCosts);
    }

    private void OnButtonClick()
    {
        if (!currencyManager.CanAfford(ActionButtonCosts))
        {
            return;
        }

        actionTracker.NotifyAction(AssignedGameplayButton);

        currencyManager.SpendCurrency(ActionButtonCosts);

        if (TryGetComponent(out CostChanger costChanger))
        {
            costChanger.TriggerCostChange();
        }

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

