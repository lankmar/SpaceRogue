using Abstracts;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementController : BaseController
    {
        private readonly SubscribedProperty<float> _horizontalInput;
        private readonly SubscribedProperty<float> _verticalInput;

        private readonly PlayerMovementModel _model;
        private readonly PlayerView _view;
        


        public PlayerMovementController(
            SubscribedProperty<float> horizontalInput, 
            SubscribedProperty<float> verticalInput,
            PlayerMovementConfig config,
            PlayerView view)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
            _view = view;
            _model = new PlayerMovementModel(config);
            _horizontalInput.Subscribe(HandleHorizontalInput);
            _verticalInput.Subscribe(HandleVerticalInput);
        }

        protected override void OnDispose()
        {
            _horizontalInput.Unsubscribe(HandleHorizontalInput);
            _verticalInput.Unsubscribe(HandleVerticalInput);
        }

        
        private void HandleVerticalInput(float newInputValue)
        {
            
        }
        
        private void HandleHorizontalInput(float newInputValue)
        {
            _model.Turn(newInputValue);
            var currentTurnAngle = _model.CurrentTurnRate;
            if (currentTurnAngle != 0)
            {
                _view.transform.Rotate(Vector3.forward, currentTurnAngle);
            }
        }
    }
}