using Scriptables.GameEvent;

namespace Gameplay.GameEvent
{
    public sealed class EmptyGameEventController : GameEventController
    {
        private readonly EmptyGameEventConfig _emptyGameEventConfig;
        public EmptyGameEventController(GameEventConfig config) : base(config)
        {
            var emptyGameEventConfig = config as EmptyGameEventConfig;
            _emptyGameEventConfig = emptyGameEventConfig
                ? emptyGameEventConfig
                : throw new System.Exception("Wrong config type was provided");
        }

        protected override void RunGameEvent()
        {
            Debug($"EmptyGameEvent completed");
        }
    }
}