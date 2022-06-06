using Abstracts;
using Gameplay.GameState;
using Gameplay.Player;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly PlayerController _playerController;

        public GameController(CurrentState currentState)
        {
            _currentState = currentState;
            
            _playerController = new PlayerController();
            AddController(_playerController);
        }
    }
}