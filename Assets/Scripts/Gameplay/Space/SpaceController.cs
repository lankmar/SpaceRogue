using Abstracts;
using Gameplay.Space.Planet;
using Gameplay.Space.Star;
using Scriptables.Space;
using Utilities.ResourceManagement;

namespace Gameplay.Space
{
    public class SpaceController : BaseController
    {
        private readonly ResourcePath _configPath = new("Configs/Space/SpaceConfig");

        private readonly ResourcePath _starSpawnConfigPath = new("Configs/Space/DefaultStarSpawn");
        private readonly StarFactory _starFactory;
        
        private readonly ResourcePath _planetSpawnConfigPath = new("Configs/Space/DefaultPlanetSpawn");
        private readonly PlanetFactory _planetFactory;

        
        
        public SpaceController()
        {
            var config = ResourceLoader.LoadObject<SpaceConfig>(_configPath);
            var starSpawnConfig = ResourceLoader.LoadObject<StarSpawnConfig>(_starSpawnConfigPath);
            var planetSpawnConfig = ResourceLoader.LoadObject<PlanetSpawnConfig>(_planetSpawnConfigPath);

            _starFactory = new StarFactory(starSpawnConfig);
            _planetFactory = new PlanetFactory(planetSpawnConfig);

            //TODO Replace with map generation
            foreach (var starSpawnPoint in config.StarSpawnPoints)
            {
                var star = _starFactory.CreateStar(starSpawnPoint);
                AddController(star);
            }
        }
    }
}