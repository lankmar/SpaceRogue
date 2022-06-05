using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementModel
    {
        private readonly PlayerMovementConfig _config;

        private bool TurningLeft = false;
        public float CurrentSpeed { get; private set; }
        public float CurrentTurnRate { get; private set; }
        
        
        private float TurnSpeedDifference => _config.maximumTurnSpeed - _config.startingTurnSpeed;
        public PlayerMovementModel(PlayerMovementConfig config)
        {
            _config = config;
            CurrentSpeed = 0.0f;
            CurrentTurnRate = 0.0f;
        }

        public void Turn(float inputValue)
        {
            if (inputValue == 0 && CurrentTurnRate != 0)
            {
                StopTurning();
                return;
            }
            
            var isContinuingTurn = CurrentTurnRate < 0 == inputValue < 0;
            
            if (!isContinuingTurn)
            {
                CurrentSpeed = _config.startingTurnSpeed;
                return;
            }
            
            var turnAcceleration = CountAcceleration(TurnSpeedDifference, _config.turnAccelerationTime, inputValue, Time.deltaTime);

            if (CurrentTurnRate < _config.maximumTurnSpeed)
            {
                CurrentTurnRate += turnAcceleration;
                return;
            }
                
            CurrentTurnRate = _config.maximumTurnSpeed;
        }

        private void StopMoving() => CurrentSpeed = 0.0f;
        private void StopTurning() => CurrentTurnRate = 0.0f;
        
        private static float CountAcceleration(float speedDifference, float accelerationTime, float inputValue, float deltaTime)
        {
            if (accelerationTime <= 0) return speedDifference * deltaTime * inputValue * 10; //Prevents zero division
            return speedDifference * inputValue * deltaTime / accelerationTime;
        }
    }
}