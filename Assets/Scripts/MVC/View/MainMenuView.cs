using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class MainMenuView : BaseView
    {
        [SerializeField]
        Button playButton;

        public void PlayButtonOnClick()
        {
            EventBus.Instance.RiseEvent(EventType.StartNewGame, null);
        }
    }
}
