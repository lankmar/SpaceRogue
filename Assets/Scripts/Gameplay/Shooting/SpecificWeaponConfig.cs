using System;
using Abstracts;
using UnityEngine;

namespace Gameplay.Shooting
{
    public abstract class SpecificWeaponConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
    }
}