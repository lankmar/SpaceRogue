using Abstracts;
using Gameplay.Movement;
using UI.Game;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementController : BaseController
    {
        private readonly SubscribedProperty<Vector3> _mousePositionInput;
        private readonly SubscribedProperty<float> _verticalInput;

        private readonly PlayerSpeedometerView _speedometerView;
        private readonly MovementModel _model;
        private readonly PlayerView _view;
		private readonly Rigidbody2D _rigidbody;
        
		private Vector3 _currentDirection;
		
        public PlayerMovementController(
            SubscribedProperty<Vector3> mousePositionInput,
            SubscribedProperty<float> verticalInput,
            MovementConfig config,
            PlayerView view)
        {
            _mousePositionInput = mousePositionInput;
            _verticalInput = verticalInput;
            _view = view;
            _rigidbody = _view.GetComponent<Rigidbody2D>();
            _model = new MovementModel(config);
            _speedometerView = GameUIController.PlayerSpeedometerView;
            _speedometerView.Init(GetSpeedometerTextValue(0.0f, _model.MaxSpeed));

            _mousePositionInput.Subscribe(HandleHorizontalMouseInput);
            _verticalInput.Subscribe(HandleVerticalInput);
        }

        protected override void OnDispose()
        {
            _mousePositionInput.Unsubscribe(HandleHorizontalMouseInput);
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

        private void HandleHorizontalMouseInput(Vector3 newMousePositionInput)
        {
            var mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(newMousePositionInput);
            mousePosition.z = 0;
            
            var direction = (mousePosition - _view.transform.position).normalized;
            _currentDirection = _view.transform.TransformDirection(Vector3.up);
            float angle = Vector2.SignedAngle(direction, _currentDirection);

            Quaternion newRotation;
            if (UnityHelper.Approximately(angle, 0, 0.2f))
            {
                _model.StopTurning();
                _rigidbody.angularVelocity = 0;
                return;
            }

            if (angle > 0)
            {
                _model.Turn(true);
                newRotation = _view.transform.rotation * Quaternion.AngleAxis(_model.CurrentTurnRate, Vector3.forward);
            }
            else
            {
                _model.Turn(false);
                newRotation = _view.transform.rotation * Quaternion.AngleAxis(-_model.CurrentTurnRate, Vector3.back);
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
                _ => $"SPD: {Mathf.RoundToInt(currentSpeed / maximumSpeed * 100)}"
            };
    }
}