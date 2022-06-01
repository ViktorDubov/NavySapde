using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class CheckMaxScore : MonoBehaviour
    {
        private EventHandler gameFinished;
        public void OnEnable()
        {
            gameFinished += OnGameFinished;
            EventBus.Instance.StartListening(EventType.FinishGame, gameFinished);
        }
        public void OnDisable()
        {
            gameFinished -= OnGameFinished;
            EventBus.Instance.StopListening(EventType.FinishGame, gameFinished);
        }
        private void OnGameFinished(object sender, EventArgs eventArgs)
        {
            int currentScore = GameData.Instance.CurrentScore;
            //Debug.Log($"currentScore {currentScore}");
            int maxScore = GameData.Instance.SavedGameData.Record;
            //Debug.Log($"maxScore {maxScore}");
            if (currentScore > maxScore)
            {
                maxScore = currentScore;
                GameData.Instance.SavedGameData.Record = maxScore;
                GameData.Save();
            }
            EventBus.Instance.RiseEvent(EventType.MaxScoreChanged, new MaxScoreEventArgs(maxScore));
        }
    }
}

