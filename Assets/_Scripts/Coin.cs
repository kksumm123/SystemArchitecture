using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 10;
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D circleCol;

    [SerializeField] float touchAnimationTime = 0.5f;

    private Vector3 _originPos;

    private void Awake()
    {
        Woony.Asserts(this,
            (animator, nameof(animator)),
            (circleCol, nameof(circleCol)));

        _originPos = transform.localPosition;
        gameObject.SafeSetActive(true);
    }

    private void OnEnable()
    {
        transform.localPosition = _originPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(CoinTouchCo());
    }

    private IEnumerator CoinTouchCo()
    {
        CoinSFXPlayer.Instance.PlaySound();
        PhaseArchitecture.GameManager.Instance.AddScore(coinValue);
        //MagneticAbility.attachedCoins.Remove(transform);

        circleCol.enabled = false;
        animator.Play("CoinTouch");
        yield return new WaitForSeconds(touchAnimationTime);
        gameObject.SafeSetActive(false);
    }
}