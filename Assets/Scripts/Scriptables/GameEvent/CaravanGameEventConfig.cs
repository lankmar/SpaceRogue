using Gameplay.GameEvent;
using Gameplay.Movement;
using Scriptables.Enemy;
using Scriptables.Health;
using UnityEngine;

namespace Scriptables.GameEvent
{
    [CreateAssetMenu(fileName = nameof(CaravanGameEventConfig), menuName = "Configs/GameEvent/" + nameof(CaravanGameEventConfig))]
    public sealed class CaravanGameEventConfig : BaseCaravanGameEventConfig
    {
        [field: SerializeField, Header("Caravan Settings"), Min(0)] public float AddHealth { get; private set; } = 50;
    }
}