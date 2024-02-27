using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostFeedback : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public float floatSpeed = 1.5f;
    public float fadeDuration = 1f;
    public float moveDistance = 1f;

    private ActionButton actionButton;
    private Vector3 LastMousePosition;
    private Dictionary<CurrencyType, string> currencyNameStrings = new Dictionary<CurrencyType, string>()
    {
        {CurrencyType.GeneticMaterial, "Genetic Material"},
        {CurrencyType.DNA, "DNA"},
        {CurrencyType.Genome, "Genome"},
        {CurrencyType.RedGenome, "Red Genome"},
        {CurrencyType.GreenGenome, "Green Genome"},
        {CurrencyType.PurpleGenome, "Purple Genome"},
        {CurrencyType.CellClusters, "Cell Clusters"},
        {CurrencyType.Research, "Research"},
        {CurrencyType.DummyCurrency, "Dummy Currency"}
    };

    public void Start()
    {
        actionButton = GetComponent<ActionButton>();
    }

    public void GenerateFeedback()
    {
        LastMousePosition = Input.mousePosition;
        GameObject textObj = Instantiate(floatingTextPrefab, LastMousePosition, Quaternion.identity, transform);
        TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
        CanvasGroup canvasGroup = textObj.GetComponent<CanvasGroup>();

        string costsString = "";
        foreach (var cost in actionButton.ActionButtonCosts)
        {
            if (cost.Amount < 0)
            {
                costsString += $"+ {-cost.Amount} {currencyNameStrings[cost.currencyType]}\n";
            }
            else
            {
                costsString += $"- {cost.Amount} {currencyNameStrings[cost.currencyType]}\n";
            }
        }

        text.text = $"{costsString}";
        StartCoroutine(FloatAndFade(textObj, canvasGroup));
    }

    private IEnumerator FloatAndFade(GameObject textObj, CanvasGroup canvasGroup)
    {
        Vector3 startPos = textObj.transform.position;
        Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            textObj.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / fadeDuration);
            canvasGroup.alpha = 1 - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(textObj);
    }
}
