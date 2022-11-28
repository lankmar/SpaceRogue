using System.Collections.Generic;
using Abstracts;
using Gameplay.Player;
using Scriptables.Asteroid;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.ResourceManagement;
using Utilities.Unity;

namespace Gameplay.Asteroid
{
    public class AsteroidsController : BaseController
    {
        private readonly ResourcePath _groupSpawnConfigPath = new(Constants.Configs.Asteroid.AsteroidSpawnConfig);
        private readonly AsteroidFactory _asteroidFactory;
        private SingleAsteroidSpawnController _singleAsteroidSpawnController;

        public AsteroidsController(PlayerController playerController)
        {
                var cloudSpawnConfig = ResourceLoader.LoadObject<AsteroidSpawnConfig>(_groupSpawnConfigPath);

                _asteroidFactory = new AsteroidFactory(cloudSpawnConfig.AsteroidClouds);

                var unitSize = cloudSpawnConfig.AsteroidClouds.Prefab.transform.localScale;

            foreach (var AsteroidGroupSpawn in cloudSpawnConfig.AsteroidCloudsSpawnPoints)
            {
                int spawnCircleRadius = AsteroidGroupSpawn.GroupCount * 2;
                for (int i = 0; i < AsteroidGroupSpawn.GroupCount; i++)
                {
                    var unitSpawnPoint = GetEmptySpawnPoint(AsteroidGroupSpawn.GroupSpawnPoint, unitSize, spawnCircleRadius);
                    var asteroidController = _asteroidFactory.CreateAsteroid(unitSpawnPoint, playerController.View);
                    AddController(asteroidController);
                }
            }

            _singleAsteroidSpawnController = new SingleAsteroidSpawnController(playerController.View, cloudSpawnConfig);
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
