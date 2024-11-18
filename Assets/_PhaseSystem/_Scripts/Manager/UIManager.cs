namespace PhaseArchitecture
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public enum UIParentType
    {
        Under, Main, Popup
    }

    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private Transform UnderRoot;
        [SerializeField] private Transform MainRoot;
        [SerializeField] private Transform PopupRoot;

        private Dictionary<Type, string> _stringKeys = new();
        private Dictionary<string, BaseUI> _uis = new();

        public T GetUI<T>(UIParentType parentType) where T : BaseUI
        {
            var uiType = typeof(T);
            if (!_stringKeys.ContainsKey(uiType))
            {
                _stringKeys[uiType] = uiType.ToString();
            }

            if (!_uis.ContainsKey(_stringKeys[uiType]))
            {
                _uis[_stringKeys[uiType]] = Instantiate(ResourceManager.Instance.UI.GetItem(_stringKeys[uiType]));
            }

            var ui = _uis[_stringKeys[uiType]];
            SetParent(ui, parentType);
            return ui as T;
        }

        private void SetParent(BaseUI ui, UIParentType parentType)
        {
            if (ui == null)
            {
                Debug.LogError($"target is null. Type = {parentType}");
                return;
            }

            switch (parentType)
            {
                case UIParentType.Under:
                    ui.transform.SetParent(UnderRoot);
                    break;
                case UIParentType.Main:
                    ui.transform.SetParent(MainRoot);
                    break;
                case UIParentType.Popup:
                    ui.transform.SetParent(PopupRoot);
                    break;
                default:
                    Debug.LogError($"예외처리 필요. Type = {parentType}");
                    break;
            }

            ui.transform.localPosition = Vector3.zero;
        }
    }
}