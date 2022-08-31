using Scriptables.Modules;
using UnityEngine;
using Utilities.Mathematics;
using Random = System.Random;

namespace Gameplay.Shooting
{
    public class FrontalMinigunController : FrontalTurretController
    {
        private readonly MinigunWeaponConfig _weaponConfig;

        internal bool IsOverheated => _overheatCooldown > 0.0f;
        private float _overheatMeter;
        private float _overheatCooldown;

        private int _sprayAngle;
        private int _sprayAngleIncrease;

        public FrontalMinigunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var minigunConfig = config.SpecificWeapon as MinigunWeaponConfig;
            if (minigunConfig is null)
            {
                throw new System.Exception("wrong config type was provided");
            }
            _weaponConfig = minigunConfig;

            _overheatCooldown = 0.0f;
            _overheatMeter = 0.0f;

            _sprayAngle = _weaponConfig.SprayAngle;
            _sprayAngleIncrease = _weaponConfig.MaxSprayAngle - _sprayAngle / _weaponConfig.TimeToOverheat;

            EntryPoint.SubscribeToUpdate(CoolDown);
        }
        
        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(CoolDown);
        }

        public override void CommenceFiring()
        {
            if (IsOnCooldown || IsOverheated)
            {
                RemoveHeat();
                return;
            }

            FireSingleProjectile(_sprayAngle);
            Debug($"{_sprayAngle}");
            AddHeat();

            CooldownTimer = Config.SpecificWeapon.Cooldown;
        }

        public override void CoolDown()
        {
            BasicCoolDown();
        }

        private void AddHeat()
        {
            if (_overheatMeter < _weaponConfig.TimeToOverheat)
            {
                _overheatMeter += _weaponConfig.Cooldown;
                _sprayAngle += _sprayAngleIncrease;
                return;
            }

            _overheatCooldown = _weaponConfig.OverheatCoolDown;
            _overheatMeter = 0.0f;
        }

        private void RemoveHeat()
        {
            if (_overheatCooldown <= Time.deltaTime)
            {
                _overheatCooldown = 0.0f;
                _sprayAngle = _weaponConfig.SprayAngle;
                return;
            }
            _overheatCooldown -= Time.deltaTime;
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