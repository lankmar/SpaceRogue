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
            EntryPoint.SubscribeToUpdate(CheckHorizontalInput);
            EntryPoint.SubscribeToUpdate(CheckVerticalInput);
            EntryPoint.SubscribeToUpdate(CheckFiringInput);
            EntryPoint.SubscribeToUpdate(CheckMousePositionInput);
        }

        private void OnDestroy()
        {
            EntryPoint.UnsubscribeFromUpdate(CheckHorizontalInput);
            EntryPoint.UnsubscribeFromUpdate(CheckVerticalInput);
            EntryPoint.UnsubscribeFromUpdate(CheckFiringInput);
            EntryPoint.UnsubscribeFromUpdate(CheckMousePositionInput);
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

        private void CheckMousePositionInput()
        {
            Vector3 value = UnityEngine.Input.mousePosition;
            OnMousePositionInput(value);
        }

        private static float CalculateInputValue(float axisOffset, float inputMultiplier)
            => axisOffset * inputMultiplier;
    }
}