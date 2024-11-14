using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    // protected static WoonyMethods.CustomEase showEase;
    // protected static WoonyMethods.CustomEase closeEase;
    protected virtual bool isPopup { get; set; } = false;
    protected bool isInit = false;

    public void Initialize()
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);

        transform.SetAsLastSibling();

        // if (showEase == null || closeEase == null)
        // {
        //     WoonyMethods.AlertError($"{nameof(BaseUI)} ease들 할당 필요.");
        // }

        OnInitialize();
        isInit = true;
    }

    protected virtual void OnInitialize() { }

    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }
}