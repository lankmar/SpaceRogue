using System;
using Abstracts;
using Asteroid;
using Gameplay.Asteroid;
using Gameplay.Asteroid.Behaviour;
using Gameplay.Movement;
using Scriptables.Health;
using UnityEngine;

namespace Scriptables.Asteroid
{
    [CreateAssetMenu(fileName = nameof(AsteroidConfig), menuName = "Configs/Asteroid/" + nameof(AsteroidConfig))]
    public class AsteroidConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public AsteroidView Prefab { get; private set; }
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField, Min(0.1f)] public float DamageAmount { get; private set; } = 2f;
        [field: SerializeField] public AsteroidType AsteroidType { get; private set; }
        [field: SerializeField] public AsteroidSizeType AsteroidSizeType { get; private set; }
        [field: SerializeField, Min(0.1f)] public float AsteroidSize { get; private set; }
        [field: SerializeField] public AsteroidMoveType AsteroidMoveType { get; private set; }
        [field: SerializeField] public float AsteroidSpeed { get; private set; }
        [field: SerializeField] public AsteroidBehaviourConfig Behaviour { get; private set; }

    }
}