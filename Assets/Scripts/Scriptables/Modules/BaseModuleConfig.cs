using System;
using Abstracts;
using UnityEngine;

namespace Scriptables.Modules
{
    public abstract class BaseModuleConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
    }
}