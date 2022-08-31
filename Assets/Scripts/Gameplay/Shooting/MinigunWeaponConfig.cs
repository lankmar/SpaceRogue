using System;
using Abstracts;
using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(MinigunWeaponConfig), menuName = "Configs/Weapons/" + nameof(MinigunWeaponConfig))]
    public class MinigunWeaponConfig : SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileConfig MinigunProjectile { get; private set; }
        [field: SerializeField, Range(0, 180)] public int SprayAngle { get; internal set; } = 1;
        [field: SerializeField, Range(1, 180)] public int MaxSprayAngle { get; private set; } = 20;
        [field: SerializeField, Min(0.1f)] public float OverheatCoolDown { get; private set; } = 2f;
        [field: SerializeField, Min(0.1f)] public int TimeToOverheat { get; private set; } = 5;
    }
}