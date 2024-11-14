using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance;

    Text scoreValue;
    Text highScoreValue;
    int score;
    int highScore;
    readonly string highScoreKey = "HighScore";
    void Awake()
    {
        Instance = this;

        scoreValue = transform.Find("Current/Value").GetComponent<Text>();
        highScoreValue = transform.Find("High/Value").GetComponent<Text>();

        highScore = PlayerPrefs.GetInt(highScoreKey);

        scoreValue.text = score.ToString();
        highScoreValue.text = highScore.ToString();
    }
    void OnDestroy()
    {
        PlayerPrefs.SetInt(highScoreKey, highScore);
    }

    int oldScore;
    int oldHighScore;
    float scoreAnimationTime = 0.2f;
    TweenerCore<int, int, NoOptions> scoreHandle;
    TweenerCore<int, int, NoOptions> highScoreHandle;
    internal void AddScore(int value)
    {
        oldScore = score;
        score += value;
        KillAndAnimaitionText(scoreHandle, oldScore, scoreValue, score, scoreAnimationTime);

        if (highScore < score)
        {
            oldHighScore = highScore;
            highScore = score;
            KillAndAnimaitionText(highScoreHandle, oldHighScore, highScoreValue, highScore, scoreAnimationTime);
        }
    }

    void KillAndAnimaitionText(TweenerCore<int, int, NoOptions> handle
        , int getter, Text text, int endValue, float duration)
    {
        if (handle != null)
            handle.Kill();
        handle = DOTween.To(() => getter, x => text.text = x.ToString(), endValue, duration)
            .SetUpdate(true).SetLink(gameObject);
    }
}
