using Abstracts;
using Asteroid;
using Gameplay.Player;

namespace Gameplay.Asteroid.Behaviour
{
    public class AsteroidBehaviourController : BaseController
    {
        private readonly AsteroidView _view;
        private readonly AsteroidBehaviourConfig _asteroidConfig;
        private readonly AsteroidMoveType _moveType;
        private AsteroidBehaviour _currentBehaviour;
        private readonly PlayerView _playerView;

        public AsteroidBehaviourController(AsteroidView view, PlayerView playerView, AsteroidBehaviourConfig config, AsteroidMoveType moveType)
        {
            _view = view;
            _asteroidConfig = config;
            _moveType = moveType;
            _playerView = playerView;

            switch (_moveType)
            {
                case AsteroidMoveType.Static:
                    _currentBehaviour = new AsteroidStaticBehavior(_view, _playerView, _asteroidConfig);
                    break;
                case AsteroidMoveType.LinearMotion:
                    _currentBehaviour = new AsteroidLinearMotion(_view, _playerView, _asteroidConfig);
                    break;

                default: return;
            }
        }

        protected override void OnDispose()
        {
            _currentBehaviour.Dispose();
        }
    }
}