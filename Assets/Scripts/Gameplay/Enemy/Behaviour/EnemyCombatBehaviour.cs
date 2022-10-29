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
        private Vector3 _currentDirection;
        private float _distance;
        private float _angle;
        private bool _inZone;

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
                ExitCombat();
            }

            _currentDirection = View.transform.TransformDirection(Vector3.up);
            var direction = PlayerView.transform.position - View.transform.position;
            _targetDirection = direction.normalized;
            _distance = direction.magnitude;
            _angle = Vector2.SignedAngle(_targetDirection, View.transform.up);

            CheckDetectionZone();
            RotateTowardsPlayer();
            Move();
            Shooting();
        }

        private void CheckDetectionZone()
        {
            if (_distance > Config.PlayerDetectionRadius)
            {
                _inZone = false;
                ExitCombat();
            }
            else
            {
                _inZone = true;
            }
        }

        private void Shooting()
        {
            if (!_inZone)
            {
                return;
            }

            if (Mathf.Abs(_angle) <= 22.5f)
            {
                _frontalTurret.CommenceFiring();
            }
        }

        private void Move()
        {
            if (UnityHelper.Approximately(_distance, Config.ShootingDistance, 0.05f))
            {
                return;
            }

            if (_distance < Config.ShootingDistance)
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
            if (_targetDirection == _currentDirection)
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
            if (_angle <= 0)
            {
                _inputController.TurnLeft();
            }
            else
            {
                _inputController.TurnRight();
            }
        }

        private void ExitCombat()
        {
            ChangeState(EnemyState.PassiveRoaming);
        }
    }
}