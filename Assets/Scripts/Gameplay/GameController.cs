using Abstracts;
using Gameplay.GameState;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;

        public GameController(CurrentState currentState)
        {
            _currentState = currentState;
        }
    }
}