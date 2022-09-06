using Abstracts;
using Gameplay;
using System;
using UI.Abstracts;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ResourceManagement;

namespace UI.Game
{
    public class GameUIController : BaseController
    {
        public static PlayerStatusBarView PlayerStatusBarView { get; private set; }
        public static PlayerSpeedometerView PlayerSpeedometerView { get; private set; }
        public DestroyPlayerMessageView DestroyPlayerViewComponent { get; private set; }

        private readonly Canvas _mainCanvas;

        private Canvas _playerStatusBarCanvas;
        private Canvas _playerSpeedometerCanvas;
        private Canvas _playerDestroyPlayerCanvas;

        private PlayerStatusBarView _playerStatusBarView;
        private PlayerSpeedometerView _playerSpeedometerView;
        private DestroyPlayerMessageView _playerDestroyPlayerView;

        private readonly ResourcePath _playerStatusBarCanvasPath = new("Prefabs/Canvas/Game/StatusBarCanvas");
        private readonly ResourcePath _playerSpeedometerCanvasPath = new("Prefabs/Canvas/Game/SpeedometerCanvas");
        private readonly ResourcePath _playerDestroyPlayerCanvasPath = new("Prefabs/Canvas/Game/DestroyPlayerCanvas");

        private Action _exitToMenu;

        public GameUIController(Canvas mainCanvas, Action exitToMenu)
        {
            _mainCanvas = mainCanvas;
            _exitToMenu = exitToMenu;
            
            AddPlayerStatusBar();
            AddPlayerSpeedometer();
        }

        private void AddPlayerStatusBar()
        {
            _playerStatusBarCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerStatusBarCanvasPath, _mainCanvas.transform);
            _playerStatusBarView = _playerStatusBarCanvas.GetComponent<PlayerStatusBarView>();
            PlayerStatusBarView = _playerStatusBarView;
            AddGameObject(_playerStatusBarCanvas.gameObject);
        }

        private void AddPlayerSpeedometer()
        {
            _playerSpeedometerCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerSpeedometerCanvasPath, _mainCanvas.transform);
            _playerSpeedometerView = _playerSpeedometerCanvas.GetComponent<PlayerSpeedometerView>();
            PlayerSpeedometerView = _playerSpeedometerView;
            AddGameObject(_playerSpeedometerCanvas.gameObject);
        }

        protected override void OnDispose()
        {
            PlayerStatusBarView = null;
            PlayerSpeedometerView = null;
        }
            
        public void AddDestroyPlayerMessage()
        {
            _playerDestroyPlayerCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerDestroyPlayerCanvasPath, _mainCanvas.transform);
            _playerDestroyPlayerView = _playerDestroyPlayerCanvas.GetComponent<DestroyPlayerMessageView>();
            _playerDestroyPlayerView.DestroyPlayerButton.Init(_exitToMenu);
            DestroyPlayerViewComponent = _playerDestroyPlayerView;
            AddGameObject(_playerDestroyPlayerCanvas.gameObject);
        }
    }
}