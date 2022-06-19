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
            //TODO Replace with map generation
            var config = ResourceLoader.LoadObject<SpaceConfig>(_configPath);
            var starSpawnConfigPath = ResourceLoader.LoadObject<StarSpawnConfig>(_starSpawnConfigPath);
            var planetSpawnConfig = ResourceLoader.LoadObject<PlanetSpawnConfig>(_planetSpawnConfigPath);

            foreach (var starSpawnPoint in config.StarSpawnPoints)
            {
                //TODO Replace with CreateStar
                //Создаем контроллеры для каждого типа объекта
            }
        }
    }
}