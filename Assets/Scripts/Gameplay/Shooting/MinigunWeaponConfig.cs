using System;
using Abstracts;
using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(MinigunWeaponConfig), menuName = "Configs/Weapons/" + nameof(MinigunWeaponConfig))]
    public class MinigunWeaponConfig : SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileConfig MinigunProjectile { get; private set; }
        [field: SerializeField, Range(0, 180)] public int SprayAngle { get; private set; } = 3;
        [field: SerializeField, Range(1, 180)] public int SprayChange { get; private set; } = 10;
        [field: SerializeField, Min(0.1f)] public float OverheatCoolDown { get; private set; } = 1f;
        [field: SerializeField, Min(0.1f)] public float TimeToOverheat { get; private set; } = 10f;
    }
}