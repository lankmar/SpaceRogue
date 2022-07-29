using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalNullGunController : FrontalTurretController
    {
        public FrontalNullGunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
        }

        public override void CommenceFiring()
        {
            UnityEngine.Debug.Log("Null-gun has fired!");
        }

        public override void CoolDown()
        {
            UnityEngine.Debug.Log("Null-gun is cooling down");
        }
    }
}