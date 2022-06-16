using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Input
{
    public abstract class BaseInputView : MonoBehaviour
    {
        private SubscribedProperty<float> _horizontalAxisInput;
        private SubscribedProperty<float> _verticalAxisInput;

        private SubscribedProperty<bool> _primaryFireInput;

        public virtual void Init(
            SubscribedProperty<float> horizontalMove, 
            SubscribedProperty<float> verticalMove,
            SubscribedProperty<bool> primaryFireInput)
        {
            _horizontalAxisInput = horizontalMove;
            _verticalAxisInput = verticalMove;
            _primaryFireInput = primaryFireInput;
        }

        protected virtual void OnHorizontalInput(float value) 
            => _horizontalAxisInput.Value = value;
        
        protected virtual void OnVerticalInput(float value) 
            => _verticalAxisInput.Value = value;

        protected virtual void OnPrimaryFireInput(bool value)
            => _primaryFireInput.Value = value;
    }
}