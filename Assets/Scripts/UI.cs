using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UI : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 finishPosition;
    [SerializeField] private GameObject[] windows;
    [SerializeField] private RectTransform window3;

    private void Start()
    {
        startPosition = Vector2.up * -800;
        finishPosition = Vector2.zero;

        StartCoroutine(Timer());
    }

    public void ShowWindow(RectTransform rect)
    {
        rect.gameObject.SetActive(true);

        StartCoroutine(ShowHideCor(rect, startPosition, finishPosition, false));
    }

    public void HideWindow(RectTransform rect) =>
        StartCoroutine(ShowHideCor(rect, finishPosition, startPosition, true));

    public void StartTimer() => StartCoroutine(Timer());

    IEnumerator Timer()
    {
        float timer = Random.Range(10f, 20f);

        for (float i = timer; i > 0; i -= Time.deltaTime)
            yield return null;

        yield return new WaitUntil(() => windows.All(x => x.activeSelf == false));

        ShowWindow(window3);
    }

    IEnumerator ShowHideCor(RectTransform rect, Vector2 startPos, Vector2 finishPos, bool hide)
    {
        for (float i = 0; i < 1; i += Time.deltaTime * 2)
        {
            rect.anchoredPosition = Vector2.Lerp(startPos, finishPos, easingSmoothSquared(i));
            yield return null;
        }

        rect.anchoredPosition = finishPos;

        if (hide) rect.gameObject.SetActive(false);
    }

    private float easingSmoothSquared(float x)
        => x < .5f ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
}