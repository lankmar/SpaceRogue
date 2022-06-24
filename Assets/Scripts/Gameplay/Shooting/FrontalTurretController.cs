using Abstracts;
using Scriptables.Modules;
using UnityEngine;
using Utilities.ResourceManagement;

namespace Gameplay.Shooting
{
    public class FrontalTurretController : BaseController
    {
        public bool IsOnCooldown => _cooldownTimer > 0;
        
        private readonly TurretModuleConfig _config;
        private readonly ProjectileFactory _projectileFactory;
        
        private readonly ResourcePath _gunPointPrefab = new("Prefabs/Stuff/GunPoint");

        private float _cooldownTimer;

        public FrontalTurretController(TurretModuleConfig config, Transform gunPointParentTransform)
        {
            _config = config;
            var gunPointView = ResourceLoader.LoadPrefab(_gunPointPrefab);
            
            var turretPoint = Object.Instantiate(
                gunPointView, 
                gunPointParentTransform.TransformDirection(Vector3.up * gunPointParentTransform.localScale.y), 
                gunPointParentTransform.rotation
            );
            turretPoint.transform.parent = gunPointParentTransform;
            
            _projectileFactory = new ProjectileFactory(_config.ProjectileConfig, _config.ProjectileConfig.Prefab, turretPoint.transform);
            
            _cooldownTimer = 0.0f;
            
            AddGameObject(turretPoint);
        }

        public void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            }

            var projectile = _projectileFactory.CreateProjectile();
            AddController(projectile);

            _cooldownTimer = _config.Cooldown;
        }

        public void CoolDown()
        {
            switch (_cooldownTimer)
            {
                case 0:
                    return;
                case < 0:
                    _cooldownTimer = 0;
                    return;
                case > 0:
                    _cooldownTimer -= Time.deltaTime;
                    return;
            }
        }
    }
}