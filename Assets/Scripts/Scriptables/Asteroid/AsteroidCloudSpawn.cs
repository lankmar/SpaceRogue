using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Asteroid
{
    [Serializable]
    public class AsteroidCloudSpawn
    {
        [field: SerializeField] public Vector3 GroupSpawnPoint { get; private set; }
        [field: SerializeField, Min(1)] public int GroupCount { get; private set; }
    }
}