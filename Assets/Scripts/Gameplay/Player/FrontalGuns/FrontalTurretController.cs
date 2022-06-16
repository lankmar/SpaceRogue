using Abstracts;
using Gameplay.Shooting;
using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Player.FrontalGuns
{
    public class FrontalTurretController : BaseController
    {
        private readonly TurretModuleConfig _config;

        private float CooldownTimer;

        public FrontalTurretController(TurretModuleConfig config)
        {
            _config = config;
            CooldownTimer = 0.0f;
        }

        public void CommenceFiring()
        {
            if (CooldownTimer > 0)
            {
                return;
            }

            var projectile = new ProjectileController(
                _config.ProjectileConfig, Vector3.forward); //TODO Replace with projectile spawn point
            AddController(projectile);

            CooldownTimer = _config.Cooldown;
        }

        public void CoolDown()
        {
            switch (CooldownTimer)
            {
                case 0:
                    return;
                case < 0:
                    CooldownTimer = 0;
                    return;
                case > 0:
                    CooldownTimer -= Time.deltaTime;
                    return;
            }
        }
    }
}