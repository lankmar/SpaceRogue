using Abstracts;
using Gameplay.Player;
using Scriptables.Enemy;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.ResourceManagement;
using Utilities.Unity;

namespace Gameplay.Enemy
{
    public class EnemyForcesController : BaseController
    {
        private readonly ResourcePath _groupSpawnConfigPath = new(Constants.Configs.Enemy.EnemySpawnConfig);
        private readonly EnemyFactory _enemyFactory;
        private readonly PlayerController _playerController;

        public EnemyForcesController(PlayerController playerController)
        {
            _playerController = playerController;
            var groupSpawnConfig = ResourceLoader.LoadObject<EnemySpawnConfig>(_groupSpawnConfigPath);

            _enemyFactory = new EnemyFactory(groupSpawnConfig.Enemy);

            var unitSize = groupSpawnConfig.Enemy.Prefab.transform.localScale;

            foreach (var enemyGroupSpawn in groupSpawnConfig.EnemyGroupsSpawnPoints)
            {
                int spawnCircleRadius = enemyGroupSpawn.GroupCount * 2;
                for (int i = 0; i < enemyGroupSpawn.GroupCount; i++)
                {
                    var unitSpawnPoint = GetEmptySpawnPoint(enemyGroupSpawn.GroupSpawnPoint, unitSize, spawnCircleRadius);
                    var enemyController = _enemyFactory.CreateEnemy(unitSpawnPoint, _playerController);
                    AddController(enemyController);
                }
            }
        }

        private static Vector3 GetEmptySpawnPoint(Vector3 spawnPoint, Vector3 unitSize, int spawnCircleRadius)
        {
            var unitSpawnPoint = spawnPoint + (Vector3)(Random.insideUnitCircle * spawnCircleRadius);
            float unitMaxSize = unitSize.MaxVector3CoordinateOnPlane();
            
            while (UnityHelper.IsAnyObjectAtPosition(unitSpawnPoint, unitMaxSize))
            {
                unitSpawnPoint = spawnPoint + (Vector3)(Random.insideUnitCircle * spawnCircleRadius);
            }

            return unitSpawnPoint;
        }
    }
}