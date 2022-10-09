using Abstracts;
using Gameplay.Damage;

namespace Gameplay.Space.Star
{
    public class StarController : BaseController
    {
        public StarView StarView { get; }
        
        public StarController(StarView starView)
        {
            StarView = starView;

            var damageModel = new DamageModel(200);
            starView.Init(damageModel);

            AddGameObject(starView.gameObject);
        }

    }
}