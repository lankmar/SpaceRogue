using Gameplay.GameEvent;
using Gameplay.Movement;
using Scriptables.Enemy;
using Scriptables.Health;
using UnityEngine;

namespace Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(CaravanGameEventConfig), menuName = "Configs/GameEvent/" + nameof(CaravanGameEventConfig))]
    public sealed class CaravanGameEventConfig : GameEventConfig
    {
        [field: SerializeField, Header("Caravan Settings")] public CaravanView CaravanView { get; private set; }
        [field: SerializeField] public MovementConfig Movement { get; private set; }
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField] public ShieldConfig Shield { get; private set; }
        [field: SerializeField, Min(0)] public float AddHealth { get; private set; } = 50;
        [field: SerializeField, Min(0)] public float SpawnOffset { get; private set; } = 5;
        [field: SerializeField, Min(0)] public float PathDistance { get; private set; } = 100;
        [field: SerializeField, Header("Enemy Settings")] public EnemyConfig EnemyConfig { get; private set; }
        [field: SerializeField, Min(0)] public int EnemyCount { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 10;
    }
}