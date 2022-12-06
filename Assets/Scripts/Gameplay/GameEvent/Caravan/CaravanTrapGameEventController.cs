using Scriptables.GameEvent;

namespace Gameplay.GameEvent
{
    public sealed class CaravanTrapGameEventController : GameEventController
    {
        private readonly CaravanTrapGameEventConfig _caravanTrapGameEventConfig;
        public CaravanTrapGameEventController(GameEventConfig config) : base(config)
        {
            var caravanTrapGameEventConfig = config as CaravanTrapGameEventConfig;
            _caravanTrapGameEventConfig = caravanTrapGameEventConfig
                ? caravanTrapGameEventConfig
                : throw new System.Exception("Wrong config type was provided");
        }

        protected override void RunGameEvent()
        {
            
        }
    }
}