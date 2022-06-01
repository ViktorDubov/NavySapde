using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public enum EventType
    {
        StartNewGame,
        FinishGame,
        ScoreChanged,
        MaxScoreChanged,
        LifesChanged,
        DistanceToNearEnemyChanged,
        DistanceToNearCristallChanged,
        CurrentPositionChanged,
        TargetPositionChanged,
        CountOfEnemiesChanged,
        CountOfCristallChanged,
    }
    public sealed class EventBus
    {
        private static Dictionary<EventType, EventHandler> eventDictionary;
        private static EventBus eventBus;
        public static EventBus Instance
        {
            get
            {
                if (eventBus == null)
                {
                    eventBus = new EventBus();
                    if (eventDictionary == null)
                    {
                        eventDictionary = new Dictionary<EventType, EventHandler>();
                    }
                }
                return eventBus;
            }
        }
        public void StartListening(EventType eventType, EventHandler eventHandler)
        {
            if (eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType] += eventHandler;
            }
            else
            {
                eventDictionary.Add(eventType, eventHandler);
            }
        }
        public void StopListening(EventType eventType, EventHandler eventHandler)
        {
            if (eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType] -= eventHandler;
            }

        }
        public void RiseEvent(EventType eventType, EventArgs eventArgs)
        {
            if (eventDictionary.ContainsKey(eventType))
            {
                eventDictionary[eventType]?.Invoke(this, eventArgs);
            }
        }
    }
}