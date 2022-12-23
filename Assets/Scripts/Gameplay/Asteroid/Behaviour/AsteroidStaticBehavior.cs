using Gameplay.Player;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Asteroid.Behaviour
{
    public class AsteroidStaticBehavior : AsteroidBehaviour
    {
        Transform _asteroidTransform;
        Vector3 _vector;

        private float speedRotation = 0.1f;

        public AsteroidStaticBehavior(AsteroidView view,
                PlayerView playerView,
                AsteroidBehaviourConfig config) : base(view, playerView, config)
        {
            _asteroidTransform = view.GetComponent<Transform>();
            Random random = new Random();
            speedRotation = random.Next(1,10);
            speedRotation = speedRotation/10;
            _vector = (random.Next(2) == 1) ? new Vector3(0, 0, -speedRotation) : new Vector3(0, 0, speedRotation);
        }
        
        protected override void OnUpdate()
        {
            AsteroidRotation();
        }

        private void AsteroidRotation()
        {
            _asteroidTransform.Rotate(_vector);
        }   
    }
}