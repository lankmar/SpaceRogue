using Gameplay.Space.Star;
using System;
using UnityEngine;

namespace Gameplay.Space.Planet
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlanetView : MonoBehaviour
    {
        public event Action CollisionEnter = () => { };

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out StarView starView) || collision.gameObject.TryGetComponent(out PlanetView planetView))
            {
                CollisionEnter();
            }
        }
    }
}