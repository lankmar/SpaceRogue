using Gameplay.Player;
using Scriptables.GameEvent;

namespace Gameplay.GameEvent
{
    public sealed class CaravanTrapGameEventController : GameEventController
    {
        private readonly CaravanTrapGameEventConfig _caravanTrapGameEventConfig;

        public CaravanTrapGameEventController(GameEventConfig config, PlayerController playerController) : base(config, playerController)
        {
            var caravanTrapGameEventConfig = config as CaravanTrapGameEventConfig;
            _caravanTrapGameEventConfig = caravanTrapGameEventConfig
                ? caravanTrapGameEventConfig
                : throw new System.Exception("Wrong config type was provided");
        }

        protected override bool RunGameEvent()
        {
            return true;
        }
    }
}