using System;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    [Serializable]
    public class PlayerMovementModel
    {
        [Header("Speed")] 
        [SerializeField] public float MaximumSpeed;
        [SerializeField] public float AccelerationTime;

        [Header("Turn speed")] 
        [SerializeField] public float StartingTurnSpeed;
        [SerializeField] public float MaximumTurnSpeed;
        [SerializeField] public float TurnAccelerationTime;
    }
}