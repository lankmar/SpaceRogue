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

        private float _currentSprayAngle;

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

            _currentSprayAngle = _weaponConfig.SprayAngle;

            EntryPoint.SubscribeToUpdate(CoolDown);
        }
        
        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(CoolDown);
        }

        public override void CommenceFiring()
        {
            if (IsOverheated || IsOnCooldown)
            {
                return;
            }

            FireSingleProjectile();

            AddHeat();
            CooldownTimer = Config.SpecificWeapon.Cooldown;
        }

        public override void CoolDown()
        {
            BasicCoolDown();
            if (_overheatCooldown > 0.0f) RemoveHeat();
        }

        private void AddHeat()
        {
            if (_overheatMeter < _weaponConfig.TimeToOverheat)
            {
                _overheatMeter += _weaponConfig.Cooldown;
                IncreaseSpray();
                return;
            }

            TriggerOverheatAndReset();
        }

        private void TriggerOverheatAndReset()
        {
            _overheatCooldown = _weaponConfig.OverheatCoolDown;
        }

        private void IncreaseSpray()
        {
            if (_currentSprayAngle >= _weaponConfig.MaxSprayAngle) return;
            var sprayIncrease = CountSprayIncrease();
            _currentSprayAngle += sprayIncrease;
        }

        private float CountSprayIncrease()
        {
            return (_weaponConfig.MaxSprayAngle - _weaponConfig.SprayAngle) / (_weaponConfig.TimeToOverheat * (1 / _weaponConfig.Cooldown));
        }

        private void RemoveHeat()
        {
            if (_overheatCooldown <= Time.deltaTime)
            {
                ResetWeapon();
                return;
            }
            _overheatCooldown -= Time.deltaTime;
        }

        private void ResetWeapon()
        {
            _overheatCooldown = 0.0f;
            _overheatMeter = 0.0f;
            _currentSprayAngle = _weaponConfig.SprayAngle;
        }

        private void FireSingleProjectile()
        {
            float angle = _currentSprayAngle / 2;
            Random r = new Random();

            float pelletAngle = RandomPicker.PickRandomBetweenTwoValues(-angle, angle, r);
            Vector3 pelletVector = (pelletAngle + 90).ToVector3();
            //TODO check 90 degrees turn
            var projectile = ProjectileFactory.CreateProjectile(pelletVector);
            AddController(projectile);
        }
    }   
}