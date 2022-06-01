using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class CalculateDistance : MonoBehaviour
    {
        private EventHandler currentPositionChanged;

        public void OnEnable()
        {
            currentPositionChanged += OnCurrentPositionChanged;
            EventBus.Instance.StartListening(EventType.CurrentPositionChanged, currentPositionChanged);
        }

        public void OnDisable()
        {
            currentPositionChanged -= OnCurrentPositionChanged;
            EventBus.Instance.StopListening(EventType.CurrentPositionChanged, currentPositionChanged);
        }

        private void OnCurrentPositionChanged(object sender, EventArgs eventArgs)
        {
            Vector3 position = (eventArgs as CurrentPositionEventArgs).CurrentPosition;
            CalculateDistanceToNearCristall(position);
            CalculateDistanceToNearEnemy(position);
        }

        private void CalculateDistanceToNearCristall(Vector3 position)
        {
            GameObjectPool cristallPool = GameData.Instance.CristallPool;
            float distance = 100000;
            for (int i = 0; i < cristallPool.Size; i++)
            {
                GameObject go = cristallPool.GetElement(i);
                if (go.activeSelf)
                {
                    float currentDistance = Vector3.Distance(position, go.transform.position);
                    if (currentDistance < distance)
                    {
                        distance = currentDistance;
                    }
                }
            }
            EventBus.Instance.RiseEvent(EventType.DistanceToNearCristallChanged, new DistanceToNearCristallEventArgs(distance));
        }
        private void CalculateDistanceToNearEnemy(Vector3 position)
        {
            GameObjectPool enemyPool = GameData.Instance.EnemyPool;
            float distance = 100000;
            for (int i = 0; i < enemyPool.Size; i++)
            {
                GameObject go = enemyPool.GetElement(i);
                if (go.activeSelf)
                {
                    float currentDistance = Vector3.Distance(position, go.transform.position);
                    if (currentDistance < distance)
                    {
                        distance = currentDistance;
                    }
                }
            }
            EventBus.Instance.RiseEvent(EventType.DistanceToNearEnemyChanged, new DistanceToNearEnemyEventArgs(distance));
        }
    }
}

