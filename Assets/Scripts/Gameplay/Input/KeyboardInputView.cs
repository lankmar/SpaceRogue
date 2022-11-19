using UnityEngine;

namespace Gameplay.Input
{
    public class KeyboardInputView : BaseInputView
    {
        [SerializeField] private float horizontalAxisInputMultiplier;
        [SerializeField] private float verticalAxisInputMultiplier;

        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const KeyCode PrimaryFire = KeyCode.Mouse0;

        private void Start()
        {
            EntryPoint.SubscribeToFixedUpdate(CheckHorizontalInput);
            EntryPoint.SubscribeToFixedUpdate(CheckVerticalInput);
            EntryPoint.SubscribeToUpdate(CheckFiringInput);
        }

        private void OnDestroy()
        {
            EntryPoint.UnsubscribeFromFixedUpdate(CheckHorizontalInput);
            EntryPoint.UnsubscribeFromFixedUpdate(CheckVerticalInput);
            EntryPoint.UnsubscribeFromUpdate(CheckFiringInput);
        }

        private void CheckHorizontalInput()
        {
            float horizontalOffset = UnityEngine.Input.GetAxis(Horizontal);
            float inputValue = CalculateInputValue(horizontalOffset, horizontalAxisInputMultiplier);
            OnHorizontalInput(inputValue);
        }

        private void CheckVerticalInput()
        {
            float verticalOffset = UnityEngine.Input.GetAxis(Vertical);
            float inputValue = CalculateInputValue(verticalOffset, verticalAxisInputMultiplier);
            OnVerticalInput(inputValue);
        }

        private void CheckFiringInput()
        {
            bool value = UnityEngine.Input.GetKey(PrimaryFire);
            OnPrimaryFireInput(value);
        }

        private static float CalculateInputValue(float axisOffset, float inputMultiplier)
            => axisOffset * inputMultiplier;
    }
}