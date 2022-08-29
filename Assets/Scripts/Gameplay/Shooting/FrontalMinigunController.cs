using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalMinigunController : FrontalTurretController
    {
        private readonly MinigunWeaponConfig _weaponConfig;

        public FrontalMinigunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var minigunConfig = config.SpecificWeapon as MinigunWeaponConfig;
            if (minigunConfig is null)
            {
                throw new System.Exception("wrong config type was provided");
            }
            _weaponConfig = minigunConfig;

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

            CooldownTimer = Config.SpecificWeapon.Cooldown;
        }

        public override void CoolDown()
        {
            BasicCoolDown();
        }
    }   
}