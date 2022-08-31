using UnityEngine;
using Abstracts;
using System;

public abstract class AbstractDestroyPlayerView : MonoBehaviour, IButtonView
{
    public abstract void Init(Action actionDestroyPlayer);
}
