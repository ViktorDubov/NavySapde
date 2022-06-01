using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class FinishView : BaseView
    {
        [SerializeField]
        Text maxScore;

        [SerializeField]
        Button restartButton;

        public void RestartButtonOnClick()
        {
            EventBus.Instance.RiseEvent(EventType.StartNewGame, null);
        }

        private EventHandler maxScoreChanged;
        public void OnEnable()
        {
            maxScoreChanged += OnMaxScoreChanged;
            EventBus.Instance.StartListening(EventType.MaxScoreChanged, maxScoreChanged);
        }
        private void OnDisable()
        {
            maxScoreChanged -= OnMaxScoreChanged;
            EventBus.Instance.StopListening(EventType.MaxScoreChanged, maxScoreChanged);
        }
        private void OnMaxScoreChanged(object sender, EventArgs maxScore)
        {
            float score = (maxScore as MaxScoreEventArgs).MaxScore;
            //Debug.Log($"score {score}");
            this.maxScore.text = "Score: " + score.ToString();
        }
    }
}