using Gameplay.Enemy;
using Scriptables.Modules;
using UnityEngine;

namespace Scriptables.Enemy
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "Configs/Enemy/" + nameof(EnemyConfig))]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyView Prefab { get; private set; }
        [field: SerializeField] public TurretModuleConfig Weapon { get; private set; }
    }
}