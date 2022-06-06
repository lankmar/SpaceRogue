using Abstracts;
using Gameplay.GameState;
using Gameplay.Player;
using Gameplay.Space;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly PlayerController _playerController;
        private readonly SpaceController _spaceController;

        public GameController(CurrentState currentState)
        {
            _currentState = currentState;
            
            _playerController = new PlayerController();
            AddController(_playerController);

            _spaceController = new SpaceController();
            AddController(_spaceController);
        }
    }
}