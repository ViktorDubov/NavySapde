using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public enum ViewType
    {
        MainMenuView,
        FinishView,
        InfoPanelView
    }
    public class BaseView : MonoBehaviour
    {
        [SerializeField]
        private ViewType view;
        public ViewType ThisView
        {
            get
            {
                return view;
            }
        }
        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}

