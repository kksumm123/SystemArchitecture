using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    private readonly string HIGH_SCORE_KEY = "HighScore";

    [SerializeField] private Text scoreValue;
    [SerializeField] private Text highScoreValue;

    private int _score;
    private int _highScore;

    private int _oldScore;
    private int _oldHighScore;
    private float _scoreAnimationTime = 0.2f;
    private TweenerCore<int, int, NoOptions> _scoreHandle;
    private TweenerCore<int, int, NoOptions> _highScoreHandle;

    protected override void OnInitialize()
    {
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);

        scoreValue.text = _score.ToString();
        highScoreValue.text = _highScore.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
    }

    public void AddScore(int value)
    {
        _oldScore = _score;
        _score += value;
        KillAndAnimaitionText(ref _scoreHandle, _oldScore, scoreValue, _score, _scoreAnimationTime);

        if (_highScore < _score)
        {
            _oldHighScore = _highScore;
            _highScore = _score;
            KillAndAnimaitionText(ref _highScoreHandle, _oldHighScore, highScoreValue, _highScore, _scoreAnimationTime);
        }

        void KillAndAnimaitionText(ref TweenerCore<int, int, NoOptions> handle, int getter, Text text, int endValue, float duration)
        {
            if (handle.IsTweenActive()) handle.Kill();

            handle = DOTween.To(
                    () => getter,
                    x => text.text = x.ToString(),
                    endValue,
                    duration)
                .SetUpdate(true)
                .SetLink(gameObject);
        }
    }
}