using UnityEngine;

namespace Gameplay.Input
{
    public class KeyboardInputView : BaseInputView
    {
        [SerializeField] private float horizontalAxisInputMultiplier;
        [SerializeField] private float verticalAxisInputMultiplier;

        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        private void Start()
        {
            EntryPoint.SubscribeToUpdate(CheckHorizontalInput);
            EntryPoint.SubscribeToUpdate(CheckVerticalInput);
        }

        private void OnDestroy()
        {
            EntryPoint.UnsubscribeFromUpdate(CheckHorizontalInput);
            EntryPoint.UnsubscribeFromUpdate(CheckVerticalInput);
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

        private static float CalculateInputValue(float axisOffset, float inputMultiplier)
            => axisOffset * inputMultiplier;
    }
}