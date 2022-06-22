using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(HealthConfig), menuName = "Configs/Health" + nameof(HealthConfig))]
    public class HealthConfig : ScriptableObject
    {
        [field: SerializeField, Min(1f)] public float MaximumHealth { get; private set; }
        [field: SerializeField, Min(1f)] public float StartingHealth { get; private set; }
        [field: SerializeField, Min(0f)] public float HealthRegen { get; private set; }
    }
}