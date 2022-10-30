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
        private readonly SubscribedProperty<Vector3> _mousePositionInput;

        private readonly PlayerSpeedometerView _speedometerView;
        private readonly MovementModel _model;
        private readonly PlayerView _view;

        public PlayerMovementController(
            SubscribedProperty<float> horizontalInput,
            SubscribedProperty<float> verticalInput,
            SubscribedProperty<Vector3> mousePositionInput,
            MovementConfig config,
            PlayerView view)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
            _mousePositionInput = mousePositionInput;
            _view = view;
            _model = new MovementModel(config);
            _speedometerView = GameUIController.PlayerSpeedometerView;
            _speedometerView.Init(GetSpeedometerTextValue(0.0f, _model.MaxSpeed));

            _horizontalInput.Subscribe(HandleHorizontalInput);
            _verticalInput.Subscribe(HandleVerticalInput);
            _mousePositionInput.Subscribe(HandleHorizontalMouseInput);
        }

        protected override void OnDispose()
        {
            _horizontalInput.Unsubscribe(HandleHorizontalInput);
            _verticalInput.Unsubscribe(HandleVerticalInput);
            _mousePositionInput.Unsubscribe(HandleHorizontalMouseInput);
        }


        private void HandleVerticalInput(float newInputValue)
        {
            if (newInputValue != 0)
            {
                _model.Accelerate(newInputValue > 0);
            }

            float currentSpeed = _model.CurrentSpeed;
            UpdateSpeedometerValue(currentSpeed, _model.MaxSpeed);
            if (currentSpeed != 0)
            {
                var transform = _view.transform;
                var forwardDirection = transform.TransformDirection(Vector3.up);
                transform.position += currentSpeed * Time.deltaTime * forwardDirection;
            }

            if (newInputValue == 0 && currentSpeed < _model.StoppingSpeed && currentSpeed > -_model.StoppingSpeed)
            {
                _model.StopMoving();
            }
        }

        private void HandleHorizontalInput(float newInputValue)
        {
            switch (newInputValue)
            {
                case 0:
                    _model.StopTurning();
                    break;
                case < 0:
                    _model.Turn(true);
                    _view.transform.Rotate(Vector3.forward, _model.CurrentTurnRate * newInputValue);
                    break;
                case > 0:
                    _model.Turn(false);
                    _view.transform.Rotate(Vector3.back, _model.CurrentTurnRate * newInputValue);
                    break;
            }
        }

        private void HandleHorizontalMouseInput(Vector3 newMousePositionInput)
        {
            var mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(newMousePositionInput);
            mousePosition.z = 0;
            var direction = mousePosition - _view.transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            
            _model.Turn(false);
            var rotationAngle = Mathf.LerpAngle(-_view.transform.rotation.eulerAngles.z, angle, _model.CurrentTurnRate / 4);
            _view.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.back);
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