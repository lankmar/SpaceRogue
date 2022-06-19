using Scriptables.Space;

namespace Gameplay.Space.Star
{
    public class StarFactory
    {
        private readonly StarSpawnConfig _config;
        public StarFactory(StarSpawnConfig config)
        {
            _config = config;
        }
    }
}