using UnityEngine;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(PlanetConfig), menuName = "Configs/Space/" + nameof(PlanetConfig))]
    public class PlanetConfig : ScriptableObject
    {
        [field: SerializeField, Min(0.1f), Header("Movement")] public float MinSpeed { get; private set; } = 0.1f;
        [field: SerializeField, Min(1f)] public float MaxSpeed { get; private set; } = 1f;
        
        [field: SerializeField, Min(0.1f), Header("Size")] public float MinSize { get; private set; } = 0.1f;
        [field: SerializeField, Min(1f)] public float MaxSize { get; private set; } = 1f;
        
        [field: SerializeField, Min(0.1f), Header("Orbit")] public float MinOrbit { get; private set; } = 0.1f;
        [field: SerializeField, Min(1f)] public float MaxOrbit { get; private set; } = 1f;
        
    }
}