using Abstracts;
using UnityEngine;
using Utilities.ResourceManagement;
using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;
using Scriptables.Asteroid;
using Utilities.Mathematics;

namespace Gameplay.Asteroid
{
    public class SingleAsteroidSpawnController : BaseController
    {
        private readonly ResourcePath _groupSpawnConfigPath = new(Constants.Configs.Asteroid.AsteroidSpawnConfig);
        private AsteroidFactory _asteroidFactory;
        int _spawnCircleRadius = 50;
        PlayerView _playerView;
        private float _currentAsteroidSpawnTime = 1;
        AsteroidSpawnConfig _asteroidSpawnConfig;

        public SingleAsteroidSpawnController(PlayerView playerView, AsteroidSpawnConfig asteroidSpawnConfig)
        {
            _playerView = playerView;
            _asteroidSpawnConfig = asteroidSpawnConfig;
            EntryPoint.SubscribeToUpdate(OnUpdate);
        }

        protected void OnUpdate()
        {
            if (_currentAsteroidSpawnTime<=0.0f)
            {
                _currentAsteroidSpawnTime = Random.Range(_asteroidSpawnConfig.MinTimeAsteroidSpawn, _asteroidSpawnConfig.MaxTimeAsteroidSpawn);
                AsteroidSpawn();
            }
            CooldownAsteroidSpawn();

        }

        protected void CooldownAsteroidSpawn()
        {
            _currentAsteroidSpawnTime -= Time.deltaTime;
        }

        private void AsteroidSpawn()
        {
            var spawnConfig = ResourceLoader.LoadObject<AsteroidSpawnConfig>(_groupSpawnConfigPath);

            _asteroidFactory = (Random.Range(0, 10) > 5) ? new AsteroidFactory(spawnConfig.SingleAsteroid) : new AsteroidFactory(spawnConfig.FastAsteroid);

            var unitSize = spawnConfig.AsteroidClouds.Prefab.transform.localScale;

            var unitSpawnPoint = GetEmptySpawnPoint(_playerView.gameObject.transform.position, unitSize, _spawnCircleRadius);
                var asteroidController = _asteroidFactory.CreateAsteroid(unitSpawnPoint,_playerView);
                AddController(asteroidController);
            
        }


        private static Vector3 GetEmptySpawnPoint(Vector3 spawnPoint, Vector3 unitSize, int spawnCircleRadius)
        {
            var unitSpawnPoint = spawnPoint + (Vector3)(Random.insideUnitCircle * spawnCircleRadius);
            float unitMaxSize = unitSize.MaxVector3CoordinateOnPlane();
                unitSpawnPoint.y = (spawnPoint.y - unitSpawnPoint.y <= 0) ? unitSpawnPoint.y + 30 : unitSpawnPoint.y - 30;

            while (UnityHelper.IsAnyObjectAtPosition(unitSpawnPoint, unitMaxSize))
            {
                unitSpawnPoint = spawnPoint + (Vector3)(Random.insideUnitCircle * spawnCircleRadius);
                unitSpawnPoint.x = (spawnPoint.x - unitSpawnPoint.x <= 0) ? unitSpawnPoint.x + 40 : unitSpawnPoint.x - 40;
            }

            return unitSpawnPoint;
        }
    }
}