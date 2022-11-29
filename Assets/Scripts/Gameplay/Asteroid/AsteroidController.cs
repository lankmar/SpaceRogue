using Abstracts;
using Gameplay.Asteroid.Behaviour;
using Gameplay.Asteroid.Movement;
using Gameplay.Damage;
using Gameplay.Health;
using Gameplay.Movement;
using Gameplay.Player;
using Scriptables.Asteroid;
using Scriptables.Health;

namespace Gameplay.Asteroid
{
    public class AsteroidController : BaseController
    {
        private readonly AsteroidView _view;
        private readonly AsteroidConfig _config;
        private readonly PlayerView _playerView;
        private readonly AsteroidBehaviourController _behaviourController;
        private readonly HealthController _healthController;


        public AsteroidController(AsteroidConfig config, AsteroidView view, PlayerView playerView)
        {
            _config = config;
            _view = view;
            _playerView = playerView;
            AddGameObject(_view.gameObject);

            var damageModel = new DamageModel(config.DamageAmount);
            _view.Init(damageModel);
            _behaviourController = new AsteroidBehaviourController(view, _playerView, _config.Behaviour, _config.AsteroidMoveType);
            AddController(_behaviourController);
            
            _healthController = AddHealthController(_config.Health);

        }
        private HealthController AddHealthController(HealthConfig healthConfig)
        {
            var healthController = new HealthController(_config.Health, _view);

            healthController.SubscribeToOnDestroy(Dispose);
            AddController(_healthController);
            return healthController;
        }
        protected override void OnDispose()
        {
            _view.CollisionEnter -= Dispose;
        }
    }
}