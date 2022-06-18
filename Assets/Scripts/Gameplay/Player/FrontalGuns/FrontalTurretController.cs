using Abstracts;
using Gameplay.Shooting;
using Scriptables.Modules;
using UnityEngine;
using Utilities.ResourceManagement;

namespace Gameplay.Player.FrontalGuns
{
    public class FrontalTurretController : BaseController
    {
        private readonly TurretModuleConfig _config;
        private readonly ProjectileFactory _projectileFactory;

        private float _cooldownTimer;

        public FrontalTurretController(TurretModuleConfig config, GameObject projectileSpawnGo)
        {
            _config = config;
            
            _projectileFactory = new ProjectileFactory(_config.ProjectileConfig, _config.ProjectileConfig.Prefab, projectileSpawnGo.transform);
            
            _cooldownTimer = 0.0f;
            
            AddGameObject(projectileSpawnGo);
        }

        public void CommenceFiring()
        {
            if (_cooldownTimer > 0)
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