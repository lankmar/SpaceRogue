using System;
using System.Threading.Tasks;
using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Player;
using Scriptables.Enemy;
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
        
        private Vector3 _targetDirection;
        
        private readonly float _playerDetectionRadius;
        private readonly float _timeToPickNewAngle;
        private float _timeBeforeUpdate;
        private bool _flagPicker;
        
        public EnemyRoamingBehaviour(
            SubscribedProperty<EnemyState> enemyState,
            EnemyView view,
            PlayerView playerView, 
            MovementModel movementModel, 
            EnemyInputController inputController) : base(enemyState, view, playerView)
        {
            _playerDetectionRadius = Config.PlayerDetectionRadius;
            _timeToPickNewAngle = Config.TimeToPickNewAngle;
            _movementModel = movementModel;
            _inputController = inputController;
        }
        
        protected override void OnUpdate()
        {
            if (!_flagPicker)
            {
                PickRandomAngle(); 
            }
            TimeToUpdateMethod();
            ChangerState();
            MoveAtLowSpeed();
            TurnToRandomDirection();
        }

        private void PickRandomAngle()
        {
            _targetDirection = View.transform.worldToLocalMatrix.MultiplyPoint(RandomPicker.PickRandomAngle(180, new Random())).normalized;
        }

        private void TimeToUpdateMethod()
        {
            _flagPicker =  true;
            if (_timeBeforeUpdate <= _timeToPickNewAngle)
            {
                _timeBeforeUpdate += Time.deltaTime;
            }
            else
            {
                _timeBeforeUpdate = 0;
                _flagPicker = false;
            }
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

        private void ChangerState()
        {
            if (Vector3.Distance(View.transform.position, PlayerView.transform.position) < _playerDetectionRadius)
            {
                EnterCombat();
            }
        }
        private void EnterCombat()
        {
            ChangeState(EnemyState.InCombat);
        }
    }
}