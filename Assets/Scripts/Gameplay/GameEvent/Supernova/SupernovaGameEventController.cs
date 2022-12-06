using Scriptables.GameEvent;

namespace Gameplay.GameEvent
{
    public sealed class SupernovaGameEventController : GameEventController
    {
        private readonly SupernovaGameEventConfig _supernovaGameEventConfig;
        public SupernovaGameEventController(GameEventConfig config) : base(config)
        {
            var supernovaGameEventConfig = config as SupernovaGameEventConfig;
            _supernovaGameEventConfig = supernovaGameEventConfig
                ? supernovaGameEventConfig
                : throw new System.Exception("Wrong config type was provided");
        }

        protected override void RunGameEvent()
        {
            
        }
    }
}