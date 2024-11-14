using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverAndClearUI : MonoBehaviour
{
    public static GameOverAndClearUI Instance;
    readonly string gameOverString = "GAME OVER";
    readonly string stageClearString = "Stage Clear";
    Text noticeText;
    Text tabToContinueText;
    void Awake()
    {
        Instance = this;
        transform.Find("RestartButton").GetComponent<Button>()
                                        .onClick.AddListener(() => RestartButton());

        noticeText = transform.Find("Text").GetComponent<Text>();
        tabToContinueText = transform.Find("TabToContinue").GetComponent<Text>();

        isRestartable = false;
        tabToContinueText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public void ShowUI(GameStateType gameStateType)
    {
        gameObject.SetActive(true);
        isRestartable = false;

        switch (gameStateType)
        {
            case GameStateType.GameOver:
                noticeText.text = gameOverString;
                break;
            case GameStateType.StageClear:
                noticeText.text = stageClearString;
                break;
        }

        var localPos = noticeText.rectTransform.localPosition;
        localPos.y += 600;
        noticeText.rectTransform.localPosition = localPos;
        noticeText.rectTransform.DOKill();
        noticeText.rectTransform.DOLocalMoveY(0, 2)
                    .SetEase(Ease.OutBounce)
                    .SetUpdate(true)
                    .SetLink(gameObject)
                    .OnComplete(() => PunchScaleTabToContinue());
    }

    bool isRestartable = false;
    void RestartButton()
    {
        if (isRestartable == true)
            GameManager.Instance.ReStartGame();
    }

    Vector3 punchScale = new Vector3(0.1f, 0.1f, 0.1f);
    private void PunchScaleTabToContinue()
    {
        isRestartable = true;
        tabToContinueText.gameObject.SetActive(true);
        tabToContinueText.rectTransform.DOPunchScale(punchScale, 1, 1, 0.5f)
                         .SetLoops(-1, LoopType.Yoyo)
                         .SetUpdate(true)
                         .SetLink(tabToContinueText.gameObject);
    }
}
