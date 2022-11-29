using System;
using UnityEngine;

namespace Gameplay.Asteroid.Behaviour
{
    [Serializable]
    public class AsteroidBehaviourConfig 
    {
        [field: SerializeField] public float AsteroidSpeed { get; private set; }
        [field: SerializeField] public float PlayerDetectionRadius { get; private set; }
    }
}