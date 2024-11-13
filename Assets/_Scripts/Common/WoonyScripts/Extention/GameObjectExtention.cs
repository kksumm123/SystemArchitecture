using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class GameObjectExtention
{
    public static bool IsSafeActive(this GameObject go)
    {
        return go != null && go.activeSelf;
    }

    public static bool IsSafeActive(this Transform tr)
    {
        return tr != null && tr.gameObject.activeSelf;
    }

    public static void SafeSetActive(this GameObject go, bool isActive)
    {
        if (go == null) return;
        go.SetActive(isActive);
    }

    public static void SafeSetActive(this Transform tr, bool isActive)
    {
        if (tr == null) return;
        tr.gameObject.SetActive(isActive);
    }

    public static void SafeSetActive(this Animation ani, bool isActive)
    {
        if (ani == null) return;
        ani.gameObject.SetActive(isActive);
    }

    public static void SafeSetActive(this MonoBehaviour go, bool isActive)
    {
        if (go == null) return;
        go.gameObject.SetActive(isActive);
    }

    public static void SafeSprite(this SpriteRenderer renderer, Sprite sprite)
    {
        if (renderer == null) return;
        renderer.sprite = sprite;
    }

    public static string SafeStringFormat(this string msg, params object[] objs)
    {
        try
        {
            return string.Format(msg, objs);
        }
        catch
        {
            Debug.LogError("SafeStringFormat : " + msg);
        }

        return string.Empty;
    }

    #region UGUI UI

    public static void SafeSetActive(this Canvas can, bool isActive)
    {
        if (can == null) return;
        can.gameObject.SetActive(isActive);
    }

    public static void SafeSliderValue(this Slider sl, float value)
    {
        if (sl == null) return;
        sl.value = value;
    }


    public static void SafeText(this TextMeshProUGUI mesh, string text)
    {
        if (mesh == null) return;
        mesh.text = text;
    }

    public static void SafeTextFormat(this TextMeshProUGUI mesh, string text, params object[] objs)
    {
        if (mesh == null) return;
        mesh.text = SafeStringFormat(text, objs);
    }

    public static void SafeColor(this TextMeshProUGUI mesh, Color color)
    {
        if (mesh == null) return;
        mesh.color = color;
    }

    public static void SafeText(this TextMeshPro mesh, string text)
    {
        if (mesh == null) return;
        mesh.text = text;
    }

    public static void SafeTextFormat(this TextMeshPro mesh, string text, params object[] objs)
    {
        if (mesh == null) return;
        mesh.text = SafeStringFormat(text, objs);
    }

    public static void SafeColor(this TextMeshPro mesh, Color color)
    {
        if (mesh == null) return;
        mesh.color = color;
    }

    public static void SafeSprite(this Image img, Sprite sprite)
    {
        if (img == null) return;
        img.sprite = sprite;
    }

    public static void SafeFillAmount(this Image img, float value)
    {
        if (img == null) return;
        img.fillAmount = value;
    }

    public static void SafeColor(this Image img, Color color)
    {
        if (img == null) return;
        img.color = color;
    }

    public static void SafeInteractable(this Button button, bool value)
    {
        if (button == null) return;
        button.interactable = value;
    }

    #endregion
}