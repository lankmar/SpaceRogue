using Scriptables.GameEvent;

namespace Gameplay.GameEvent
{
    public sealed class CaravanGameEventController : GameEventController
    {
        private readonly CaravanGameEventConfig _caravanGameEventConfig;
        public CaravanGameEventController(GameEventConfig config) : base(config)
        {
            var caravanGameEventConfig = config as CaravanGameEventConfig;
            _caravanGameEventConfig = caravanGameEventConfig
                ? caravanGameEventConfig
                : throw new System.Exception("Wrong config type was provided");
        }

        protected override void RunGameEvent()
        {
            
        }
    }
}