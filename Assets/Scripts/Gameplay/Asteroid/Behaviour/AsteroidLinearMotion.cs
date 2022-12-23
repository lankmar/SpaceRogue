using Gameplay.Damage;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Asteroid.Behaviour
{
    public class AsteroidLinearMotion : AsteroidBehaviour
    {
        private Vector3 _derection;
        private Vector2 _derection2d;

        private Vector3 _playerPosition;
        private float _playerDetectionRadius = 150;
        private float _speed = 10;
        Transform _asteroidTransform;
        IDamagingView _damagingView;
        private int _spawnOffset = 5;

        public AsteroidLinearMotion(AsteroidView view,
            PlayerView playerView,
            AsteroidBehaviourConfig config) : base(view, playerView, config)
        {
            _playerPosition = playerView.GetComponent<Transform>().transform.position;

            _asteroidTransform = view.GetComponent<Transform>();
            _derection = _playerPosition - _asteroidTransform.position;
            _derection.x = _derection.x + Random.Range(-_spawnOffset, _spawnOffset);
            _derection.y = _derection.y + Random.Range(-_spawnOffset, _spawnOffset);
            _derection2d = new Vector2(_derection.x, _derection.y);

            _damagingView = view;
            _speed = config.AsteroidSpeed/10;

            EntryPoint.SubscribeToUpdate(Move);
            EntryPoint.SubscribeToUpdate(DetectPlayer);
        }

        protected override void OnDispose()
        {
            View.CollisionEnter -= Dispose;
            EntryPoint.UnsubscribeFromUpdate(Move);
            EntryPoint.UnsubscribeFromUpdate(DetectPlayer);
        }

        private void Move(float deltaTime)
        {
            View.GetComponent<Rigidbody2D>().AddForce(_derection2d * _speed * Time.deltaTime, ForceMode2D.Force);
        }

        private void DetectPlayer()
        {
            if (Vector3.Distance(View.transform.position, PlayerView.transform.position) > _playerDetectionRadius)
            {
                View.TakeDamage(_damagingView);
            }
        }
    }
}
