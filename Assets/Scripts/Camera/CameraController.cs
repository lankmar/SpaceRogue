using Abstracts;
using Gameplay.Player;
using UnityEngine;

public class CameraController : BaseController
{
    private CameraView _cameraView = Camera.main.GetComponent<CameraView>();
    private readonly PlayerView _playerView = GameObject.FindObjectOfType<PlayerView>();

    public CameraController()
    {
        EntryPoint.SubscribeToUpdate(MovementCamera);
    }

    public void MovementCamera()
    {
        _cameraView.gameObject.transform.position = new Vector3(_playerView.gameObject.transform.position.x, _playerView.gameObject.transform.position.y, _playerView.transform.position.z - 10);
    }
}
