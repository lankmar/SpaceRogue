using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalShotgunController : FrontalTurretController
    {
        private readonly ShotgunWeaponConfig _config;

        public FrontalShotgunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var shotgunConfig = config.SpecificWeapon as ShotgunWeaponConfig;
            if (shotgunConfig is null)
            {
                throw new System.Exception("wrong config type was provided");
            }
            _config = shotgunConfig;

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