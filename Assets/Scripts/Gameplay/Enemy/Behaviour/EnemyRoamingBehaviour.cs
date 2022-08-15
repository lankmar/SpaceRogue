using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;

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
            MoveAtLowSpeed();
        }

        private void MoveAtLowSpeed()
        {
            var quarterMaxSpeed = _movementModel.MaxSpeed / 4;
            switch (CompareSpeeds(_movementModel.CurrentSpeed, quarterMaxSpeed))
            {
                case -1: { _inputController.Accelerate(); return; }
                case 0: { _inputController.HoldSpeed(); return; }
                case 1: { _inputController.Decelerate(); return; }
            }
        }

        private int CompareSpeeds(float currentSpeed, float targetSpeed)
        {
            if (UnityHelper.Approximately(currentSpeed, targetSpeed, 0.1f)) return 0;
            if (currentSpeed < targetSpeed) return -1;
            return 1;
        }

        private void TurnToRandomDirection()
        {
            
        }

        private void EnterCombat()
        {
            ChangeState(EnemyState.InCombat);
        }
    }
}