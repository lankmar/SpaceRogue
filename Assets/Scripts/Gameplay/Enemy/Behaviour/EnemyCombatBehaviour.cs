using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using Scriptables.Enemy;
using UnityEditor;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyCombatBehaviour : EnemyBehaviour
    {
        private readonly MovementModel _movementModel;
        private readonly EnemyInputController _inputController;
        private readonly EnemyView _enemyView;
        private readonly PlayerView _playerView;
        private readonly FrontalTurretController _frontalTurret;
        private readonly float _shootingDistance;
        private Vector3 _targetDirection;        

        public EnemyCombatBehaviour(
            SubscribedProperty<EnemyState> enemyState, 
            EnemyView view,
            PlayerView playerView, 
            MovementModel movementModel, 
            EnemyInputController inputController,
            FrontalTurretController frontalTurret,
            EnemyConfig config) : base(enemyState, view, playerView)
        {
            _movementModel = movementModel;
            _inputController = inputController;
            _frontalTurret = frontalTurret;
            _enemyView = view;
            _playerView = playerView;
            _shootingDistance = config.Behaviour.ShootingDistance;
        }

        protected override void OnUpdate()
        {
            TurnToRandomDirection();
            Move();
            Shooting();
        }

        private void Shooting()
        {
            var currentDirection = View.transform.InverseTransformDirection(Vector3.up);
            if (!UnityHelper.Approximately(_targetDirection, currentDirection, 0.1f))
            {
                _frontalTurret.CommenceFiring();
            }
        }
        private void Move()
        {
            if (Vector3.Distance(_playerView.transform.position, View.transform.position) <= _shootingDistance)
            {
                _inputController.Decelerate();
                return;
            }
            var quarterMaxSpeed = _movementModel.MaxSpeed / 2;
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
            _targetDirection = _enemyView.transform.worldToLocalMatrix.MultiplyPoint(_playerView.transform.position).normalized;
            var currentDirection = View.transform.InverseTransformDirection(Vector3.up);

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