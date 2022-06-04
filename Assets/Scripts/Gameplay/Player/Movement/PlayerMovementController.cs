using Abstracts;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementController : BaseController
    {
        private readonly SubscribedProperty<float> _horizontalInput;
        private readonly SubscribedProperty<float> _verticalInput;
        
        private readonly PlayerView _view;
        private readonly PlayerMovementModel _model;

        private float CurrentSpeed = 0.0f;
        private float CurrentTurnRate = 0.0f;

        private float TurnSpeedDifference => _model.maximumTurnSpeed - _model.startingTurnSpeed;

        
        public PlayerMovementController(
            SubscribedProperty<float> horizontalInput, 
            SubscribedProperty<float> verticalInput,
            PlayerMovementModel model,
            PlayerView view)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
            _view = view;
            _model = model;
            _horizontalInput.Subscribe(HandleHorizontalInput);
            _verticalInput.Subscribe(HandleVerticalInput);
        }

        protected override void OnDispose()
        {
            _horizontalInput.Unsubscribe(HandleHorizontalInput);
            _verticalInput.Unsubscribe(HandleVerticalInput);
        }

        
        private void HandleVerticalInput(float inputValue)
        {
        }
        
        //TODO rework
        private void HandleHorizontalInput(float inputValue)
        {
            if (inputValue == 0 && CurrentTurnRate > 0)
            {
                StopTurning();
                return;
            }

            var turnAcceleration = CountAcceleration(TurnSpeedDifference, _model.accelerationTime, inputValue, Time.deltaTime);
            if (inputValue < 0)
            {
                if (CurrentTurnRate > 0)
                {
                    StopTurning();
                    CurrentTurnRate -= _model.startingTurnSpeed;
                    _view.transform.Rotate(Vector3.forward, CurrentTurnRate);
                    CurrentTurnRate -= turnAcceleration;
                }
                else
                {
                    if (Mathf.Abs(CurrentTurnRate) >= _model.maximumTurnSpeed)
                    {
                        CurrentTurnRate = 0 - _model.maximumTurnSpeed;
                    }
                    _view.transform.Rotate(Vector3.forward, CurrentTurnRate);
                }
            }
        }
        

        private void StopTurning() => CurrentTurnRate = 0.0f;

        private static float CountAcceleration(float speedDifference, float accelerationTime, float inputValue, float deltaTime)
        {
            if (accelerationTime <= 0) return speedDifference * deltaTime * inputValue * 10; //Prevents zero division
            return speedDifference * inputValue * deltaTime / accelerationTime;
        }

        
    }
}