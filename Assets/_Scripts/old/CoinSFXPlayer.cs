using UnityEngine;

public class CoinSFXPlayer : MonoSingleton<CoinSFXPlayer>
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}