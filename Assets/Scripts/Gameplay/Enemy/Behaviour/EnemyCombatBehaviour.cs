using Gameplay.Enemy.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyCombatBehaviour : EnemyBehaviour
    {
        private readonly EnemyInputController _inputController;
        private readonly FrontalTurretController _frontalTurret;
        private Vector3 _targetDirection;        

        public EnemyCombatBehaviour(
            SubscribedProperty<EnemyState> enemyState, 
            EnemyView view,
            PlayerController playerController,
            EnemyInputController inputController,
            FrontalTurretController frontalTurret,
            EnemyBehaviourConfig config) : base(enemyState, view, playerController, config)
        {
            _inputController = inputController;
            _frontalTurret = frontalTurret;
        }

        protected override void OnUpdate()
        {
            if (IsPlayerDead)
            {
                return;
            }

            RotateTowardsPlayer();
            Move();
            Shooting();
        }

        private void Shooting()
        {
            var currentDirection = View.transform.TransformDirection(Vector3.up);
            if (UnityHelper.Approximately(_targetDirection, currentDirection, 0.1f))
            {
                _frontalTurret.CommenceFiring();
            }
        }
        private void Move()
        {
            if (Vector3.Distance(PlayerView.transform.position, View.transform.position) <= Config.ShootingDistance)
            {
                _inputController.Decelerate();
            }
            else
            {
                _inputController.Accelerate();
            }
        }

        private void RotateTowardsPlayer()
        {
            _targetDirection = View.transform.worldToLocalMatrix.MultiplyPoint(PlayerView.transform.position).normalized;
            var currentDirection = View.transform.TransformDirection(Vector3.up);

            if (UnityHelper.Approximately(_targetDirection, currentDirection, 0.1f))
            {
                _inputController.StopTurning();
            }
            else
            {
                HandleTurn();
            }
            
        }

        private void HandleTurn()
        {
            if (_targetDirection.x <= 0)
            {
                _inputController.TurnLeft();
            }
            else
            {
                _inputController.TurnRight();
            }
        }
    }
}