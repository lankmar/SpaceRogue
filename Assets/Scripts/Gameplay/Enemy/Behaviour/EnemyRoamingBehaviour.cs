using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyRoamingBehaviour : EnemyBehaviour
    {
        private readonly EnemyView _view;
        
        public EnemyRoamingBehaviour(SubscribedProperty<EnemyState> enemyState, EnemyView view) : base(enemyState)
        {
            _view = view;
        }
        
        protected override void OnUpdate()
        {
            //TODO move view randomly
        }
    }
}