using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Asteroid
{
    [CreateAssetMenu(fileName = nameof(AsteroidSpawnConfig), menuName = "Configs/Asteroid/" + nameof(AsteroidSpawnConfig))]
    public class AsteroidSpawnConfig : ScriptableObject
    {
        [field: Header("Asteroids Prefab")]
        [field: SerializeField] public List<AsteroidConfig> SingleAsteroid { get; private set; }
        [field: SerializeField] public List<AsteroidConfig> FastAsteroid { get; private set; }
        [field: SerializeField] public int SpawnOffset { get; private set; }
        [field: SerializeField] public float MinTimeAsteroidSpawn { get; private set; }
        [field: SerializeField] public float MaxTimeAsteroidSpawn { get; private set; }
        [field: SerializeField] public SmallCloudConfig SmallCloudConfig { get; private set; }

        [field: Header("Asteroid Clouds")]
        [field: SerializeField] public List<AsteroidConfig> AsteroidClouds { get; private set; }

        [field: SerializeField] public List<AsteroidCloudSpawn> AsteroidCloudsSpawnConfig { get; private set; }
    }
}