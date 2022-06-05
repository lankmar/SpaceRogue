using Gameplay.Player.Movement;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerMovementConfig movement;
    }
}