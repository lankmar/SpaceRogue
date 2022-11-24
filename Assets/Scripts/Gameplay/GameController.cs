using Abstracts;
using Gameplay.Background;
using Gameplay.Camera;
using Gameplay.Enemy;
using Gameplay.GameState;
using Gameplay.Player;
using Gameplay.Space;
using UI.Game;
using UnityEngine;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly GameUIController _gameUIController;
        private readonly PlayerController _playerController;
        private readonly CameraController _cameraController;
        private readonly BackgroundController _backgroundController;
        private readonly SpaceController _spaceController;
        private readonly EnemyForcesController _enemyForcesController;

        public GameController(CurrentState currentState, Canvas mainUICanvas)
        {
            _currentState = currentState;

            _gameUIController = new(mainUICanvas, ExitToMenu);
            AddController(_gameUIController);

            _playerController = new();
            AddController(_playerController);
            _playerController.PlayerDestroyed += OnPlayerDestroyed;

            _cameraController = new(_playerController);
            AddController(_cameraController);

            _backgroundController = new();
            AddController(_backgroundController);

            _spaceController = new();
            AddController(_spaceController);

            _enemyForcesController = new(_playerController);
            AddController(_enemyForcesController);
        }

        private void OnPlayerDestroyed()
        {
            _gameUIController.AddDestroyPlayerMessage();
        }

        public void ExitToMenu() 
        {
            _currentState.CurrentGameState.Value = GameState.GameState.Menu;
        }
    }
}