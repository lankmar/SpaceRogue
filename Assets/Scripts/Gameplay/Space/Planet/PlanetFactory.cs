using Scriptables.Space;

namespace Gameplay.Space.Planet
{
    public class PlanetFactory
    {
        private readonly PlanetSpawnConfig _config;
        
        public PlanetFactory(PlanetSpawnConfig config)
        {
            _config = config;
        }
    }
}