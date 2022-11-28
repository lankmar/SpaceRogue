using Asteroid;
using Gameplay.Damage;
using Gameplay.Player;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Asteroid.Behaviour 
{
    public class AsteroidLinearMotion : AsteroidBehaviour
    {
        private Vector3 _derection;
        private Vector3 _playerPosition;
        private float _playerDetectionRadius = 150;
        private float _speed = 10;
        Transform _asteroidTransform;
        IDamagingView _damagingView;

        public AsteroidLinearMotion(
            SubscribedProperty<AsteroidMoveType> moveType,
            AsteroidView view,
            PlayerView playerView,
            AsteroidBehaviourConfig config) : base(moveType, view, playerView, config)
        {
            _playerPosition = playerView.GetComponent<Transform>().transform.position;
            _asteroidTransform = view.GetComponent<Transform>();
            _derection = _playerPosition - _asteroidTransform.position;
            _derection.x = _derection.x + Random.Range(-10,10);
            _derection.z = _playerPosition.z + Random.Range(-10,10);
            _damagingView = view;
            _speed = config.AsteroidSpeed/100;
        }

        protected override void OnUpdate()
        {
            DetectPlayer();
            Move();
        }

        private void Move()
        {
            _asteroidTransform.position += new Vector3(_derection.x * _speed *Time.deltaTime, _derection.y * _speed * Time.deltaTime, _derection.z * _speed * Time.deltaTime); 
        }

        private void DetectPlayer()
        {
            if (Vector3.Distance(View.transform.position, PlayerView.transform.position) > _playerDetectionRadius)
            {
                View.TakeDamage(_damagingView);
                Debug.Log("Destroi Asteroid");
            }
        }
    }
}