using Abstracts;
using System;
using UnityEngine;

namespace UI.Abstracts
{
    public abstract class DestroyPlayerView : MonoBehaviour, IDestroyPlayerView
    {
        public abstract void Init(Action actionDestroyPlayer);
    }
}