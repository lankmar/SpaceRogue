using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyRoamingBehaviour : EnemyBehaviour
    {
        private readonly MovementModel _movementModel;
        private readonly EnemyInputController _inputController;
        
        public EnemyRoamingBehaviour(
            SubscribedProperty<EnemyState> enemyState,
            PlayerView playerView, 
            MovementModel movementModel, 
            EnemyInputController inputController) : base(enemyState, playerView)
        {
            _movementModel = movementModel;
            _inputController = inputController;
        }
        
        protected override void OnUpdate()
        {
            //TODO move view randomly
        }
    }
}