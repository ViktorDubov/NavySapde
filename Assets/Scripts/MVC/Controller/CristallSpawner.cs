using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace MVC
{
    public class CristallSpawner : MonoBehaviour
    {
        Task randomTask;
        private bool stopRandom = false;

        public void OnEnable()
        {
            StartRandom();
        }
        public void OnDisable()
        {
            stopRandom = true;
        }

        private async Task StartRandom()
        {
            stopRandom = false;
            while (!stopRandom)
            {
                //await Task.Delay(1000);
                GameObject cristall = GameData.Instance.CristallPool.GetNext();
                if (cristall != null)
                {
                    Vector2Int temp;
                    GOType goType;
                    do
                    {
                        temp = new Vector2Int(UnityEngine.Random.Range(1, GameData.Instance.FieldSize - 2), UnityEngine.Random.Range(1, GameData.Instance.FieldSize - 2));
                        goType = Grid.GetGOTypeByCell(temp.x, temp.y);
                    } while (goType != GOType.None);
                    Grid.SetGOTypeBycell(GOType.Cristall, temp.x, temp.y);
                    cristall
                        .transform
                        .position = new Vector3(temp.x, 1, temp.y);
                    cristall.SetActive(true);

                }
                await Task.Delay(UnityEngine.Random.Range(1000, 5000));
            }
        }
    }
}