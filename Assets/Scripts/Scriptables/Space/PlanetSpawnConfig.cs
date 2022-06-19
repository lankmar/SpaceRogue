using UnityEngine;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(PlanetSpawnConfig), menuName = "Configs/Space/" + nameof(PlanetSpawnConfig))]
    public class PlanetSpawnConfig : ScriptableObject
    {
        [field: SerializeField, Min(0), Header("Spawn weights")] public int LivingPlanetWeight { get; private set; }
        [field: SerializeField, Min(0)] public int RockyPlanetWeight { get; private set; }
        [field: SerializeField, Min(0)] public int RuinedPlanetWeight { get; private set; }
        
        [field: SerializeField, Header("Configs")] public PlanetConfig LivingPlanetConfig { get; private set; }
        [field: SerializeField] public PlanetConfig RockyPlanetConfig { get; private set; }
        [field: SerializeField] public PlanetConfig RuinedPlanetConfig { get; private set; }
    }
}