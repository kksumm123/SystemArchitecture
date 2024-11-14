using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticAbility : AbilityBase
{
    public static MagneticAbility Instance;
    void Awake() => Instance = this;

    Transform PlayerTr => Player.Instance.transform;
    CircleCollider2D circleCol;
    void Start()
    {
        circleCol = GetComponent<CircleCollider2D>();
        DeActivate();
    }

    Vector3 dir;
    float magneticPower = 0.3f;
    void Update()
    {
        if (GameManager.Instance.GameState != GameStateType.Playing)
            return;

        foreach (var item in attachedCoins)
        {
            if (item.Key == null)
                continue;

            dir = PlayerTr.position - item.Key.position;
            dir.Normalize();
            item.Value.power += magneticPower;

            item.Key.Translate(item.Value.power * Time.deltaTime * dir);
        }
    }

    public override AbilityType GetAbilityType()
    {
        return AbilityType.Magnetic;
    }
    public override void Activate()
    {
        enabled = true;
        circleCol.enabled = true;
    }
    public override void DeActivate()
    {
        enabled = false;
        circleCol.enabled = false;
        attachedCoins.Clear();
    }

    public class MagneticPower
    {
        public float power = 0;
    }
    public static Dictionary<Transform, MagneticPower> attachedCoins = new Dictionary<Transform, MagneticPower>();
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled == false)
            return;

        if (collision.CompareTag("Coin"))
        {
            if (attachedCoins.ContainsKey(collision.transform))
                return;
            attachedCoins[collision.transform] = new MagneticPower();
        }
    }
}
