using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonInteractivityController : MonoBehaviour
{
    //[SerializeField] private Button generateButton;
    //[SerializeField] private Button researchButton;

    //private Button button;
    //[SerializeField] private Currency costType;
    //[SerializeField] private GameplayAction _action;
    //// Get button component from the GameObject this script is attached to
    //private CurrencyManager currencyManager;

    //void Awake()
    //{
    //    currencyManager = FindObjectOfType<CurrencyManager>();
    //    if (currencyManager == null)
    //    {
    //        Debug.LogError("CurrencyManager script not found in the scene.");
    //    }

    //    button = GetComponent<Button>();
    //    if (button == null)
    //    {
    //        Debug.LogError("TextMeshPro-Button component not found on the GameObject.");
    //    }

    //    SetButtonVisible(false);
    //}

    //void Update()
    //{
    //    // Check if the button should be interactable
    //    if (CanBePushed(_action))
    //    {
    //        button.interactable = true;
    //        SetButtonOpacity(1.0f); // Set to full opacity
    //    }
    //    else
    //    {
    //        button.interactable = false;
    //        SetButtonOpacity(0.3f); // Set to half opacity
    //    }

    //    // Check if the button should be visible at all
    //    if (_action.phaseThreshold > currencyManager.researchLevelCurrency.amount)
    //    {
    //        SetButtonVisible(false);
    //    }
    //    else
    //    {
    //        SetButtonVisible(true);
    //    }
    //}

    //private void SetButtonOpacity(float alpha)
    //{
    //    Image image = button.GetComponent<Image>();
    //    if (image != null)
    //    {
    //        Color color = image.color;
    //        color.a = alpha;
    //        image.color = color;
    //    }
    //}

    //private void SetButtonVisible(bool visible)
    //{
    //    Image image = button.GetComponent<Image>();
    //    if (image != null)
    //    {
    //        image.enabled = visible;
    //    }

    //    for (int i = 0; i < button.transform.childCount; i++)
    //    {
    //        Transform child = button.transform.GetChild(i);
    //        if (child != null)
    //        {
    //            child.gameObject.SetActive(visible);
    //        }
    //    }
    //}
    //bool CanBePushed(GameplayAction action)
    //{
    //    if (costType.amount < action.cost)
    //    {
    //        return false;
    //    }

    //    return true;
    //}


}
