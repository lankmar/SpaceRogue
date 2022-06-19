using Abstracts;
using Gameplay.Space.Star;
using UnityEngine;

namespace Gameplay.Space.Planet
{
    public class PlanetController : BaseController
    {
        private readonly PlanetView _view;
        private readonly float _currentSpeed;
        private const bool IsMovingRetrograde = false;
        
        private readonly StarView _starView;

        public PlanetController(PlanetView view, StarView starView, float speed)
        {
            _view = view;
            _starView = starView;
            _currentSpeed = speed;
            
            EntryPoint.SubscribeToUpdate(Move);
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(Move);
        }

        private void Move()
        {
            if (_starView is not null)
            {
                _view.transform.RotateAround(
                    _starView.transform.position,
                    IsMovingRetrograde ? Vector3.back : Vector3.forward,
                    _currentSpeed * Time.deltaTime
                );
            }
        }
    }
}