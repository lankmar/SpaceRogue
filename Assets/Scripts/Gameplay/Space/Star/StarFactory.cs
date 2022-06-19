using System;
using System.Linq;
using Scriptables.Space;
using UnityEngine;
using Utilities.Mathematics;

namespace Gameplay.Space.Star
{
    public class StarFactory
    {
        private readonly StarSpawnConfig _config;
        private readonly System.Random _random;
        public StarFactory(StarSpawnConfig config)
        {
            _config = config;
            _random = new System.Random();
        }
        
        public StarController CreateStar(Vector3 spawnPosition)
        {
            var config = PickAStar();
            return new StarController(CreateStarView(config.Prefab, PickSize(config.MinSize, config.MaxSize), spawnPosition));
        }

        private float PickSize(float minSize, float maxSize)
        {
            float sizeDifference = maxSize - minSize;
            return (float)Math.Round(_random.NextDouble() * sizeDifference + minSize, 3);
        }
        
        private static StarView CreateStarView(StarView prefab, float size, Vector3 spawnPosition)
        {
            var viewGo = UnityEngine.Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            viewGo.transform.localScale = new Vector3(size, size);
            return viewGo;
        }

        private StarConfig PickAStar()
        {
            return RandomPicker.PickOneElementByWeights(_config.WeightConfigs, _random);
        }
    }
}