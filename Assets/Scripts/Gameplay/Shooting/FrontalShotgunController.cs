using Scriptables.Modules;
using UnityEngine;
using Utilities.Mathematics;
using Random = System.Random;

namespace Gameplay.Shooting
{
    public class FrontalShotgunController : FrontalTurretController
    {
        private readonly ShotgunWeaponConfig _weaponConfig;

        public FrontalShotgunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var shotgunConfig = config.SpecificWeapon as ShotgunWeaponConfig;
            if (shotgunConfig is null)
            {
                throw new System.Exception("wrong config type was provided");
            }
            _weaponConfig = shotgunConfig;

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

            FireMultipleProjectiles(_weaponConfig.PelletCount, _weaponConfig.SprayAngle);

            CooldownTimer = Config.SpecificWeapon.Cooldown;
        }

        public override void CoolDown()
        {
            BasicCoolDown();
        }

        private void FireMultipleProjectiles(int count, int sprayAngle)
        {
            int minimumAngle = -sprayAngle / 2;
            int singlePelletAngle = sprayAngle / count;
            Random r = new Random();

            for (int i = 0; i < count; i++)
            {
                int minimumPelletAngle = minimumAngle + i * singlePelletAngle;
                int maximumPelletAngle = minimumAngle + (i + 1) * singlePelletAngle;

                int pelletAngle = RandomPicker.PickRandomBetweenTwoValues(minimumPelletAngle, maximumPelletAngle, r);
                Vector3 pelletVector = (pelletAngle + 90).ToVector3();
                //TODO check 90 degrees turn
                var projectile = ProjectileFactory.CreateProjectile(pelletVector);
                AddController(projectile);
            }
        }
    }   
}