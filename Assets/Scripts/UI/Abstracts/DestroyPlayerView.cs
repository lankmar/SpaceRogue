using Abstracts;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Abstracts
{
    [RequireComponent(typeof(Button))]
    public class DestroyPlayerView : MonoBehaviour, IButtonView
    {
        public void Init(Action actionDestroyPlayer)
        { 

        }
    }
}