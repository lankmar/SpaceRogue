using Abstracts;

namespace Gameplay.Space.Star
{
    public class StarController : BaseController
    {
        private readonly StarView _view;
        
        public StarController(StarView view)
        {
            _view = view;
        }

    }
}