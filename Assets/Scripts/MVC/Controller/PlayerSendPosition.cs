using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace MVC
{
    public class PlayerSendPosition : MonoBehaviour
    {
        Task task;
        private bool stopSend = false;

        public EventHandler positionChanged;
        public void OnEnable()
        {
            EventBus.Instance.StartListening(EventType.CurrentPositionChanged, positionChanged);
            StartSend();
        }
        public void OnDisable()
        {
            EventBus.Instance.StopListening(EventType.CurrentPositionChanged, positionChanged);
            stopSend = true;
        }

        private async Task StartSend()
        {
            stopSend = false;
            while (!stopSend)
            {
                //EventBus.Instance.RiseEvent(EventType.CurrentPositionChanged, new CurrentPositionEventArgs(transform.position));
                GameData.Instance.CurrentPosition = transform.position;
                await Task.Delay(300);
            }
        }
    }
}

