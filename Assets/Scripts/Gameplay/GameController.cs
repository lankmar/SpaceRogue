using Abstracts;
using Gameplay.GameState;
using Gameplay.Player;
using Gameplay.Space;
using CameraInGame;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly PlayerController _playerController;
        private readonly SpaceController _spaceController;
        private readonly CameraController _cameraController;

        public GameController(CurrentState currentState)
        {
            _currentState = currentState;
            
            _playerController = new PlayerController();
            AddController(_playerController);

            _cameraController = new CameraController(_playerController._view);
            AddController(_cameraController);

            _spaceController = new SpaceController();
            AddController(_spaceController);
        }
    }
}