using System;
using Asteroid;
using Gameplay.Asteroid;
using Gameplay.Asteroid.Behaviour;
using Scriptables.Health;
using UnityEngine;

namespace Scriptables.Asteroid
{
    public class AsteroidCloudConfig : MonoBehaviour
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public AsteroidView Prefab { get; private set; }
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField, Min(0.1f)] public float DamageAmount { get; private set; } = 2f;
        [field: SerializeField] public AsteroidMoveType AsteroidMoveType { get; private set; }
        [field: SerializeField] public AsteroidBehaviourConfig Behaviour { get; private set; }
        [field: SerializeField] public bool IsDestroyedOnHit { get; private set; } = true;
    }
}
