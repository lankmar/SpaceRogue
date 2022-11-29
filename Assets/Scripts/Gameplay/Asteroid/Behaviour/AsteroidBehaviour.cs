using System;
using Gameplay.Player;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Asteroid;

namespace Gameplay.Asteroid.Behaviour
{
    public class AsteroidBehaviour : MonoBehaviour
    {
        protected readonly AsteroidView View;
        protected readonly PlayerView PlayerView;
        protected readonly AsteroidBehaviourConfig Config;

        private readonly SubscribedProperty<AsteroidMoveType> _moveType;
        private bool _isDisposed;
        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            OnDispose();
            EntryPoint.UnsubscribeFromUpdate(OnUpdate);
        }
        protected AsteroidBehaviour(SubscribedProperty<AsteroidMoveType> moveType, AsteroidView view, PlayerView playerView, AsteroidBehaviourConfig config)
        {
            _moveType = moveType;
            View = view;
            PlayerView = playerView;
            Config = config;
            EntryPoint.SubscribeToUpdate(OnUpdate);
        }

        protected void ChangeMoveType(AsteroidMoveType newState)
        {
            if (newState != _moveType.Value) _moveType.Value = newState;
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDispose() { }
    }
}