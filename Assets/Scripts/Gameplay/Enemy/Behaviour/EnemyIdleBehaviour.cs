using Gameplay.Player;
using Scriptables.Enemy;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyIdleBehaviour : EnemyBehaviour
    {
        private readonly float _playerDetectionRadius;
        public EnemyIdleBehaviour(SubscribedProperty<EnemyState> enemyState,
            EnemyView view,
            PlayerView playerView,
            EnemyConfig config) : base(enemyState, view, playerView)
        {
            _playerDetectionRadius = config.Behaviour.PlayerDetectionRadius;
        }

        protected override void OnUpdate()
        {
            ChangerState();
        }
        
        private void ChangerState()
        {
            if (Vector3.Distance(View.transform.position, PlayerView.transform.position) < _playerDetectionRadius)
            {
                EnterCombat();
            }
        }
        private void EnterCombat()
        {
            ChangeState(EnemyState.InCombat);
        }
    }
}