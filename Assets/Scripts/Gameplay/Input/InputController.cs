using Abstracts;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;

namespace Gameplay.Input
{
    public class InputController : BaseController
    {
        private readonly ResourcePath _viewPrefabPath = new(Constants.Prefabs.Input.KeyboardInput);
        private readonly BaseInputView _view;

        public InputController(
            SubscribedProperty<float> horizontalInput,
            SubscribedProperty<float> verticalInput,
            SubscribedProperty<bool> primaryFireInput,
            SubscribedProperty<Vector3> mousePositionInput)
        {
            _view = LoadView<BaseInputView>(_viewPrefabPath);
            _view.Init(horizontalInput, verticalInput, primaryFireInput, mousePositionInput);
        }

    }
}