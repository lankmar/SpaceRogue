using Abstracts;
using Asteroid;
using Gameplay.Asteroid.Movement;
using Gameplay.Enemy.Movement;
using Gameplay.Health;
using Gameplay.Movement;
using Gameplay.Player;
using Scriptables.Asteroid;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Asteroid.Behaviour
{
    public class AsteroidBehaviourController : BaseController
    {
        private readonly SubscribedProperty<AsteroidMoveType> _asteroidCurrentState;
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

            _asteroidCurrentState = new SubscribedProperty<AsteroidMoveType>(AsteroidMoveType.Static);
            _asteroidCurrentState.Subscribe(OnMoveTypeChange);
            switch (_moveType)
            {
                case AsteroidMoveType.Static:
                    OnMoveTypeChange(AsteroidMoveType.Static);
                    break;
                //case AsteroidMoveType.OrbitalMotion:
                //     OnMoveTypeChange(AsteroidMoveType.OrbitalMotion);
                //    break;
                case AsteroidMoveType.LinearMotion:
                    OnMoveTypeChange(AsteroidMoveType.LinearMotion);
                    break;
                //case AsteroidMoveType.StrikingMotion:
                //     OnMoveTypeChange(AsteroidMoveType.StrikingMotion);
                //    break;
                default: return;
            }

        }


        protected override void OnDispose()
        {
            _asteroidCurrentState.Unsubscribe(OnMoveTypeChange);
            _currentBehaviour.Dispose();
        }

        private void OnMoveTypeChange(AsteroidMoveType newState)
        {
            _currentBehaviour?.Dispose();

            switch (newState)
            {
                case AsteroidMoveType.Static:
                    _currentBehaviour = new AsteroidStaticBehavior(_asteroidCurrentState, _view, _playerView, _asteroidConfig);
                    break;
                //case AsteroidMoveType.OrbitalMotion:
                //    _currentBehaviour = new AsteroidOrbitalMotion(_asteroidCurrentState, _view, _playerView, _asteroidConfig);
                //    break;
                case AsteroidMoveType.LinearMotion:
                    _currentBehaviour = new AsteroidLinearMotion(_asteroidCurrentState, _view, _playerView, _asteroidConfig);
                    break;
                //case AsteroidMoveType.StrikingMotion:
                //    _currentBehaviour = new AsteroidStrikingMotion(_asteroidCurrentState, _view, _playerView, _asteroidConfig);
                //    break;
                default: return;
            }
        }
    }
}