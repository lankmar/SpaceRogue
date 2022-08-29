using Scriptables.Modules;
using UnityEngine;
using Utilities.Mathematics;
using Random = System.Random;

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

            FireSingleProjectile(_weaponConfig.SprayAngle);

            CooldownTimer = Config.SpecificWeapon.Cooldown;
        }

        public override void CoolDown()
        {
            BasicCoolDown();
        }
        private void FireSingleProjectile(int sprayAngle)
        {
            int angle = sprayAngle / 2;
            Random r = new Random();

            int pelletAngle = RandomPicker.PickRandomBetweenTwoValues(-angle, angle, r);
            Vector3 pelletVector = (pelletAngle + 90).ToVector3();
            //TODO check 90 degrees turn
            var projectile = ProjectileFactory.CreateProjectile(pelletVector);
            AddController(projectile);
        }
    }   
}