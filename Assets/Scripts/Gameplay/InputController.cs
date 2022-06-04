using Abstracts;
using Gameplay.Input;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;

namespace Gameplay
{
    public class InputController : BaseController
    {
        private readonly ResourcePath _viewPrefabPath = new ResourcePath("Prefabs/Input/KeyboardInput");
        private BaseInputView _view;

        public InputController(
            SubscribedProperty<float> horizontalInput,
            SubscribedProperty<float> verticalInput)
        {
            _view = LoadView();
            _view.Init(horizontalInput, verticalInput);
        }

        private BaseInputView LoadView()
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPrefabPath);
            GameObject viewObject = Object.Instantiate(prefab);
            AddGameObject(viewObject);

            BaseInputView view = viewObject.GetComponent<BaseInputView>();
            return view;
        }
    }
}