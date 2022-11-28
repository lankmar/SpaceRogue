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

        public void OnTriggerEnter2D(Collider2D other)
        {
            CollisionEnter();
        }
    }
}