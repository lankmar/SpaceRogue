using Abstracts;
using Gameplay.Movement;
using UI.Game;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementController : BaseController
    {
        private readonly SubscribedProperty<float> _horizontalInput;
        private readonly SubscribedProperty<float> _verticalInput;

        private readonly PlayerSpeedometerView _speedometerView;
        private readonly MovementModel _model;
        private readonly PlayerView _view;
        private readonly Rigidbody2D _rigidbody;
        
        public PlayerMovementController(
            SubscribedProperty<float> horizontalInput, 
            SubscribedProperty<float> verticalInput,
            MovementConfig config,
            PlayerView view)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
            _view = view;
            _rigidbody = _view.GetComponent<Rigidbody2D>();
            _model = new MovementModel(config);
            _speedometerView = GameUIController.PlayerSpeedometerView;
            _speedometerView.Init(GetSpeedometerTextValue(0.0f, _model.MaxSpeed));

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
            if (newInputValue != 0)
            {
                _model.Accelerate(newInputValue > 0);
            }
            
            float currentSpeed = _model.CurrentSpeed;
            float maxSpeed = _model.MaxSpeed;
            UpdateSpeedometerValue(currentSpeed, maxSpeed);
            
            if (currentSpeed != 0)
            {
                var transform = _view.transform;
                var forwardDirection = transform.TransformDirection(Vector3.up);
                _rigidbody.AddForce(forwardDirection.normalized * currentSpeed, ForceMode2D.Force);
            }
            
            if (_rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                Vector3 velocity = _rigidbody.velocity.normalized * maxSpeed;
                _rigidbody.velocity = velocity;
            }

            if (newInputValue == 0 && currentSpeed < _model.StoppingSpeed && currentSpeed > -_model.StoppingSpeed)
            {
                _model.StopMoving();
            }
        }
        
        private void HandleHorizontalInput(float newInputValue)
        {
            Quaternion newRotation = Quaternion.identity;
            switch (newInputValue)
            {
                case 0:
                    _model.StopTurning();
                    newRotation = _view.transform.rotation;
                    break;
                case < 0:
                    _model.Turn(true);
                    newRotation = _view.transform.rotation * Quaternion.AngleAxis(_model.CurrentTurnRate * newInputValue, Vector3.forward);
                    break;
                case > 0:
                    _model.Turn(false);
                    newRotation = _view.transform.rotation * Quaternion.AngleAxis(_model.CurrentTurnRate * newInputValue, Vector3.back);
                    break;
            }
            _rigidbody.MoveRotation(newRotation);
        }

        private void UpdateSpeedometerValue(float currentSpeed, float maxSpeed)
        {
            _speedometerView.UpdateText(GetSpeedometerTextValue(currentSpeed, maxSpeed));
        }

        private static string GetSpeedometerTextValue(float currentSpeed, float maximumSpeed) =>
            currentSpeed switch
            {
                < 0 => "R",
                _ => $"SPD: {Mathf.RoundToInt(currentSpeed/maximumSpeed * 100)}"
            };
    }
}