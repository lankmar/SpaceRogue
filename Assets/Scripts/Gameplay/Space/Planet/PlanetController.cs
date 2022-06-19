using Abstracts;
using Gameplay.Space.Star;

namespace Gameplay.Space.Planet
{
    public class PlanetController : BaseController
    {
        private readonly PlanetView _view;
        
        private readonly StarView _starView;

        public PlanetController(PlanetView view, StarView starView)
        {
            _view = view;
            _starView = starView;
        }
    }
}