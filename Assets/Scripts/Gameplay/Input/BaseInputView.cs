using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Input
{
    public abstract class BaseInputView : MonoBehaviour
    {
        private SubscribedProperty<float> _horizontalAxisInput;
        private SubscribedProperty<float> _verticalAxisInput;

        public virtual void Init(
            SubscribedProperty<float> horizontalMove, 
            SubscribedProperty<float> verticalMove)
        {
            _horizontalAxisInput = horizontalMove;
            _verticalAxisInput = verticalMove;
        }

        protected virtual void OnHorizontalInput(float value) 
            => _horizontalAxisInput.Value = value;
        
        protected virtual void OnVerticalInput(float value) 
            => _verticalAxisInput.Value = value;
    }
}