using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUI : BaseUI
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Text readyText;

    private Tween _handle;

    protected override void OnInitialize()
    {
        Woony.Asserts(this,
            (canvasGroup, nameof(canvasGroup)),
            (readyText, nameof(readyText)));
    }

    public void SetReady(int readyTime, CancellationToken token)
    {
        Ready(readyTime, token).Forget();
    }

    private async UniTask Ready(int readyTime, CancellationToken token)
    {
        for (int i = readyTime; i > 0; i--)
        {
            readyText.text = i.ToString();
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        }

        readyText.text = "G O ! ! !";

        _handle = DOTween.To(() => 1, x => canvasGroup.alpha = x, 0, 1)
            .SetUpdate(true)
            .SetLink(gameObject);
    }

    public void ForceStop()
    {
        if (_handle.IsTweenActive()) _handle.Kill();
        canvasGroup.alpha = 0;
    }
}