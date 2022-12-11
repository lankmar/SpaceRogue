using Gameplay.Player;
using Scriptables.GameEvent;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.Unity;

namespace Gameplay.GameEvent
{
    public sealed class CaravanGameEventController : GameEventController
    {
        private const int MaxCountOfCaravanSpawnTries = 10;

        private readonly CaravanGameEventConfig _caravanGameEventConfig;
        private readonly PlayerView _playerView;
        private readonly float _orthographicSize;
        private readonly float _caravanSize;

        private CaravanController _caravanController;
        private bool _isStopped;

        public CaravanGameEventController(GameEventConfig config, PlayerController playerController) : base(config, playerController)
        {
            var caravanGameEventConfig = config as CaravanGameEventConfig;
            _caravanGameEventConfig = caravanGameEventConfig
                ? caravanGameEventConfig
                : throw new System.Exception("Wrong config type was provided");

            _playerView = _playerController.View;
            _orthographicSize = UnityEngine.Camera.main.orthographicSize;
            _caravanSize = _caravanGameEventConfig.CaravanView.transform.localScale.MaxVector3CoordinateOnPlane();
        }

        protected override bool RunGameEvent()
        {
            if (_isStopped)
            {
                return true;
            }

            if (!TryGetNewCaravanPositionAndTargetPosition(out var position, out var targetPosition))
            {
                Debug("No place for Caravan");
                return false;
            }

            _caravanController =  new(_caravanGameEventConfig, _playerController, CreateCaravanView(position), targetPosition);
            _caravanController.OnDestroy.Subscribe(DestroyController);
            AddController(_caravanController);
            return true;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _caravanController?.OnDestroy.Unsubscribe(DestroyController);
        }

        protected override void OnPlayerDestroyed()
        {
            _isStopped = true;
        }

        private void DestroyController(bool onDestroy)
        {
            if (onDestroy)
            {
                Dispose();
            }
        }

        private bool TryGetNewCaravanPositionAndTargetPosition(out Vector3 position, out Vector3 targetPosition)
        {
            var tryCount = 0;
            var radius = _caravanSize + _caravanGameEventConfig.EnemyCount * 2;
            do
            {
                position = GetRandomCaravanPosition();
                targetPosition = GetRandomCaravanTargetPosition(position);
                tryCount++;
            }
            while ((UnityHelper.IsAnyObjectAtPosition(position, radius)
                   || UnityHelper.IsAnyObjectAtPosition(targetPosition, radius))
                   && tryCount <= MaxCountOfCaravanSpawnTries);

            if (tryCount > MaxCountOfCaravanSpawnTries)
            {
                return false;
            }

            return true;
        }

        private Vector3 GetRandomCaravanPosition()
        {
            var angleDirection = RandomPicker.PickRandomAngle(360, _random).normalized;
            var playerPosition = _playerView.transform.position;
            var offset = _orthographicSize * 2 + _caravanSize + _caravanGameEventConfig.SpawnOffset;
            var position = playerPosition + angleDirection * offset;
            return position;
        }

        private Vector3 GetRandomCaravanTargetPosition(Vector3 caravanPosition)
        {
            var angleDirection = RandomPicker.PickRandomAngle(360, _random).normalized;
            var offset = _caravanSize + _caravanGameEventConfig.PathDistance;
            var position = caravanPosition + angleDirection * offset;
            return position;
        }

        private CaravanView CreateCaravanView(Vector3 position)
        {
            var caravanView = Object.Instantiate(_caravanGameEventConfig.CaravanView, position, Quaternion.identity);
            return caravanView;
        }
    }
}