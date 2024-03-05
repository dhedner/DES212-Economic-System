using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    public float TimeUntilFade = 0.0f;
    public float TimeToFade = 1.0f;

    private float _timeElapsed = 0.0f;

    void Update()
    {
        TimeUntilFade -= Time.deltaTime;

        if (TimeUntilFade > 0.0f || _timeElapsed > TimeToFade)
        {
            return;
        }

        Color fadingColor = GetComponent<TextMeshPro>().color;
        fadingColor.a = Mathf.Clamp(1.0f - (_timeElapsed / TimeToFade), 0.0f, 1.0f);
        GetComponent<TextMeshPro>().color = fadingColor;

        _timeElapsed += Time.deltaTime;
    }
}
