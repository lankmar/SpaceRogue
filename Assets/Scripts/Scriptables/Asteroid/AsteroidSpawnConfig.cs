using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Asteroid
{
    [CreateAssetMenu(fileName = nameof(AsteroidSpawnConfig), menuName = "Configs/Asteroid/" + nameof(AsteroidSpawnConfig))]
    public class AsteroidSpawnConfig : ScriptableObject
    {
        [field: SerializeField] public AsteroidConfig SingleAsteroid { get; private set; }
        [field: SerializeField] public AsteroidConfig FastAsteroid { get; private set; }
        [field: SerializeField] public AsteroidConfig AsteroidClouds { get; private set; }
        [field: SerializeField] public float MinTimeAsteroidSpawn { get; private set; }
        [field: SerializeField] public float MaxTimeAsteroidSpawn { get; private set; }

        [field: SerializeField] public List<AsteroidCloudSpawn> AsteroidCloudsSpawnPoints { get; private set; }
    }
}