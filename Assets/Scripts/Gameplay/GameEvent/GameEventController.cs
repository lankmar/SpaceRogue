using Abstracts;
using Gameplay.Mechanics.Timer;
using Scriptables.GameEvent;
using Utilities.Mathematics;
using Random = System.Random;

namespace Gameplay.GameEvent
{
    public abstract class GameEventController : BaseController
    {
        protected readonly GameEventConfig _config;
        protected readonly Random _random = new();
        protected Timer _timer;

        public GameEventController(GameEventConfig config)
        {
            _config = config;
            _timer = new(_config.TimerValue);
            _timer.Start();

            EntryPoint.SubscribeToUpdate(CheckEvent);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _timer.Dispose();
            EntryPoint.UnsubscribeFromUpdate(CheckEvent);
        }

        private void CheckEvent()
        {
            if (_timer.IsExpired)
            {
                if(RandomPicker.TakeChance(_config.Chance, _random))
                {
                    RunGameEvent();
                }

                if (_config.IsRecurring)
                {
                    _timer.Start();
                }
                else
                {
                    EntryPoint.UnsubscribeFromUpdate(CheckEvent);
                }
            }
        }

        protected abstract void RunGameEvent();
    }
}