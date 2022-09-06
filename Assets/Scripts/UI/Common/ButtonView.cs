using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Common
{
    [RequireComponent(typeof(Button))]
    public class ButtonView : AbstractButtonView
    {
        private Button _button;

        public override void Init(Action onClickAction)
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(new UnityAction(onClickAction));
        }
    }
}