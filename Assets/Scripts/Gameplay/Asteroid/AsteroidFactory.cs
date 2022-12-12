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

        public AsteroidController CreateAsteroid(Vector3 spawnPosition, PlayerView playerView)
        {
            //return  new(_config, CreateAsteroidView(spawnPosition));
            return  new(_config, CreateAsteroidView(spawnPosition), playerView);
        }

        private AsteroidView CreateAsteroidView(Vector3 spawnPosition)
        {
            var asteroid = Object.Instantiate(_config.Prefab, spawnPosition, Quaternion.identity);
            return asteroid;
        }
    }
}
