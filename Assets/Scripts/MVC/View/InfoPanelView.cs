using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class InfoPanelView : BaseView
    {
        [SerializeField]
        private Text score;

        [SerializeField]
        private Text life;

        [SerializeField]
        private Text nearEnemyDistance;

        [SerializeField]
        private Text nearCristallDistance;

        private Canvas canvas;
        private GraphicRaycaster graphicRaycaster;

        private EventHandler scoreChanged;
        private EventHandler lifeChanged;
        private EventHandler nearEnemyDistanceChanged;
        private EventHandler nearCristallDistanceChanged;


        public void Initiate(Canvas canvas, GraphicRaycaster graphicRaycaster)
        {
            this.canvas = canvas;
            this.graphicRaycaster = graphicRaycaster;
        }

        public void OnEnable()
        {
            graphicRaycaster.enabled = false;

            scoreChanged += OnScoreChanged;
            lifeChanged += OnLifesChanged;
            nearEnemyDistanceChanged += OnDistanceToNearEnemyChanged;
            nearCristallDistanceChanged += OnDistanceToNearCristallChanged;

            EventBus.Instance.StartListening(EventType.ScoreChanged, scoreChanged);
            EventBus.Instance.StartListening(EventType.LifesChanged, lifeChanged);
            EventBus.Instance.StartListening(EventType.DistanceToNearEnemyChanged, nearEnemyDistanceChanged);
            EventBus.Instance.StartListening(EventType.DistanceToNearCristallChanged, nearCristallDistanceChanged);
        }
        public void OnDisable()
        {
            graphicRaycaster.enabled = true;

            scoreChanged -= OnScoreChanged;
            lifeChanged -= OnLifesChanged;
            nearEnemyDistanceChanged -= OnDistanceToNearEnemyChanged;
            nearCristallDistanceChanged -= OnDistanceToNearCristallChanged;

            EventBus.Instance.StopListening(EventType.ScoreChanged, scoreChanged);
            EventBus.Instance.StopListening(EventType.LifesChanged, lifeChanged);
            EventBus.Instance.StopListening(EventType.DistanceToNearEnemyChanged, nearEnemyDistanceChanged);
            EventBus.Instance.StopListening(EventType.DistanceToNearCristallChanged, nearCristallDistanceChanged);
        }
        private void OnScoreChanged(object sender,  EventArgs currentScore)
        {
            float score = (currentScore as CurrentScoreEventArgs).CurrentScore;
            this.score.text = "Score: " + score.ToString();
        }
        private void OnLifesChanged(object sender, EventArgs currentLife)
        {
            int life = (currentLife as CurrentLifeEventArgs).Life;
            this.life.text = "Life: " + life.ToString();
        }
        private void OnDistanceToNearEnemyChanged(object sender, EventArgs currentLife)
        {
            float distance = (currentLife as DistanceToNearEnemyEventArgs).DistanceToNearEnemy;
            distance = (float)System.Math.Round(distance, 2);
            this.nearEnemyDistance.text = "Near Enemy: " + distance.ToString() + "m";
        }
        private void OnDistanceToNearCristallChanged(object sender, EventArgs currentLife)
        {
            float distance = (currentLife as DistanceToNearCristallEventArgs).DistanceToNearCristall;
            distance = (float)System.Math.Round(distance, 2);
            this.nearCristallDistance.text = "Near Cristall: " + distance.ToString() + "m";
        }
    }
}