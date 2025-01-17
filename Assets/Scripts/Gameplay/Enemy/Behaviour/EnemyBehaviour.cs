using System;
using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;
namespace Gameplay.Enemy.Behaviour
{
    public abstract class EnemyBehaviour : IDisposable
    {
        protected readonly EnemyView View;
        protected readonly PlayerView PlayerView;
        protected readonly EnemyBehaviourConfig Config;

        private readonly PlayerController _playerController;

        private readonly SubscribedProperty<EnemyState> _enemyState;
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            OnDispose();
            _playerController.PlayerDestroyed -= OnPlayerDestroyed;
            EntryPoint.UnsubscribeFromUpdate(DetectPlayer);
            EntryPoint.UnsubscribeFromUpdate(OnUpdate);
        }

        protected EnemyBehaviour(SubscribedProperty<EnemyState> enemyState, EnemyView view, PlayerController playerController, EnemyBehaviourConfig config)
        {
            _enemyState = enemyState;
            View = view;
            _playerController = playerController;
            _playerController.PlayerDestroyed += OnPlayerDestroyed;
            PlayerView = _playerController.View;
            Config = config;
            EntryPoint.SubscribeToUpdate(DetectPlayer);
            EntryPoint.SubscribeToUpdate(OnUpdate);
        }

        protected void ChangeState(EnemyState newState)
        {
            if (newState != _enemyState.Value) _enemyState.Value = newState;
        }
        
        protected abstract void OnUpdate();
        protected virtual void OnDispose() { }

        protected abstract void DetectPlayer();

        private void OnPlayerDestroyed()
        {
            EntryPoint.UnsubscribeFromUpdate(DetectPlayer);
            
            if(_enemyState.Value == EnemyState.InCombat)
            {
                ChangeState(EnemyState.PassiveRoaming);
            }
        }
    }
}