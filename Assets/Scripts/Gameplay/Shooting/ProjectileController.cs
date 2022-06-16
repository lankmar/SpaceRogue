using Abstracts;
using Gameplay.Damage;
using UnityEngine;
using Utilities.ResourceManagement;

namespace Gameplay.Shooting
{
    public class ProjectileController : BaseController
    {
        private readonly ProjectileConfig _config;
        private readonly DamageModel _damageModel;
        private readonly ProjectileView _view;
        private float _remainingLifeTime;
        
        public ProjectileController(ProjectileConfig config, Vector3 spawnPosition)
        {
            _config = config;
            _view = LoadView<ProjectileView>(new ResourcePath(config.PrefabPath));
            _damageModel = new DamageModel(config.DamageAmount);
            _remainingLifeTime = config.LifeTime;
            
            EntryPoint.SubscribeToUpdate(TickDown);
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(TickDown);
        }

        private void TickDown()
        {
            if (_remainingLifeTime <= 0)
            {
                Dispose();
                return;
            }

            _remainingLifeTime -= Time.deltaTime;
        }
        
        //TODO init view and subscribe to onCollisionEnter
    }
}