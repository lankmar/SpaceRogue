using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.GameState
{
    public class CurrentState
    {
        public readonly SubscribedProperty<GameState> CurrentGameState;

        public CurrentState(GameState initialState)
        {
            CurrentGameState = new SubscribedProperty<GameState>(initialState);
        }
    }
}