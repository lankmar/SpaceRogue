using System;
using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Player;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;
using Random = System.Random;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyRoamingBehaviour : EnemyBehaviour
    {
        private readonly MovementModel _movementModel;
        private readonly EnemyInputController _inputController;

        private readonly Random _random;
        private Vector3 _targetDirection; 
        
        public EnemyRoamingBehaviour(
            SubscribedProperty<EnemyState> enemyState,
            EnemyView view,
            PlayerView playerView, 
            MovementModel movementModel, 
            EnemyInputController inputController) : base(enemyState, view, playerView)
        {
            _movementModel = movementModel;
            _inputController = inputController;
            _random = new Random();
            _targetDirection = View.transform.TransformDirection(Vector3.up);
        }
        
        protected override void OnUpdate()
        {
            MoveAtLowSpeed();
            TurnToRandomDirection();
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
            var currentDirection = View.transform.TransformDirection(Vector3.up);

            if (UnityHelper.Approximately(_targetDirection, currentDirection, 0.1f))
            {
                _targetDirection = RandomPicker.PickRandomAngle(180, new Random());
                _inputController.StopTurning();
            }
            else
            {
                HandleTurn(currentDirection);
            }
        }

        private void HandleTurn(Vector3 currentDirection)
        {
            float x = (currentDirection - _targetDirection).x;
            float y = (currentDirection - _targetDirection).y;

            Action turnAction = (x, y) switch
            {
                (< 0, _) => _inputController.TurnLeft,
                (>= 0, _) => _inputController.TurnRight,
                _ => _inputController.TurnLeft
            };

            turnAction();
        }

        private void EnterCombat()
        {
            ChangeState(EnemyState.InCombat);
        }
    }
}