using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfo : MonoBehaviour
{
    public Text infoText; // Assign in inspector

    void Start()
    {
        this.gameObject.SetActive(false); // Hide initially
    }

    public void ShowInfo(string info)
    {
        infoText.text = info;
        AjustSize();
        this.gameObject.SetActive(true); // Show panel
    }

    public void HideInfo()
    {
        this.gameObject.SetActive(false); // Hide panel
    }

    private void AjustSize()
    {
        // Adjust size of panel to fit text
        RectTransform rt = this.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, infoText.preferredHeight + 10);
    }
}

