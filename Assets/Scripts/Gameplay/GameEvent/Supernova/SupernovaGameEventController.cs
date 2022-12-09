using Gameplay.Player;
using Gameplay.Space.Star;
using Scriptables.GameEvent;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GameEvent
{
    public sealed class SupernovaGameEventController : GameEventController
    {
        private readonly SupernovaGameEventConfig _supernovaGameEventConfig;
        private readonly PlayerView _playerView;

        public SupernovaGameEventController(GameEventConfig config, PlayerView playerView) : base(config)
        {
            var supernovaGameEventConfig = config as SupernovaGameEventConfig;
            _supernovaGameEventConfig = supernovaGameEventConfig
                ? supernovaGameEventConfig
                : throw new System.Exception("Wrong config type was provided");

            _playerView = playerView;
        }

        protected override void RunGameEvent()
        {
            if(_playerView == null)
            {
                return;
            }

            if(!TryGetNearestStarView(_playerView.transform.position, _supernovaGameEventConfig.SearchRadius, out var starView))
            {
                return;
            }

            var supernovaController = new SupernovaController(_supernovaGameEventConfig, starView);
            AddController(supernovaController);
        }

        private bool TryGetNearestStarView(Vector3 position, float radius, out StarView starView)
        {
            starView = null;
            var colliders = Physics2D.OverlapCircleAll(position, radius);

            var views = new List<StarView>();
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out StarView view))
                {
                    if (!view.InGameEvent)
                    {
                        views.Add(view); 
                    }
                }
            }

            if (views.Count == 0)
            {
                return false;
            }

            starView = GetClosestStarView(views, position);
            return true;

        }

        private StarView GetClosestStarView(List<StarView> starViews, Vector3 currentPosition)
        {
            var view = default(StarView);
            var closestDistanceSqr = Mathf.Infinity;

            for (int i = 0; i < starViews.Count; i++)
            {
                var direction = starViews[i].transform.position - currentPosition;
                var sqrMagnitude = direction.sqrMagnitude;

                if (sqrMagnitude < closestDistanceSqr)
                {
                    closestDistanceSqr = sqrMagnitude;
                    view = starViews[i];
                }
            }
            view.InGameEvent = true;
            return view;
        }
    }
}