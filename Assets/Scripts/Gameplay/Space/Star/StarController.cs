using Abstracts;
using Gameplay.Damage;

namespace Gameplay.Space.Star
{
    public class StarController : BaseController
    {
        public StarView StarView { get; }

        private const int fatalDamage = 9999;

        public StarController(StarView starView)
        {
            StarView = starView;

            var damageModel = new DamageModel(fatalDamage);
            starView.Init(damageModel);

            AddGameObject(starView.gameObject);
        }

    }
}