using Gameplay.Damage;
using Gameplay.Health;
using Gameplay.Space.Star;
using System;
using UnityEngine;

namespace Gameplay.Space.Planet
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlanetView : MonoBehaviour, IDamagingView
    {
        public event Action CollisionEnter = () => { };

        public DamageModel DamageModel { get; private set; }

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out StarView starView) || collision.gameObject.TryGetComponent(out PlanetView planetView))
            {
                CollisionEnter();
            }
        }
    }
}