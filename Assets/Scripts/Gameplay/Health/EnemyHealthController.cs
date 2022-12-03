using Gameplay.Enemy;
using Scriptables.Health;
using UI.Game;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;

namespace Gameplay.Health
{
    public sealed class EnemyHealthController : HealthController
    {
        private readonly Collider2D _collider;
        private readonly HealthStatusBarView _enemyStatusBarView;
        private readonly UnityEngine.Camera _camera;
        private readonly float _scaleFactor;
        private readonly float _healthBarOffset;

        private readonly SubscribedProperty<bool> _isVisible = new();


        public EnemyHealthController(HealthConfig healthConfig, EnemyView view) : base(healthConfig, view)
        {
        }

        public EnemyHealthController(HealthConfig healthConfig, ShieldConfig shieldConfig, EnemyView view) : base(healthConfig, shieldConfig, view)
        {
        }

        public EnemyHealthController(HealthConfig healthConfig, HealthStatusBarView statusBarView, EnemyView view, float healthBarOffset) : base(healthConfig, statusBarView, view)
        {
            _camera = UnityEngine.Camera.main;
            _enemyStatusBarView = statusBarView;
            _enemyStatusBarView.gameObject.SetActive(false);
            _collider = view.GetComponent<Collider2D>();
            _scaleFactor = GameUIController.EnemyHealthBars.GetComponentInParent<Canvas>().scaleFactor;
            _healthBarOffset = healthBarOffset;

            _isVisible.Subscribe(ShowHealthBar);
            EntryPoint.SubscribeToUpdate(FollowEnemy);
        }

        public EnemyHealthController(HealthConfig healthConfig, ShieldConfig shieldConfig, HealthShieldStatusBarView statusBarView, EnemyView view, float healthBarOffset) : base(healthConfig, shieldConfig, statusBarView, view)
        {
            _collider = view.GetComponent<Collider2D>();
            _enemyStatusBarView = statusBarView;
            _enemyStatusBarView.gameObject.SetActive(false);
            _camera = UnityEngine.Camera.main;
            _scaleFactor = GameUIController.EnemyHealthBars.GetComponentInParent<Canvas>().scaleFactor;
            _healthBarOffset = healthBarOffset;

            _isVisible.Subscribe(ShowHealthBar);
            EntryPoint.SubscribeToUpdate(FollowEnemy);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _isVisible.Unsubscribe(ShowHealthBar);
            EntryPoint.UnsubscribeFromUpdate(FollowEnemy);            
        }

        private void FollowEnemy()
        {
            if(_collider == null)
            {
                return;
            }

            _isVisible.Value = UnityHelper.IsObjectVisible(_camera, _collider.bounds);

            if (!_isVisible.Value)
            {
                return;
            }

            var position = _camera.WorldToScreenPoint(_collider.transform.position + Vector3.up * _healthBarOffset);
            position = new Vector3(position.x - Screen.width / 2, position.y - Screen.height / 2, 0);
            var finalPosition = new Vector3(position.x / _scaleFactor, position.y / _scaleFactor, 0);

            _enemyStatusBarView.GetComponent<RectTransform>().anchoredPosition = finalPosition;
        }

        private void ShowHealthBar(bool isVisible)
        {
            _enemyStatusBarView.gameObject.SetActive(isVisible);
        }
    }
}