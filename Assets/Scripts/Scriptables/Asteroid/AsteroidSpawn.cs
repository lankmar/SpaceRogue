using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Asteroid
{
    [Serializable]
    public class AsteroidSpawn
    {
        [field: SerializeField] public Vector3 SpawnPoint { get; private set; }
    }
}