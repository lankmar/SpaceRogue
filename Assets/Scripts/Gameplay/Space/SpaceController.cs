using Abstracts;
using Gameplay.Space.Planet;
using Gameplay.Space.Star;
using Scriptables.Space;
using Utilities.ResourceManagement;

namespace Gameplay.Space
{
    public class SpaceController : BaseController
    {
        private readonly ResourcePath _configPath = new("Configs/SpaceConfig");
        private readonly SpaceConfig _config;

        private readonly ResourcePath _planetSpawnConfigPath = new("Configs/PlanetSpawnConfig");
        private readonly PlanetFactory _planetFactory;

        private readonly ResourcePath _starSpawnConfigPath = new("Configs/StarSpawnConfig");
        private readonly StarFactory _starFactory;
        
        public SpaceController()
        {
            //TODO Replace with map generation
            _config = ResourceLoader.LoadObject<SpaceConfig>(_configPath);

            foreach (var starSpawnPoint in _config.StarSpawnPoints)
            {
                //TODO Replace with CreateStar
                //Создаем контроллеры для каждого типа объекта
            }
        }
    }
}