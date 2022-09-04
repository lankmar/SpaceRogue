using Abstracts;
using System;
using UnityEngine;

public abstract class ButtonView : MonoBehaviour, IButtonView
{
    public abstract void Init(Action onClickAction);
}
