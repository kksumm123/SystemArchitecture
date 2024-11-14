using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUI : MonoBehaviour
{
    public static ReadyUI Instance;

    CanvasGroup canvasGroup;
    Text readyText;
    void Awake()
    {
        Instance = this;

        canvasGroup = GetComponent<CanvasGroup>();
        readyText = GetComponentInChildren<Text>();
    }

    internal void SetReady(int readyTime)
    {
        StartCoroutine(ReadyCo(readyTime));
    }

    IEnumerator ReadyCo(int readyTime)
    {
        for (int i = readyTime; i > 0; i--)
        {
            readyText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        readyText.text = "G O ! ! !";

        DOTween.To(() => 1, x => canvasGroup.alpha = x, 0, 1)
            .SetUpdate(true).SetLink(gameObject);
    }
}
