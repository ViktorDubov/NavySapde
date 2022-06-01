using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    
    /// <summary>
    /// Класс для удобства настройки игры ГД. Никакой логики, кроме предоставления данных от гд тут быть не должно.
    /// </summary>
    public class GameSetting : MonoBehaviour
    {
        private static GameSetting gameSetting;
        public static GameSetting Instance
        {
            get
            {
                if (!gameSetting)
                {
                    gameSetting = FindObjectOfType(typeof(GameSetting)) as GameSetting;

                    if (!gameSetting)
                    {
                        Debug.LogError("There needs to be one active GameSetting script on a GameObject in your scene.");
                    }
                }
                return gameSetting;
            }
        }
        [SerializeField]
        private int maxNumberOfPlayerLife = 3;
        public int MaxNumberOfLife
        {
            get
            {
                return maxNumberOfPlayerLife;
            }
        }
        [SerializeField]
        private int playerSpeed = 5;
        public int PlayerSpeed
        {
            get
            {
                return playerSpeed;
            }
        }
        [SerializeField]
        private int maxCountOfEnemy = 10;
        public int MaxCountOfEnemy
        {
            get
            {
                return maxCountOfEnemy;
            }
        }
        [SerializeField]
        private int enemySpeed = 4;
        public int EnemySpeed
        {
            get
            {
                return enemySpeed;
            }
        }
        [SerializeField]
        private int maxCountOfCristall = 3;
        public int MaxCountOfCristall
        {
            get
            {
                return maxCountOfCristall;
            }
        }
        [SerializeField]
        private int fieldSize = 20;
        public int FieldSize
        {
            get
            {
                return fieldSize;
            }
        }
        [SerializeField]
        private int countOfSpawnPoint = 2;
        public int CountOfSpawnPoint
        {
            get
            {
                return countOfSpawnPoint;
            }
        }
        [SerializeField]
        private int countOfObstacles = 10;
        public int CountOfObstacles
        {
            get
            {
                return countOfObstacles;
            }
        }
    }
}
