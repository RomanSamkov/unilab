using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab : MonoBehaviour
{
    RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void Hide()
    {
        StartCoroutine(HideIE());
    }

    IEnumerator HideIE()
    {
        float t = 0;

        while (t < 1)
        {
            t+=Time.deltaTime;
            float i = (Mathf.Cos(Mathf.PI * t + Mathf.PI) + 1) / 2; //(cos(3.14*x+3.14)+1)/2

            rt.anchoredPosition = new Vector2(Mathf.Lerp(0, -1200, i), -150);
            yield return null;
        }
    }

    public void Show()
    {
        StartCoroutine(StartIE());
    }

    IEnumerator StartIE()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            float i = (Mathf.Cos(Mathf.PI * t + Mathf.PI) + 1) / 2; //(cos(3.14*x+3.14)+1)/2

            rt.anchoredPosition = new Vector2(Mathf.Lerp(-1200, 0, i), -150);
            yield return null;
        }
    }
}
