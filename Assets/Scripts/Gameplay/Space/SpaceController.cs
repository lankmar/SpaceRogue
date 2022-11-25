using System.Linq;
using Abstracts;
using Gameplay.Space.Generator;
using Gameplay.Space.Obstacle;
using Gameplay.Space.Planet;
using Scriptables.Space;
using Utilities.ResourceManagement;

namespace Gameplay.Space
{
    public sealed class SpaceController : BaseController
    {
        private readonly ResourcePath _viewPath = new(Constants.Prefabs.Gameplay.Space.Level);
        private readonly ResourcePath _configPath = new(Constants.Configs.Space.SpaceConfig);

        private readonly ResourcePath _starSpawnConfigPath = new(Constants.Configs.Space.DefaultStarSpawn);
        private readonly ResourcePath _planetSpawnConfigPath = new(Constants.Configs.Space.DefaultPlanetSpawn);

        private readonly SpaceView _view;
        private readonly SpaceObjectFactory _spaceObjectFactory;

        public SpaceController()
        {
            _view = LoadView<SpaceView>(_viewPath);
            var config = ResourceLoader.LoadObject<SpaceConfig>(_configPath);
            var starSpawnConfig = ResourceLoader.LoadObject<StarSpawnConfig>(_starSpawnConfigPath);
            var planetSpawnConfig = ResourceLoader.LoadObject<PlanetSpawnConfig>(_planetSpawnConfigPath);

            _spaceObjectFactory = new SpaceObjectFactory(starSpawnConfig, planetSpawnConfig);

            var levelGenerator = new LevelGenerator(_view, config, starSpawnConfig);
            levelGenerator.Generate();
            AddObstacleController(_view.ObstacleView, config.ObstacleForce);

            foreach (var starSpawnPoint in levelGenerator.GetStarSpawnPoints())
            {
                var (star, planetControllers) = _spaceObjectFactory.CreateStarSystem(starSpawnPoint);
                AddController(star);
                AddPlanetControllers(planetControllers);
            }
        }

        private void AddPlanetControllers(PlanetController[] planetControllers)
        {
            if (!planetControllers.Any()) return;
            foreach (var planet in planetControllers)
            {
                AddController(planet);
            }
        }

        private void AddObstacleController(ObstacleView obstacleView, float obstacleForce)
        {
            var obstacleController = new ObstacleController(obstacleView, obstacleForce);
            AddController(obstacleController);
        }
    }
}