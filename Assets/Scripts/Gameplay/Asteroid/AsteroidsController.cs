using Abstracts;
using Gameplay.Player;
using Scriptables.Asteroid;
using Utilities.ResourceManagement;

namespace Gameplay.Asteroid
{
    public class AsteroidsController : BaseController
    {
        private readonly ResourcePath _groupSpawnConfigPath = new(Constants.Configs.Asteroid.AsteroidSpawnConfig);
        private SingleAsteroidSpawnController _singleAsteroidSpawnController;
        private CloudAsteroidsSpawnController _cloudAsteroidsSpawnController;
        private AsteroidExplosionController _asteroidExplosionController;

        public AsteroidsController(PlayerController playerController)
        {
            var asteroidSpawnConfig = ResourceLoader.LoadObject<AsteroidSpawnConfig>(_groupSpawnConfigPath);
            _asteroidExplosionController = new AsteroidExplosionController();

            _cloudAsteroidsSpawnController = new CloudAsteroidsSpawnController(playerController.View, asteroidSpawnConfig, _asteroidExplosionController);
            _singleAsteroidSpawnController = new SingleAsteroidSpawnController(playerController.View, asteroidSpawnConfig, _asteroidExplosionController, _cloudAsteroidsSpawnController);
        }
    }
}
