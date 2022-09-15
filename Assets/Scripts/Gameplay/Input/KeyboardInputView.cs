using UnityEngine;
using Utilities.Unity;

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
        }

        private void OnDestroy()
        {
            EntryPoint.UnsubscribeFromUpdate(CheckHorizontalInput);
            EntryPoint.UnsubscribeFromUpdate(CheckVerticalInput);
            EntryPoint.UnsubscribeFromUpdate(CheckFiringInput);
        }

        private void CheckHorizontalInput()
        {
            float horizontalOffset = UnityEngine.Input.GetAxis(Horizontal);
            
            var mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            
            mousePosition = transform.worldToLocalMatrix.MultiplyPoint(mousePosition).normalized;
            
            var direction = transform.TransformDirection(Vector3.up);
            
            horizontalOffset = 0;
            
            if (!UnityHelper.Approximately(mousePosition, direction, 0.1f))
            {
                horizontalOffset = mousePosition.x <= 0 ? -1 : 1;
            }
            Debug.LogWarning($"{mousePosition}   {direction}");
            Debug.Log(horizontalOffset);
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