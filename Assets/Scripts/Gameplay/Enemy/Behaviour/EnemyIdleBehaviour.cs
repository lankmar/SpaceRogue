using Gameplay.Player;
using Scriptables.Enemy;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyIdleBehaviour : EnemyBehaviour
    {
        public EnemyIdleBehaviour(SubscribedProperty<EnemyState> enemyState,
            EnemyView view,
            PlayerView playerView,
            EnemyBehaviourConfig config) : base(enemyState, view, playerView, config)
        {
        }

        protected override void OnUpdate()
        {
            DetectPlayer();
        }
        
        private void DetectPlayer()
        {
            if (Vector3.Distance(View.transform.position, PlayerView.transform.position) < Config.PlayerDetectionRadius)
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