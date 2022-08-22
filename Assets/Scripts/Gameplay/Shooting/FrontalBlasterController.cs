using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalBlasterController : FrontalTurretController
    {
        private readonly BlasterWeaponConfig _config;

        public FrontalBlasterController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var blasterConfig = config.SpecificWeapon as BlasterWeaponConfig;
            if (blasterConfig is null)
            {
                throw new System.Exception("wrong config type was provided");
            }
            _config = blasterConfig;

            EntryPoint.SubscribeToUpdate(CoolDown);
        }
        
        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(CoolDown);
        }

        public override void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            }

            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);

            CooldownTimer = Config.Cooldown;
        }

        public override void CoolDown()
        {
            BasicCoolDown();
        }
    }
}