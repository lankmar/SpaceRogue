using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.Space
{
    [CreateAssetMenu(fileName = nameof(SpaceConfig), menuName = "Configs/Space/" + nameof(SpaceConfig))]
    public class SpaceConfig : ScriptableObject
    {
        [field: SerializeField] public List<Vector3> StarSpawnPoints { get; private set; }
    }
}