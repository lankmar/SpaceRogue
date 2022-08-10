using Gameplay.Player;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.Unity;

namespace Gameplay.Enemy.Movement
{
    public class EnemyBehaviourMovementModel
    {
        private readonly EnemyView _view;
        private readonly EnemyMovementModel _movementModel;
        private readonly PlayerView _player;
        private readonly System.Random _random;

        private const int RandomMovementAngle = 20;
        
        public bool IsFacingPlayer => UnityHelper.Approximately(CurrentDirection, PlayerDirection, 0.05f);

        private Vector3 CurrentDirection => _view.transform.TransformDirection(Vector3.up);
        private Vector3 PlayerDirection => (_view.transform.position - _player.transform.position).normalized;

        public EnemyBehaviourMovementModel(EnemyMovementModel movementModel, EnemyView view, PlayerView playerView)
        {
            _movementModel = movementModel;
            _view = view;
            _player = playerView;
            _random = new System.Random();
        }

        public void MoveForward()
        {
            if (_movementModel.CurrentSpeed < 0)
            {
                StopMoving();
                return;
            }
            
            _movementModel.Accelerate(true);
        }
        
        public void MoveForwardAtLowSpeed()
        {
            if (_movementModel.CurrentSpeed < 0)
            {
                StopMoving();
                return;
            }
            
            if (_movementModel.CurrentSpeed <= _movementModel.MaximumSpeed / 2) 
                _movementModel.Accelerate(true);
        }

        public void MoveBackward()
        {
            if (_movementModel.CurrentSpeed > 0)
            {
                StopMoving();
                return;
            }
            
            _movementModel.Accelerate(false);
        }
        
        public void RotateTowardsPlayer()
        {
            if (IsFacingPlayer)
            {
                _movementModel.StopTurning();
            }
            else
            {
                Rotate(CurrentDirection, PlayerDirection);
            }
        }

        public void RotateByRandomAngle()
        {
            //TODO Complete
            Transform transform = _view.transform;
            Vector3 targetDirection = RandomPicker.PickRandomAngle(transform.position, RandomMovementAngle, _random);
            Rotate(CurrentDirection, targetDirection);
        }

        public void StopMoving()
        {
            if (_movementModel.CurrentSpeed > _movementModel.StoppingSpeed)
            {
                _movementModel.Accelerate(false);
                return;
            }
            
            if (_movementModel.CurrentSpeed < -_movementModel.StoppingSpeed)
            {
                _movementModel.Accelerate(true);
                return;
            }
            
            _movementModel.StopMoving();
        }

        public void StopTurning()
        {
            _movementModel.StopTurning();
        }

        private void Rotate(Vector3 currentDirection, Vector3 targetDirection)
        {
            //TODO complete
            if ((currentDirection - targetDirection).normalized.x <= 0.0f)
            {
                _movementModel.TurnLeft();
                return;
            }
            _movementModel.TurnRight();
        }
    }
}