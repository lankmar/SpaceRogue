using Abstracts;
using Gameplay.Player;
using UnityEngine;
using Utilities.Extensions;

namespace Gameplay.Camera
{
    public class CameraController : BaseController
    {
        private readonly CameraView _cameraView;
        private readonly PlayerController _playerController;
        private const int CameraZAxisOffset = -10;
        private Vector3 playerPosition;

        public CameraController(PlayerController playerController)
        {
            _cameraView = UnityEngine.Camera.main!.GetComponent<CameraView>();
            _playerController = playerController;
            EntryPoint.SubscribeToUpdate(FollowPlayer);
            _playerController.PlayerDestroyed += OnPlayerDestroyed;
        }
        
        public void OnPlayerDestroyed()
        {
            EntryPoint.UnsubscribeFromUpdate(FollowPlayer);
        }

        private void FollowPlayer()
        {
            playerPosition = _playerController.View.gameObject.transform.position;
            _cameraView.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + CameraZAxisOffset);
        }
    }
}