using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace MVC
{
    public class PostCurrentPosition : MonoBehaviour
    {
        private bool isCancel;
        private void OnEnable()
        {
            isCancel = false;
            PostPosition();
        }
        private void OnDisable()
        {
            isCancel = false;
        }
        private async Task PostPosition()
        {
            while (!isCancel)
            {
                GameData.Instance.CurrentPosition = transform.position;
                //Debug.Log(transform.position);
                await Task.Delay(300);
            }
        }
    }
}