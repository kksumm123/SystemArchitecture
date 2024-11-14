using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{
    [SerializeField] int coinValue = 10;
    Animator animator;
    CircleCollider2D circleCol;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        circleCol = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(CoinTouchCo());
    }

    float touchAnimationTime = 0.5f;
    IEnumerator CoinTouchCo()
    {
        CoinSFXPlayer.Instance.PlaySound();
        GameManager.Instance.AddScore(coinValue);
        MagneticAbility.attachedCoins.Remove(transform);

        circleCol.enabled = false;
        animator.Play("CoinTouch");
        yield return new WaitForSeconds(touchAnimationTime);
        Destroy(gameObject);
    }
}
