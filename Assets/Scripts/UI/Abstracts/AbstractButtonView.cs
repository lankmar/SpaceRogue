using Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractButtonView : MonoBehaviour, IButtonView
{
    public abstract void Init(Action onClickAction);
}
