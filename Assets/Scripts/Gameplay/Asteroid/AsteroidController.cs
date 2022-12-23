using Abstracts;
using Asteroid;
using Gameplay.Asteroid.Behaviour;
using Gameplay.Damage;
using Gameplay.Health;
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
        private readonly AsteroidExplosionController _asteroidExplosionController;
        private readonly CloudAsteroidsSpawnController _cloudAsteroidsSpawnController;

        public AsteroidController(AsteroidConfig config, AsteroidView view, PlayerView playerView, AsteroidExplosionController asteroidExplosionController)
        {
            _config = config;
            _view = view;
            AddGameObject(_view.gameObject);
            _playerView = playerView;
            _asteroidExplosionController = asteroidExplosionController;

            var damageModel = new DamageModel(config.DamageAmount);
            _view.Init(damageModel);
            if (config.IsDestroyedOnHit) _view.CollisionEnter += OnDispose;
            _behaviourController = new AsteroidBehaviourController(view, _playerView, _config.Behaviour, _config.AsteroidMoveType);
            AddController(_behaviourController);
            AddHealthController(_config.Health);
        }

        public AsteroidController(AsteroidConfig config, AsteroidView view, PlayerView playerView, AsteroidExplosionController asteroidExplosionController, CloudAsteroidsSpawnController cloudAsteroidsSpawnController) : this(config, view, playerView, asteroidExplosionController)
        {
            _cloudAsteroidsSpawnController = cloudAsteroidsSpawnController;
        }

        protected override void OnDispose()
        {
            _asteroidExplosionController.AsteroidDestructionView.PlayExplosion(_view.transform.position);
            if (_config.AsteroidSizeType == AsteroidSizeType.Big | _config.AsteroidSizeType == AsteroidSizeType.Middle)
            {
                _cloudAsteroidsSpawnController.CloudAsteroidsSpawn(_view.transform.position);
            }
            _view.TakeDamage(_view);
            _view.CollisionEnter -= Dispose;
        }


        private HealthController AddHealthController(HealthConfig healthConfig)
        {
            var healthController = new HealthController(_config.Health, _view);

            healthController.SubscribeToOnDestroy(Dispose);
            AddController(_healthController);
            return healthController;
        }

    }
}