namespace PlayerSystem
{
    using System.Collections.Generic;
    using PhaseArchitecture;
    using UnityEngine;

    public class AbilitySystem
    {
        private Transform _owner;
        private Rigidbody2D _rigidbody2D;

        private Dictionary<AbilityType, AbilityBase> _abilities = new();

        public void Initialize(Transform owner, Rigidbody2D rigidbody2D)
        {
            _owner = owner;
            _rigidbody2D = rigidbody2D;

            GenerateAbility();

            // bind 
            var controlPad = UIManager.Instance.GetUI<ControlPadUI>();
            controlPad.AddDashClickAction(_abilities[AbilityType.Dash].Execute);
        }

        private void GenerateAbility()
        {
            _abilities[AbilityType.Dash] = AbilityFactory.GetAbility(AbilityType.Dash, _owner, _rigidbody2D);
        }
    }
}