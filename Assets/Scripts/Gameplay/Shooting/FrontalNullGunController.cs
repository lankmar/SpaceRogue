using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public sealed class FrontalNullGunController : FrontalTurretController
    {
        public FrontalNullGunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
        }

        public override void CommenceFiring()
        {
            UnityEngine.Debug.Log("Null-gun has fired!");
        }
    }
}