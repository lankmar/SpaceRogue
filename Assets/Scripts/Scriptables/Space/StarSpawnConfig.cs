using UnityEngine;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(StarSpawnConfig), menuName = "Configs/Space/" + nameof(StarSpawnConfig))]
    public class StarSpawnConfig : ScriptableObject
    {
        [field: SerializeField, Min(0), Header("Spawn weights")] public int SunStarWeight { get; private set; }
        [field: SerializeField, Min(0)] public int RedGiantWeight { get; private set; }
        [field: SerializeField, Min(0)] public int WhiteDwarfWeight { get; private set; }
        
        [field: SerializeField, Min(0), Header("Configs")] public StarConfig SunStarConfig { get; private set; }
        [field: SerializeField, Min(0)] public StarConfig RedGiantConfig { get; private set; }
        [field: SerializeField, Min(0)] public StarConfig WhiteDwarfConfig { get; private set; }
    }
}