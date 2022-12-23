using Gameplay.Player;

namespace Gameplay.Asteroid.Behaviour
{
    public class AsteroidBehaviour
    {
        protected readonly AsteroidView View;
        protected readonly PlayerView PlayerView;
        protected readonly AsteroidBehaviourConfig Config;

        private bool _isDisposed;
        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            OnDispose();
            EntryPoint.UnsubscribeFromUpdate(OnUpdate);
        }
        protected AsteroidBehaviour(AsteroidView view, PlayerView playerView, AsteroidBehaviourConfig config)
        {
            View = view;
            PlayerView = playerView;
            Config = config;
            EntryPoint.SubscribeToUpdate(OnUpdate);
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDispose() { }
    }
}