using System;
using UnityEngine;

namespace Scriptables.Enemy
{
    [Serializable]
    public class EnemyGroupSpawn
    {
        [field: SerializeField] public Vector3 GroupSpawnPoint { get; private set; }
        [field: SerializeField, Min(1)] public int GroupCount { get; private set; }
    }
}