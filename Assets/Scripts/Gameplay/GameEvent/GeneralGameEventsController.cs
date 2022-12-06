using Abstracts;
using Scriptables.GameEvent;
using System;
using Utilities.ResourceManagement;

namespace Gameplay.GameEvent
{
    public sealed class GeneralGameEventsController : BaseController
    {
        private readonly ResourcePath _configPath = new(Constants.Configs.GameEvent.GeneralGameEventConfig);
        private readonly GeneralGameEventConfig _config;        

        public GeneralGameEventsController()
        {
            _config = ResourceLoader.LoadObject<GeneralGameEventConfig>(_configPath);

            foreach (var gameEvent in _config.GameEvents)
            {
                InitializeGameEvent(gameEvent);
            }
        }

        private void InitializeGameEvent(GameEventConfig gameEvent)
        {
            var gameEventController = CreateGameEvent(gameEvent);
            AddController(gameEventController);
        }

        private GameEventController CreateGameEvent(GameEventConfig gameEvent)
        {
            return gameEvent.GameEventType switch
            {
                GameEventType.Empty => new EmptyGameEventController(gameEvent),
                GameEventType.Comet => new CometGameEventController(gameEvent),
                GameEventType.Supernova => new SupernovaGameEventController(gameEvent),
                GameEventType.Caravan => new CaravanGameEventController(gameEvent),
                GameEventType.CaravanTrap => new CaravanTrapGameEventController(gameEvent),
                _ => throw new ArgumentOutOfRangeException(nameof(gameEvent.GameEventType), gameEvent.GameEventType, "A not-existent game event type is provided")
            };
        }
    }
}