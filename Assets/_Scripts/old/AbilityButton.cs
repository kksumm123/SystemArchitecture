using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AbilityType
{
    None,
    Magnetic,
    Dash,
}

public class AbilityButton : MonoBehaviour
{
    public AbilityType abilityType;
    Image abilityImage;
    void Start()
    {
        if (abilityType == AbilityType.None)
            Debug.LogError($"{transform} : abilityType 설정해줘야해 !");

        abilityImage = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(() => UseAbility());
    }

    float useableTime;
    [SerializeField] float coolTime = 10;
    void UseAbility()
    {
        if (GameManager.Instance.GameState != GameStateType.Playing)
            return;

        if (useableTime < Time.time)
        {
            useableTime = Time.time + coolTime;
            StartCoroutine(CoolTimeCo());
            StartCoroutine(UseAbilityCo());
        }
    }

    IEnumerator CoolTimeCo()
    {
        var endTime = Time.time + coolTime;
        while (Time.time < endTime)
        {
            abilityImage.fillAmount = 1 - ((endTime - Time.time) / coolTime);
            yield return null;
        }
        abilityImage.fillAmount = 1;
    }

    [SerializeField] float duration = 3;
    IEnumerator UseAbilityCo()
    {
        AbilityBase ability = abilityType.GetAblity();

        ability.Activate();
        yield return new WaitForSeconds(duration);
        ability.DeActivate();
    }
}
