using Scriptables.GameEvent;

namespace Gameplay.GameEvent
{
    public sealed class CometGameEventController : GameEventController
    {
        private readonly CometGameEventConfig _cometGameEventConfig;

        public CometGameEventController(GameEventConfig config) : base(config)
        {
            var cometGameEventConfig = config as CometGameEventConfig;
            _cometGameEventConfig = cometGameEventConfig
                ? cometGameEventConfig
                : throw new System.Exception("Wrong config type was provided");
        }

        protected override void RunGameEvent()
        {
            
        }

    }
}