using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class ViewsController : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private GraphicRaycaster graphicRaycaster;

        [SerializeField]
        private BaseView[] views;

        private BaseView currentView;

        private static ViewsController viewsController;
        public static ViewsController Instance
        {
            get
            {
                viewsController = FindObjectOfType(typeof(ViewsController)) as ViewsController;

                if (!viewsController)
                {
                    Debug.LogError("There needs to be one active ViewsController script on a GameObject in your scene.");
                }
                return viewsController;
            }
        }

        public void Awake()
        {
            for (int i = 0; i < views.Length; i++)
            {
                if (views[i].ThisView == ViewType.InfoPanelView)
                {
                    (views[i] as InfoPanelView).Initiate(canvas, graphicRaycaster);
                }
                if (views[i].ThisView == ViewType.MainMenuView)
                {
                    views[i].gameObject.SetActive(true);
                    currentView = views[i];
                }
                if (views[i].ThisView == ViewType.FinishView)
                {
                    (views[i] as FinishView).OnEnable();
                }
            }
        }

        public void ShowView(ViewType viewType)
        {
            for (int i = 0; i < views.Length; i++)
            {
                if (views[i].ThisView == viewType)
                {
                    if (currentView.ThisView != viewType)
                    {
                        currentView.Hide();
                        views[i].Show();
                        currentView = views[i];
                    }
                }
            }
        }
        public void HideCurrentView()
        {
            currentView.Hide();
        }


        private EventHandler gameStarted;
        private EventHandler gameFinished;

        public void OnEnable()
        {
            gameStarted += OnGameStarted;
            EventBus.Instance.StartListening(EventType.StartNewGame, gameStarted);
            gameFinished += OnGameFinished;
            EventBus.Instance.StartListening(EventType.FinishGame, gameFinished);
        }
        public void OnDisable()
        {
            gameStarted -= OnGameStarted;
            EventBus.Instance.StopListening(EventType.StartNewGame, gameStarted);
            gameFinished -= OnGameFinished;
            EventBus.Instance.StopListening(EventType.FinishGame, gameFinished);
        }
        private void OnGameStarted(object sender, EventArgs eventArgs)
        {
            ShowView(ViewType.InfoPanelView);
        }
        private void OnGameFinished(object sender, EventArgs eventArgs)
        {
            ShowView(ViewType.FinishView);
        }
    }
}

