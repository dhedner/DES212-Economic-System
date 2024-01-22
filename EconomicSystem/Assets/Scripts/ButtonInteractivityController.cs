using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonInteractivityController : MonoBehaviour
{
    [SerializeField] int levelThreshold;
    [SerializeField] Currency costType;
    [SerializeField] GameplayAction action;

    private Button _button;
    private CurrencyManager currencyManager;

    void Awake()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager == null)
        {
            Debug.LogError("CurrencyManager script not found in the scene.");
        }

        // Automatically get the Button component attached to the same GameObject
        _button = GetComponent<Button>();
        if (_button == null)
        {
            Debug.LogError("TextMeshPro-Button component not found on the GameObject.");
        }

        SetButtonVisible(false);
    }

    void Update()
    {
        // Check if the button should be interactable
        if (CanBePushed(costType, action))
        {
            _button.interactable = true;
            SetButtonOpacity(1.0f); // Set to full opacity
        }
        else
        {
            _button.interactable = false;
            SetButtonOpacity(0.3f); // Set to half opacity
        }

        // Check if the button should be visible at all
        if (levelThreshold > currencyManager.researchLevel)
        {
            SetButtonVisible(false);
        }
        else
        {
            SetButtonVisible(true);
        }
    }

    private void SetButtonOpacity(float alpha)
    {
        Image image = _button.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    private void SetButtonVisible(bool visible)
    {
        Image image = _button.GetComponent<Image>();
        if (image != null)
        {
            image.enabled = visible;
        }

        for (int i = 0; i < _button.transform.childCount; i++)
        {
            Transform child = _button.transform.GetChild(i);
            if (child != null)
            {
                child.gameObject.SetActive(visible);
            }
        }
    }
    bool CanBePushed(Currency costType, GameplayAction action)
    {
        if (costType.amount < action.cost)
        {
            return false;
        }

        return true;
    }
}
