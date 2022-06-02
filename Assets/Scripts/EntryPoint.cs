using Gameplay.GameState;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _uiPosition;
    
    private const GameState InitialGameState = GameState.Game;

    private MainController _mainController;
    
    private void Awake()
    {
        var gameState = new CurrentState(InitialGameState);
        _mainController = new MainController(gameState, _uiPosition);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
