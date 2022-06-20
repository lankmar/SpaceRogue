using System.Collections.Generic;
using Abstracts;
using Scriptables.Modules;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;

namespace Gameplay.Player.FrontalGuns
{
    public class FrontalGunsController : BaseController
    {
        private readonly SubscribedProperty<bool> _primaryFireInput;
        private readonly List<TurretModuleConfig> _turretConfigs;
        private readonly List<FrontalTurretController> _turretControllers;

        private readonly ResourcePath _gunPointPrefab = new("Prefabs/Stuff/GunPoint");
        private readonly GameObject _gunPointView;

        public FrontalGunsController(SubscribedProperty<bool> primaryFireInput, List<TurretModuleConfig> turretConfigs, PlayerView playerView)
        {
            _primaryFireInput = primaryFireInput;
            _turretConfigs = turretConfigs;

            _gunPointView = ResourceLoader.LoadPrefab(_gunPointPrefab);
            _turretControllers = new List<FrontalTurretController>();

            foreach (var config in turretConfigs)
            {
                InitializeTurret(config, playerView);
            }
            
            _primaryFireInput.Subscribe(HandleFiring);
        }

        protected override void OnDispose()
        {
            _primaryFireInput.Unsubscribe(HandleFiring);
        }

        private void HandleFiring(bool isFiring)
        {
            foreach (var turret in _turretControllers)
            {
                if (isFiring) turret.CommenceFiring();
                turret.CoolDown();
            }
        }

        private void InitializeTurret(TurretModuleConfig turretConfig, PlayerView view)
        {
            var transform = view.transform;
            var turretPoint = Object.Instantiate(
                _gunPointView, 
                transform.TransformDirection(Vector3.up * 4), 
                transform.rotation
            );
            turretPoint.transform.parent = transform;
            var turretController = new FrontalTurretController(turretConfig, turretPoint);
            AddController(turretController);
            _turretControllers.Add(turretController);
        }
    }
}