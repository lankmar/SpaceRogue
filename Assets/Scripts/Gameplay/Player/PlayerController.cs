using Abstracts;
using Gameplay.Player.Movement;
using Scriptables;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;

namespace Gameplay.Player
{
    public class PlayerController : BaseController
    {
        private readonly ResourcePath _configPath = new("Configs/PlayerConfig");
        private readonly ResourcePath _viewPath = new("Prefabs/Gameplay/Player");
        
        private readonly PlayerConfig _config;
        private readonly PlayerView _view;

        private readonly SubscribedProperty<float> _horizontalInput = new();
        private readonly SubscribedProperty<float> _verticalInput = new();
        private readonly PlayerMovementController _movementController;
        

        public PlayerController()
        {
            _config = ResourceLoader.LoadObject<PlayerConfig>(_configPath);
            _view = LoadView<PlayerView>(_viewPath, Vector3.zero);

            _movementController = AddInputController(_config.movement, _view);
        }

        private PlayerMovementController AddInputController(PlayerMovementConfig movementConfig, PlayerView view)
        {
            var movementController = new PlayerMovementController(_horizontalInput, _verticalInput, movementConfig, view);
            AddController(movementController);
            return movementController;
        }
    }
}