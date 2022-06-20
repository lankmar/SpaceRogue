using Abstracts;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;

namespace Gameplay.Input
{
    public class InputController : BaseController
    {
        private readonly ResourcePath _viewPrefabPath = new("Prefabs/Input/KeyboardInput");
        private BaseInputView _view;

        public InputController(
            SubscribedProperty<float> horizontalInput,
            SubscribedProperty<float> verticalInput,
            SubscribedProperty<bool> primaryFireInput)
        {
            _view = LoadView<BaseInputView>(_viewPrefabPath);
            _view.Init(horizontalInput, verticalInput, primaryFireInput);
        }

    }
}