using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    /// <summary>
    /// Класс для хранения данных игры и вызова ивентов, связанныз с их изменением.
    /// </summary>
    public sealed class GameData
    {
        private GameData() 
        {

        }
        private static GameData instance = null;
        public static GameData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameData();
                    Load();
                }
                return instance;
            }
        }

        private SavedGameData savedGameData;
        public SavedGameData SavedGameData
        {
            get
            {
                if (savedGameData == null)
                {
                    savedGameData = SavedGameData.Load();
                }
                return savedGameData;
            }
            set
            {
                savedGameData = value;
                SavedGameData.Save(savedGameData);
            }
        }
        public static void Load()
        {
            Instance.savedGameData = SavedGameData.Load();
        }
        public static void Save()
        {
            SavedGameData.Save(Instance.savedGameData);
        }
        #region Event Properties
        private int currentScore = 0;
        public int CurrentScore
        {
            get
            {
                return currentScore;
            }
            set
            {
                currentScore = value;
                //Debug.Log($"currentScore {currentScore}");
                EventBus.Instance.RiseEvent(EventType.ScoreChanged, new CurrentScoreEventArgs(currentScore));
            }
        }
        private int maxScore;
        public int MaxScore
        {
            get
            {
                return maxScore;
            }
            set
            {
                maxScore = value;
            }
        }
        private int currentLife = GameSetting.Instance.MaxNumberOfLife;
        public int CurrentLife
        {
            get
            {
                return currentLife;
            }
            set
            {
                if ((value) <= MaxNumberOfLife && value > 0)
                {
                    currentLife = value;
                    EventBus.Instance.RiseEvent(EventType.LifesChanged, new CurrentLifeEventArgs(currentLife));
                }
                else if (value <= 0)
                {
                    EventBus.Instance.RiseEvent(EventType.FinishGame, null);
                }
                //Debug.Log($"currentLife {currentLife}");
            }
        }
        private Vector3 currentPosition;
        public Vector3 CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                currentPosition = value;
                //Debug.Log($"currentPosition {currentPosition}");
                EventBus.Instance.RiseEvent(EventType.CurrentPositionChanged, new CurrentPositionEventArgs(currentPosition));
            }
        }
        private Vector2 targetPosition;
        public Vector2 Targetosition
        {
            get
            {
                return targetPosition;
            }
            set
            {
                targetPosition = value;
                //Debug.Log($"targetPosition {targetPosition}");
                EventBus.Instance.RiseEvent(EventType.TargetPositionChanged, new CurrentTargetPositionEventArgs(targetPosition));
            }
        }
        private int countOfEnemies = 0;
        public int CountOfEnemies
        {
            get
            {
                return countOfEnemies;
            }
            set
            {
                countOfEnemies = value;
                //Debug.Log($"countOfEnemies {countOfEnemies}");
                EventBus.Instance.RiseEvent(EventType.CountOfEnemiesChanged, new CurrentCountOfEnemyEventArgs(countOfEnemies));
            }
        }
        private int countOfCristall = 0;
        public int CountOfCrystall
        {
            get
            {
                return countOfCristall;
            }
            set
            {
                countOfCristall = value;
                //Debug.Log($"countOfCristall {countOfCristall}");
                EventBus.Instance.RiseEvent(EventType.CountOfCristallChanged, new CurrentCountOfCristallEventArgs(countOfCristall));
            }
        }
        #endregion

        #region Variable Properties
        GameObjectPool enemyPool;
        public GameObjectPool EnemyPool
        {
            get
            {
                return enemyPool;
            }
            set
            {
                enemyPool = value;
            }
        }
        GameObjectPool cristallPool;
        public GameObjectPool CristallPool
        {
            get
            {
                return cristallPool;
            }
            set
            {
                cristallPool = value;
            }
        }
        #endregion

        #region Constant Properties
        private int maxNumberOfPlayerLife = GameSetting.Instance.MaxNumberOfLife;
        public int MaxNumberOfLife
        {
            get
            {
                return maxNumberOfPlayerLife;
            }
        }
        private int playerSpeed = GameSetting.Instance.PlayerSpeed;
        public int PlayerSpeed
        {
            get
            {
                return playerSpeed;
            }
        }
        private int maxCountOfEnemy = GameSetting.Instance.MaxCountOfEnemy;
        public int MaxCountOfEnemy
        {
            get
            {
                return maxCountOfEnemy;
            }
        }
        private int enemySpeed = GameSetting.Instance.EnemySpeed;
        public int EnemySpeed
        {
            get
            {
                return enemySpeed;
            }
        }
        private int maxCountOfCristall = GameSetting.Instance.MaxCountOfCristall;
        public int MaxCountOfCristall
        {
            get
            {
                return maxCountOfCristall;
            }
        }
        private int fieldSize = GameSetting.Instance.FieldSize;
        public int FieldSize
        {
            get
            {
                return fieldSize;
            }
        }
        private int countOfSpawnPoint = GameSetting.Instance.CountOfSpawnPoint;
        public int CountOfSpawnPoint
        {
            get
            {
                return countOfSpawnPoint;
            }
        }
        private int countOfObstacles = GameSetting.Instance.CountOfObstacles;
        public int CountOfObstacles
        {
            get
            {
                return countOfObstacles;
            }
        }
        #endregion
    }
}