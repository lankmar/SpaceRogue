using Abstracts;
using Gameplay.Mechanics.Timer;
using Gameplay.Player;
using Scriptables.GameEvent;
using Utilities.Mathematics;
using Random = System.Random;

namespace Gameplay.GameEvent
{
    public abstract class GameEventController : BaseController
    {
        protected readonly GameEventConfig _config;
        protected readonly PlayerController _playerController;
        protected readonly Random _random = new();
        protected Timer _timer;

        private bool _isOnceSuccessfully;

        public GameEventController(GameEventConfig config, PlayerController playerController)
        {
            _config = config;
            _playerController = playerController;
            _playerController.PlayerDestroyed += OnPlayerDestroyed;
            _timer = new(_config.TimerValue);
            _timer.Start();

            EntryPoint.SubscribeToUpdate(CheckEvent);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _timer.Dispose();
            _playerController.PlayerDestroyed -= OnPlayerDestroyed;
            EntryPoint.UnsubscribeFromUpdate(CheckEvent);
        }

        private void CheckEvent()
        {
            if (_timer.IsExpired)
            {
                if(RandomPicker.TakeChance(_config.Chance, _random))
                {
                    _isOnceSuccessfully = RunGameEvent();
                }

                if (_config.IsRecurring || !_isOnceSuccessfully)
                {
                    _timer.Start();
                    return;
                }

                if (!_config.IsRecurring && _isOnceSuccessfully)
                {
                    EntryPoint.UnsubscribeFromUpdate(CheckEvent);
                }
            }
        }

        protected abstract bool RunGameEvent();

        protected virtual void OnPlayerDestroyed()
        {
            Dispose();
        }
    }
}