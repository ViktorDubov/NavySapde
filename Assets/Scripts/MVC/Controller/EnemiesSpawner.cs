using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace MVC
{
    public class EnemiesSpawner : MonoBehaviour
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
                GameObject enemy = GameData.Instance.EnemyPool.GetNext();
                if (enemy != null)
                {
                    Vector2Int temp;
                    GOType goType;
                    do
                    {
                        int xxx = Mathf.RoundToInt(transform.position.x);
                        int yyy = Mathf.RoundToInt(transform.position.z);
                        temp = new Vector2Int(UnityEngine.Random.Range(xxx - 2, xxx + 2), UnityEngine.Random.Range(yyy - 2, yyy + 2));
                        goType = Grid.GetGOTypeByCell(temp.x, temp.y);
                        await Task.Delay(100);
                        //Debug.Log(goType);
                    } while (goType != GOType.None);
                    Grid.SetGOTypeBycell(GOType.Enemies, temp.x, temp.y);
                    enemy
                        .transform
                        .position = new Vector3(temp.x, 1, temp.y);
                    enemy.SetActive(true);
                }
                await Task.Delay(UnityEngine.Random.Range(1000, 10000));
            }
        }
    }
}

