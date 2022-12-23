using Asteroid;
using Gameplay.Player;
using Scriptables.Asteroid;
using UnityEngine;

namespace Gameplay.Asteroid
{
    public class AsteroidFactory 
    {
        private readonly AsteroidConfig _config;

        public AsteroidFactory(AsteroidConfig config)
        {
            _config = config;
        }

        public AsteroidController CreateAsteroid(Vector3 spawnPosition, PlayerView playerView, AsteroidExplosionController asteroidExplosionController)
        {
            return new(_config, CreateAsteroidView(spawnPosition), playerView, asteroidExplosionController);
        }

        public AsteroidController CreateAsteroid(Vector3 spawnPosition, PlayerView playerView, AsteroidExplosionController asteroidExplosionController, CloudAsteroidsSpawnController cloudAsteroidsSpawnController)
        {
            return new(_config, CreateAsteroidView(spawnPosition), playerView, asteroidExplosionController, cloudAsteroidsSpawnController); 
        }

        private AsteroidView CreateAsteroidView(Vector3 spawnPosition)
        {
            var asteroid = Object.Instantiate(_config.Prefab, spawnPosition, Quaternion.identity);
            Vector3 size = asteroid.transform.localScale;
            float sizeMulti = Random.Range(_config.MinimumSize, _config.MaximumSize)/100;
            asteroid.transform.localScale = new Vector3(size.x + sizeMulti, size.y + sizeMulti, size.z);
            return asteroid;
        }
    }
}
