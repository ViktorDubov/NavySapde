using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class CurrentScoreEventArgs : EventArgs
    {
        public int CurrentScore { get; set; }
        public CurrentScoreEventArgs(int currentScore)
        {
            CurrentScore = currentScore;
        }
    }
    public class MaxScoreEventArgs : EventArgs
    {
        public int MaxScore { get; set; }
        public MaxScoreEventArgs(int maxScore)
        {
            MaxScore = maxScore;
        }
    }
    public class CurrentLifeEventArgs : EventArgs
    {
        public int Life { get; set; }
        public CurrentLifeEventArgs(int currentLife)
        {
            Life = currentLife;
        }
    }
    public class DistanceToNearEnemyEventArgs : EventArgs
    {
        public float DistanceToNearEnemy { get; set; }
        public DistanceToNearEnemyEventArgs(float distanceToNearEnemy)
        {
            DistanceToNearEnemy = distanceToNearEnemy;
        }
    }
    public class DistanceToNearCristallEventArgs : EventArgs
    {
        public float DistanceToNearCristall { get; set; }
        public DistanceToNearCristallEventArgs(float distanceToNearCristall)
        {
            DistanceToNearCristall = distanceToNearCristall;
        }
    }
    public class CurrentPositionEventArgs : EventArgs
    {
        public Vector3 CurrentPosition { get; set; }
        public CurrentPositionEventArgs(Vector3 currentPosition)
        {
            CurrentPosition = currentPosition;
        }
    }
    public class CurrentTargetPositionEventArgs : EventArgs
    {
        public Vector2 TargetPosition { get; set; }
        public CurrentTargetPositionEventArgs(Vector2 targetPosition)
        {
            TargetPosition = targetPosition;
        }
    }
    public class CurrentCountOfEnemyEventArgs : EventArgs
    {
        public int CountOfEnemy { get; set; }
        public CurrentCountOfEnemyEventArgs(int countOfEnemy)
        {
            CountOfEnemy = countOfEnemy;
        }
    }
    public class CurrentCountOfCristallEventArgs : EventArgs
    {
        public int CountOfCristall { get; set; }
        public CurrentCountOfCristallEventArgs(int countOfCristall)
        {
            CountOfCristall = countOfCristall;
        }
    }
}