using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalRailgunController : FrontalTurretController
    {
        private readonly RailgunWeaponConfig _weaponConfig;

        public FrontalRailgunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var railgunConfig = config.SpecificWeapon as RailgunWeaponConfig;
            if (railgunConfig is null)
            {
                throw new System.Exception("wrong config type was provided");
            }
            _weaponConfig = railgunConfig;

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

        public override void CoolDown(float deltaTime)
        {
            BasicCoolDown(deltaTime);
        }
    }   
}
