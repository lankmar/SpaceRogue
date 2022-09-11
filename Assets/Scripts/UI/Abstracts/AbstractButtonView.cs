using Abstracts;
using System;
using UnityEngine;

public abstract class AbstractButtonView : MonoBehaviour, IButtonView
{
    public abstract void Init(Action onClickAction);
}
