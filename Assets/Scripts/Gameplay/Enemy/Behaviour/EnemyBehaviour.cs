using System;
using Gameplay.Player;
using Scriptables.Enemy;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public abstract class EnemyBehaviour : IDisposable
    {
        protected readonly EnemyView View;
        protected readonly PlayerView PlayerView;
        protected readonly EnemyBehaviourConfig Config;
        
        private readonly SubscribedProperty<EnemyState> _enemyState;
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            OnDispose();
            EntryPoint.UnsubscribeFromUpdate(OnUpdate);
        }

        protected EnemyBehaviour(SubscribedProperty<EnemyState> enemyState, EnemyView view, PlayerView playerView, EnemyBehaviourConfig config)
        {
            _enemyState = enemyState;
            View = view;
            PlayerView = playerView;
            Config = config;
            EntryPoint.SubscribeToUpdate(OnUpdate);
        }

        protected void ChangeState(EnemyState newState)
        {
            if (newState != _enemyState.Value) _enemyState.Value = newState;
        }
        
        protected abstract void OnUpdate();

        protected virtual void OnDispose() { }
    }
}