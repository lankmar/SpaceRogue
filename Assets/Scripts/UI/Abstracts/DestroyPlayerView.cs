using Gameplay;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Abstracts
{
    [RequireComponent(typeof(Button))]
    public class DestroyPlayerView : ButtonView
    {
        private Button _button;
        public Action ClickAction;

        public DestroyPlayerView() 
        {
            _button = GetComponent<Button>();
        }

        public override void Init(Action onClickAction)
        {
            _button.onClick.AddListener(new UnityAction(ClickAction));
        }
    }
}