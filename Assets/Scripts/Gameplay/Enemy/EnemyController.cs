using Abstracts;
using Gameplay.Shooting;
using Scriptables.Enemy;
using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyController : BaseController
    {
        private readonly EnemyView _view;
        private readonly EnemyConfig _config;
        private FrontalTurretController _turretController;

        public EnemyController(EnemyConfig config, EnemyView view)
        {
            _config = config;
            _view = view;
            InitializeTurret(_config.Weapon, _view.transform);
        }
        
        private void InitializeTurret(TurretModuleConfig turretConfig, Transform transform)
        {
            _turretController = new FrontalTurretController(turretConfig, transform);
            AddController(_turretController);
        }
    }
}