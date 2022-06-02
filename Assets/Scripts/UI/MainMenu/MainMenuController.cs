using Abstracts;
using Gameplay.GameState;
using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly Transform _uiPosition;

        public MainMenuController(CurrentState currentState, Transform uiPosition)
        {
            _currentState = currentState;
            _uiPosition = uiPosition;
            
            //TODO Menu load
            
            //TODO Remake when menu will be done
            _currentState.CurrentGameState.Value = GameState.Game;
        }
    }
}