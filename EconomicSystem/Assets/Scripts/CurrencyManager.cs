using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public Currency currency;
    public Button increaseButton;

    void Start()
    {
        if (increaseButton != null)
        {
            increaseButton.onClick.AddListener(IncreaseCurrency);
        }
    }

    void IncreaseCurrency()
    {
        currency.amount += 1;
        Debug.Log("Currency increased: " + currency.amount);
    }
}
