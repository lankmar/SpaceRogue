using Gameplay.Player;
using Scriptables.GameEvent;

namespace Gameplay.GameEvent
{
    public sealed class CaravanGameEventController : GameEventController
    {
        private readonly CaravanGameEventConfig _caravanGameEventConfig;

        public CaravanGameEventController(GameEventConfig config, PlayerController playerController) : base(config, playerController)
        {
            var caravanGameEventConfig = config as CaravanGameEventConfig;
            _caravanGameEventConfig = caravanGameEventConfig
                ? caravanGameEventConfig
                : throw new System.Exception("Wrong config type was provided");
        }

        protected override bool RunGameEvent()
        {
            return true;
        }
    }
}