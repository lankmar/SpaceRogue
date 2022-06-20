using Abstracts;

namespace Gameplay.Space.Star
{
    public class StarController : BaseController
    {
        public StarView StarView { get; }
        
        public StarController(StarView starView)
        {
            StarView = starView;
        }

    }
}