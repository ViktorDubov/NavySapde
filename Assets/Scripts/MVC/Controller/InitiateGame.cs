using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class InitiateGame : MonoBehaviour
    {
        private Grid grid;
        private EventBus eventBus;
        private GameObjectPool enemyPool;
        private GameObjectPool cristallPool;
        private GameObjectPool obstaclesPool;
        private GameObjectPool spawnEnemyPool;

        private GameObject cristallSpawn;
        private GameObject player;

        public void Awake()
        {
            eventBus = EventBus.Instance;

            grid = Grid.Instance;

            SavedGameData savedGameData = GameData.Instance.SavedGameData;

            enemyPool = new GameObjectPool(Fabrica.Instance.GetEnemy(), GameData.Instance.MaxCountOfEnemy);
            GameData.Instance.EnemyPool = enemyPool;
            cristallPool = new GameObjectPool(Fabrica.Instance.GetCristall(), GameData.Instance.MaxCountOfCristall);
            GameData.Instance.CristallPool = cristallPool;

            obstaclesPool = new GameObjectPool(Fabrica.Instance.GetObstacle(), GameData.Instance.CountOfObstacles);
            spawnEnemyPool = new GameObjectPool(Fabrica.Instance.GetSpawnPoint(), GameData.Instance.CountOfSpawnPoint);


            cristallSpawn = Instantiate(Fabrica.Instance.GetCristallSpawnPoint(), Vector3.zero, Quaternion.Euler(Vector3.zero));
        }

        private EventHandler gameStarted;
        private EventHandler gameFinished;

        public void OnEnable()
        {
            gameStarted += OnGameStarted;
            EventBus.Instance.StartListening(EventType.StartNewGame, gameStarted);
            gameFinished += OnGameFinished;
            EventBus.Instance.StartListening(EventType.FinishGame, gameFinished);
        }
        public void OnDisable()
        {
            gameStarted -= OnGameStarted;
            EventBus.Instance.StopListening(EventType.StartNewGame, gameStarted);
            gameFinished -= OnGameFinished;
            EventBus.Instance.StopListening(EventType.FinishGame, gameFinished);
        }

        private void OnGameStarted(object sender, EventArgs eventArgs)
        {
            Grid.GenerateNewGrid();
            SetGOOnGrid();

            GameData.Instance.CurrentLife = GameData.Instance.MaxNumberOfLife;
            GameData.Instance.CurrentScore = 0;

            cristallSpawn.SetActive(true);
            player.SetActive(true);
        }
        private void OnGameFinished(object sender, EventArgs eventArgs)
        {
            //Debug.Log("Game Finished");
            enemyPool.DisableAll();
            cristallPool.DisableAll();
            obstaclesPool.DisableAll();
            spawnEnemyPool.DisableAll();

            cristallSpawn.SetActive(false);
            //player.SetActive(false);
            Destroy(player);
        }

        private void SetGOOnGrid()
        {
            for (int i = 0; i < GameData.Instance.FieldSize; i++)
            {
                for (int j = 0; j < GameData.Instance.FieldSize; j++)
                {
                    GOType goType = Grid.GetGOTypeByCell(i, j);
                    switch (goType)
                    {
                        case GOType.None:
                            break;
                        case GOType.Obstacles:
                            //Instantiate(Fabrica.Instance.GetObstacle(), new Vector3(i, 0.5f, j), Quaternion.Euler(Vector3.zero));
                            GameObject go = obstaclesPool.GetNext();
                            
                            go.transform.position = new Vector3(i, 0.5f, j);
                            go.SetActive(true);
                            break;
                        case GOType.Player:
                            //player.transform.position = new Vector3(i, 0.5f, j);
                            player = Instantiate(Fabrica.Instance.GetPlayer(), new Vector3(i, 0.5f, j), Quaternion.Euler(Vector3.zero));
                            break;
                        case GOType.Enemies:
                            break;
                        case GOType.Cristall:
                            break;
                        case GOType.SpawnPoint:
                            //Instantiate(Fabrica.Instance.GetSpawnPoint(), new Vector3(i, 1, j), Quaternion.Euler(Vector3.zero));
                            GameObject goSP = spawnEnemyPool.GetNext();
                            goSP.transform.position = new Vector3(i, 0.5f, j);
                            goSP.SetActive(true);
                            break;
                        case GOType.NULL:
                            break;
                        default:
                            break;
                    }

                }
            }
        }

    }
}