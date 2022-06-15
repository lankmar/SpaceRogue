using UnityEngine;

namespace Scriptables.Modules
{
    [CreateAssetMenu(fileName = nameof(TurretModuleConfig), menuName = "Configs/Modules/" + nameof(TurretModuleConfig))]
    public class TurretModuleConfig : BaseModuleConfig
    {
        [field: SerializeField] public float DamageAmount { get; private set; }
        [field: SerializeField, Min(0.1f)] public float Cooldown { get; private set; }
    }
}