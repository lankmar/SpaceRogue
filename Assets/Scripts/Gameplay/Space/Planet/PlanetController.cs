using Abstracts;

namespace Gameplay.Space.Planet
{
    public class PlanetController : BaseController
    {
        private readonly PlanetView _view;
        
        public PlanetController(PlanetView view)
        {
            _view = view;
        }
    }
}