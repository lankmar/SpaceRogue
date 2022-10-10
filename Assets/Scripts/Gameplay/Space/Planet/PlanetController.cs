using Abstracts;
using Gameplay.Space.Star;
using UnityEngine;

namespace Gameplay.Space.Planet
{
    public class PlanetController : BaseController
    {
        private readonly PlanetView _view;
        private readonly float _currentSpeed;
        private readonly bool _isMovingRetrograde;
        
        private readonly StarView _starView;

        public PlanetController(PlanetView view, StarView starView, float speed, bool isMovingRetrograde)
        {
            _view = view;
            AddGameObject(view.gameObject);
            _starView = starView;
            _currentSpeed = speed;
            _isMovingRetrograde = isMovingRetrograde;
            
            EntryPoint.SubscribeToUpdate(Move);
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(Move);
        }

        private void Move(float deltaTime)
        {
            if (_starView is not null)
            {
                _view.transform.RotateAround(
                    _starView.transform.position,
                    _isMovingRetrograde ? Vector3.forward : Vector3.back,
                    _currentSpeed * deltaTime
                );
            }
        }
    }
}