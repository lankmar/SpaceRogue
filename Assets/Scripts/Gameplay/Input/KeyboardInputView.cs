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
            var horizontalOffset = UnityEngine.Input.GetAxis(Horizontal);
            var inputValue = CalculateInputValue(horizontalOffset, horizontalAxisInputMultiplier, Time.deltaTime);
            OnHorizontalInput(inputValue);
        }

        private void CheckVerticalInput()
        {
            var verticalOffset = UnityEngine.Input.GetAxis(Vertical);
            var inputValue = CalculateInputValue(verticalOffset, verticalAxisInputMultiplier, Time.deltaTime);
            OnVerticalInput(inputValue);
        }

        private static float CalculateInputValue(float axisOffset, float inputMultiplier, float deltaTime) 
            => axisOffset * inputMultiplier * deltaTime;
    }
}