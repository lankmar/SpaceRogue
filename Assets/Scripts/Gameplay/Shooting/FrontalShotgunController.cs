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
            _weaponConfig = shotgunConfig 
                ? shotgunConfig 
                : throw new System.Exception("Wrong config type was provided");
        }

        public override void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            }

            FireMultipleProjectiles(_weaponConfig.PelletCount, _weaponConfig.SprayAngle);

            CooldownTimer.Start();
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