using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaystyleHoverDialogue : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI infoText;
    public string dialogText;
    //public bool isUpgrade;

    private ActionButton actionButton;

    void Start()
    {
        panel.SetActive(false);
        actionButton = GetComponent<ActionButton>();
    }

    public void RefreshText()
    {
        infoText.text = $"{dialogText}";
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
