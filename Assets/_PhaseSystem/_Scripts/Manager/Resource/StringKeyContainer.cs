namespace PhaseArchitecture
{
    using System.Collections.Generic;
    using UnityEngine;

    public class StringKeyContainer<DATA_TYPE> : AddressableDataContainer<string, DATA_TYPE>
    {
        // key : original Key, value : lower Key
        private readonly Dictionary<string, string> _cachedKeys = new();
        public StringKeyContainer(string label, bool isLazyLoad) : base(label, isLazyLoad) { }

        protected override string ConvertKey(string stringKey)
        {
            return GetLowerKey(stringKey);
        }

        protected string GetLowerKey(string stringKey)
        {
            if (stringKey == null)
            {
                Debug.LogError($"{this}.{nameof(GetLowerKey)}: key is null");
                return null;
            }

            if (!_cachedKeys.ContainsKey(stringKey))
            {
                _cachedKeys[stringKey] = stringKey.ToLower();
            }

            return _cachedKeys[stringKey];
        }

        public override DATA_TYPE GetItem(string originalKey)
        {
            return base.GetItem(GetLowerKey(originalKey));
        }
    }
}