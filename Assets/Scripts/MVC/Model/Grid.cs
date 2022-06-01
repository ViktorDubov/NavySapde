using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public enum GOType
    {
        None,
        Obstacles,
        Player,
        Enemies,
        Cristall,
        SpawnPoint,
        NULL
    }
    public sealed class Grid
    {
        private static GOType[,] grid;
        private static Grid instance = null;
        public Grid() { }
        public static Grid Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Grid();
                    if (grid == null)
                    {
                        grid = new GOType[GameData.Instance.FieldSize, GameData.Instance.FieldSize];
                    }
                }
                return instance;
            }
        }
        public static void GenerateNewGrid()
        {
            int size = GameData.Instance.FieldSize;
            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++)
                {
                    grid[i, k] = GOType.None;
                }
            }
            int j = 0;
            while (j < GameData.Instance.CountOfObstacles)
            {
                if (SetGOTypeBycellInternal(GOType.Obstacles, UnityEngine.Random.Range(1, size - 1), UnityEngine.Random.Range(1, size - 1 )))
                {
                    j++;
                }
            }
            j = 0;
            while (j < GameData.Instance.CountOfSpawnPoint)
            {
                if (SetGOTypeBycellInternal(GOType.SpawnPoint, UnityEngine.Random.Range(1, size - 1), UnityEngine.Random.Range(1, size - 1)))
                {
                    j++;
                }
            }
            j = 0;
            while (j < 1)
            {
                if (SetGOTypeBycellInternal(GOType.Player, UnityEngine.Random.Range(1, size - 1), UnityEngine.Random.Range(1, size - 1)))
                {
                    j++;
                }
            }
        }
        private static bool SetGOTypeBycellInternal(GOType goType, int x, int y)
        {
            grid[x, y] = goType;
            return true;
        }
        public static GOType GetGOTypeByCell(int x, int y)
        {
            if (x >= 0 && x <= GameData.Instance.FieldSize - 1
                && y >= 0 && y <= GameData.Instance.FieldSize - 1
                )
            {
                return grid[x, y];
            }
            else
            {
                //Debug.LogError("Wrong coordinates");
                //throw new System.ArgumentNullException("Wrong coordinates in grid");
                return GOType.NULL;
            }
        }
        public static bool SetGOTypeBycell(GOType goType, int x, int y)
        {
            if (x >= 0 && x <= GameData.Instance.FieldSize - 1
                && y >= 0 && y <= GameData.Instance.FieldSize - 1
                )
            {
                //Debug.LogError("Wrong coordinates");
                return false;
            }
            if (goType == GOType.Obstacles || goType == GOType.SpawnPoint)
            {
                //Debug.LogError("you can not set this type of cell");
                return false;
            }
            GOType tempGOType = GetGOTypeByCell(x, y);
            if (tempGOType == GOType.Obstacles || tempGOType == GOType.SpawnPoint)
            {
                //Debug.LogError("you can not set in this cell");
                return false;
            }
            grid[x, y] = goType;
            return true;
        }
    }
}