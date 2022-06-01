using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    /// <summary>
    /// В дальнейшем можно расширить до нескольких вариантов префабов с выбором или случайной генерацией
    /// Или переделать в обычный класс с addressables, что не так удобно гд, но лучше по перфомансу
    /// </summary>
    public class Fabrica : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;

        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private GameObject cristallPrefab;

        [SerializeField]
        private GameObject obstaclePrefab;

        [SerializeField]
        private GameObject spawnPointPrefab;

        [SerializeField]
        private GameObject cristallSpawnPrefab;

        private static Fabrica fabrica;
        public static Fabrica Instance
        {
            get
            {
                if (!fabrica)
                {
                    fabrica = FindObjectOfType(typeof(Fabrica)) as Fabrica;

                    if (!fabrica)
                    {
                        Debug.LogError("There needs to be one active Fabrica script on a GameObject in your scene.");
                    }
                }
                return fabrica;
            }
        }

        public GameObject GetPlayer()
        {
            return playerPrefab;
        }
        public GameObject GetEnemy()
        {
            return enemyPrefab;
        }
        public GameObject GetObstacle()
        {
            return obstaclePrefab;
        }
        public GameObject GetCristall()
        {
            return cristallPrefab;
        }
        public GameObject GetSpawnPoint()
        {
            return spawnPointPrefab;
        }
        public GameObject GetCristallSpawnPoint()
        {
            return cristallSpawnPrefab;
        }
        
    }
}

