using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    public static VolumeUI Instance;
    void Awake() => Instance = this;

    string bgmSliderKey = "BGMSliderKey";
    string sfxSliderKey = "SFXSliderKey";
    Slider bgmSlider;
    Slider sfxSlider;
    AudioSource bgmPlayer;
    AudioSource coinSFXPlayer;
    void Start()
    {
        bgmSlider = transform.Find("BGM/Slider").GetComponent<Slider>();
        sfxSlider = transform.Find("SFX/Slider").GetComponent<Slider>();
        bgmPlayer = GameObject.Find("BGMPlayer").GetComponent<AudioSource>();
        coinSFXPlayer = GameObject.Find("CoinSFXPlayer").GetComponent<AudioSource>();
        bgmSlider.onValueChanged.AddListener((x) => bgmPlayer.volume = x);
        sfxSlider.onValueChanged.AddListener((x) => coinSFXPlayer.volume = x);

        bgmSlider.value = PlayerPrefs.GetFloat(bgmSliderKey, 0.6f);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxSliderKey, 1f);

        CloseUI();
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(bgmSliderKey, bgmSlider.value);
        PlayerPrefs.SetFloat(sfxSliderKey, sfxSlider.value);
    }

    public void ShowUI()
    {
        GameManager.Instance.GameState = GameStateType.Menu;
        gameObject.SetActive(true);

        var localPos = transform.localPosition;
        localPos.y += 600;
        transform.localPosition = localPos;
        transform.DOKill();
        transform.DOLocalMoveY(0, 1)
                 .SetEase(Ease.OutBounce)
                 .SetUpdate(true)
                 .SetLink(gameObject);
    }
    public void CloseUI()
    {
        GameManager.Instance.GameState = GameStateType.MenuOut;
        gameObject.SetActive(false);
    }
}
