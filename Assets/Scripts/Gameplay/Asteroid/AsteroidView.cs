using Abstracts;
using Gameplay.Damage;
using System;
using UnityEngine;

namespace Gameplay.Asteroid
{
    [RequireComponent(typeof(Collider))]
    public class AsteroidView : UnitView, IDamagingView
    {
        public event Action CollisionEnter = () => { };
        public DamageModel DamageModel { get; private set; }

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }

        new public void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter();
        }
    }
}