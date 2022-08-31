using Abstracts;
using Gameplay.Enemy;
using Gameplay.Camera;
using Gameplay.GameState;
using Gameplay.Player;
using Gameplay.Space;
using UI.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly PlayerController _playerController;
        private readonly SpaceController _spaceController;
        private readonly EnemyForcesController _enemyForcesController;
        private readonly CameraController _cameraController;
        private readonly GameUIController _gameUIController;

        public GameController(CurrentState currentState, Canvas mainUICanvas)
        {
            _currentState = currentState;
            _gameUIController = new GameUIController(mainUICanvas);
            AddController(_gameUIController);
            
            _playerController = new PlayerController();
            AddController(_playerController);
            _playerController.PlayerDestroyed += OnPlayerDestroyed;

            _cameraController = new CameraController(_playerController.View);
            AddController(_cameraController);

            _spaceController = new SpaceController();
            AddController(_spaceController);

            _enemyForcesController = new EnemyForcesController(_playerController.View);
            AddController(_enemyForcesController);
        }

        private void OnPlayerDestroyed()
        {
            _gameUIController.AddDestroyPlayerMessage();
            _gameUIController.DestroyPlayerViewComponent.gameObject.GetComponentInChildren<Button>().onClick.AddListener(EditorStatusGameOnMenu);
        }

        public void EditorStatusGameOnMenu() 
        {
            _currentState.CurrentGameState.Value = GameState.GameState.Menu;
        }
    }
}