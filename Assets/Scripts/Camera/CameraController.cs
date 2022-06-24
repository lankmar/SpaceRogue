using Abstracts;
using Gameplay.Player;
using UnityEngine;

namespace CameraInGame
{
    public class CameraController : BaseController
    {
        private CameraView _cameraView = Camera.main.GetComponent<CameraView>();
        private readonly PlayerView _playerView;
        private const int CameraZAxisOffset = -10;

        public CameraController(PlayerView playerView)
        {
            _playerView = playerView;
            EntryPoint.SubscribeToUpdate(MovementedCamera);
        }

        private void MovementedCamera()
        {
            Vector3 playerPosistion = _playerView.gameObject.transform.position;
            playerPosistion.z = CameraZAxisOffset;
            _cameraView.gameObject.transform.position = playerPosistion;
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(MovementedCamera);
        }
    }
}