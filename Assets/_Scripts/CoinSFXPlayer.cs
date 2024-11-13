using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSFXPlayer : MonoBehaviour
{
    public static CoinSFXPlayer Instance;
    void Awake() => Instance = this;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        audioSource.Play();
    }
}
